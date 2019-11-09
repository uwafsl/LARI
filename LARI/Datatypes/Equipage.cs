using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Data.SQLite;

namespace UW.LARI.Datatypes
{
    // TODO: Implement the necessary parts to tie to CrashHistory and Notes tables

    /// <summary>
    /// A controller for maintaining equipage of various systems.
    /// 
    /// Some responsibilities of this controller include but are not limited to:
    /// 
    /// -Maintaining a list of AFSLSystems
    /// -Exposing data so other applications can access this information.
    /// </summary>
    public class Equipage : IDisposable
    {
        //Version History:
        //07/25/18: Created

        #region Fields

        // List of systems in the fleet
        private List<AFSLSystem> fleet;

        // Lock in order to prevent data conflicts
        private static readonly object locker = new object();

        // SQLite Connection object for connecting to the local database
        private SQLiteConnection conn;

        // commands
        private SQLiteCommand createTablesCommand;
        private SQLiteCommand insertSystemCommand;
        private SQLiteCommand insertComponentCommand;
        private SQLiteCommand insertNoteCommand;
        private SQLiteCommand insertCrashHistoryCommand;
        private SQLiteCommand getSystemCommand;
        private SQLiteCommand getComponentCommand;
        private SQLiteCommand getAllComponentsForSystemCommand;
        private SQLiteCommand getAllComponentsCommand;
        private SQLiteCommand getAllSystemsCommand;
        private SQLiteCommand removeSystemCommand;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor that creates an in memory sqlite database
        /// </summary>
        //public Equipage()
        //{
        //    conn = new SQLiteConnection("Data Source=.\\test.db;Version=3;New=True");
        //    conn.Open();
        //    SQLiteCommand command = conn.CreateCommand();
        //    command.CommandText = "PRAGMA foreign_keys=ON";
        //    command.ExecuteNonQuery();
        //    this.initializeSqlCommands();

        //    // Create tables in memory
        //    using (var tx = conn.BeginTransaction())
        //    {
        //        createTablesCommand.ExecuteNonQuery();
        //        tx.Commit();
        //    }

        //    this.startErrorHandling();
        //    this.initializeOtherPrivateFields();
        //    this.createCommands();
        //    this.acquireControllers();
        //    this.subscribeToEvents();
        //}

        /// <summary>
        /// Constructor that takes a given, existing database
        /// </summary>
        public Equipage() 
        {
            string dbFilePath = ".\\test.db";
            if (!File.Exists(dbFilePath))
            {
                SQLiteConnection.CreateFile(dbFilePath);
            }
            conn = new SQLiteConnection("Data Source=" + dbFilePath + ";Version=3;");
            conn.Open();
            SQLiteCommand command = conn.CreateCommand();
            command.CommandText = "PRAGMA foreign_keys=ON";
            command.ExecuteNonQuery();
            this.initializeSqlCommands();
            this.startErrorHandling();
            this.initializeOtherPrivateFields();
            this.createCommands();
            this.acquireControllers();
            this.subscribeToEvents();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Number of vehicles in the database.
        /// </summary>
        public int NumVehicles
        {
            get
            {
                string sql = "select count(*) from systems";
                SQLiteCommand countSystems = new SQLiteCommand(sql, conn);
                SQLiteDataReader reader = countSystems.ExecuteReader();
                if (!reader.Read())
                    throw new KeyNotFoundException("Cannot read from systems table");
                return reader.GetInt32(0);
            }
        }

        /// <summary>
        /// A list of the systems that this database has data for
        /// </summary>
        public List<AFSLSystem> Fleet
        {
            get
            {
                // TODO: This will be slow with many parts.
                // Need to update fleet with every other method in this file
                // rather than clearing the fleet and repopulating
                this.fleet.Clear();

                using (SQLiteDataReader reader = getAllSystemsCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        this.fleet.Add(GetSystem(reader.GetString(0)));
                    }
                }

                return this.fleet;
            }
        }

        /// <summary>
        /// DEPRECATED: localName used to encode object as xml element
        /// </summary>
        public const string LocalName = "equipage";

        /// <summary>
        /// name of default system
        /// </summary>
        public const string InventorySystem = "Inventory";

        #endregion

        #region Public Methods

        /// <summary>
        /// Add flight system to this Equipage
        /// </summary>
        /// <param name="newSystem"></param>
        public void AddSystem(AFSLSystem newSystem)
        {
            getSystemCommand.Parameters["@Name"].Value = newSystem.Name;
            SQLiteDataReader reader = getSystemCommand.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                throw new ArgumentException("A system with the given name already exists");
            }
            reader.Close();

            foreach(Component component in newSystem.Components)
            {
                getComponentCommand.Parameters["Id"].Value = component.PartNumber;
                reader = getComponentCommand.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close();
                    throw new ArgumentException("The given system contains components that already exist");
                }
                reader.Close();
            }

            // Add the system
            insertSystemCommand.Parameters["@Name"].Value = newSystem.name;
            insertSystemCommand.Parameters["@Description"].Value = newSystem.description;
            insertSystemCommand.Parameters["@WingType"].Value = newSystem.wingType;
            insertSystemCommand.Parameters["@StartDate"].Value = DateTime.Now;
            insertSystemCommand.ExecuteNonQuery();

            // Add the components
            foreach(Component c in newSystem.Components)
            {
                AddComponent(c, newSystem.Name);
            }
        }

        /// <summary>
        /// Remove flight system from this Equipage
        /// </summary>
        /// <param name="afslSystemName"></param>
        public void RemoveSystem(string afslSystemName)
        {
            // TODO: SQL-ize this
            for(int i = 0; i < this.Fleet.Count; i++)
            {
                if(this.Fleet[i].Name == afslSystemName)
                {
                    this.Fleet.RemoveAt(i);
                }
            }

            removeSystemCommand.Parameters["@Name"].Value = afslSystemName;
            removeSystemCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Gets a particular system from the database
        /// </summary>
        /// <param name="systemName"></param>
        /// <returns>The </returns>
        public AFSLSystem GetSystem(string systemName)
        {
            string description;
            DateTime startDate;
            List<Component> components;
            WingTypes wingType;
            ///using (var tx = conn.BeginTransaction())
            ///{
                getSystemCommand.Parameters["@Name"].Value = systemName;
                using (SQLiteDataReader reader = getSystemCommand.ExecuteReader())
                {
                    ///tx.Commit();
                    if (!reader.Read())
                    {
                        return null;
                    }
                    else
                    {
                        description = reader.GetString(1);
                        string wingTypeString = reader.GetString(2);
                        wingType = ConvertToWingType(wingTypeString);
                        startDate = reader.GetDateTime(3);
                    }
                }
            ///}

            // Get corresponding components
            components = GetAllComponentsForSystem(systemName);
            return new AFSLSystem(systemName, description, components, wingType);
        }

        /// <summary>
        /// Add a component to the specified system or inventory system
        /// </summary>
        /// <param name="component"></param>
        /// <param name="systemName"></param>
        public void AddComponent(Component component, string systemName=InventorySystem)
        {
            if (ContainsComponent(component.PartNumber))
            {
                throw new ArgumentException("Component Part Number " + component.PartNumber + " already exists.");
            }
            using (var tx = conn.BeginTransaction())
            {
                // (@Name, @Description, @StartDate, @Active, @System)
                insertComponentCommand.Parameters["@Name"].Value = "NoName";
                insertComponentCommand.Parameters["@Description"].Value = component.Description;
                insertComponentCommand.Parameters["@StartDate"].Value = DateTime.Now;
                insertComponentCommand.Parameters["@Active"].Value = component.Active;
                if (systemName.Equals(InventorySystem))
                    insertComponentCommand.Parameters["@System"].Value = DBNull.Value;
                else
                    insertComponentCommand.Parameters["@System"].Value = systemName;
                insertComponentCommand.ExecuteNonQuery();
                tx.Commit();
            }
        }

        /// <summary>
        /// Gets a particular component from the database
        /// </summary>
        /// <param name="partNumber">The associated partNumber for a component</param>
        /// <returns>The corresponding component</returns>
        public Component GetComponent(int partNumber)
        {
            using (var tx = conn.BeginTransaction())
            {
                getComponentCommand.Parameters["@Id"].Value = partNumber;
                using (SQLiteDataReader reader = getComponentCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // TODO: Link up the notes and crashHistory stuff
                        return new Component(reader.GetInt32(0), reader.GetString(1), 
                                             reader.GetString(3), reader.GetString(2), reader.GetBoolean(4));
                    } else
                    {
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Gets all components for a specific system
        /// </summary>
        /// <param name="systemName">The specified system to retieve components for</param>
        /// <returns>List of all components for the systemName</returns>
        private List<Component> GetAllComponentsForSystem(string systemName)
        {
            List<Component> components = new List<Component>();
            ///using (var tx = conn.BeginTransaction())
            ///{
                getAllComponentsForSystemCommand.Parameters["@System"].Value = systemName;
                using (SQLiteDataReader reader = getAllComponentsForSystemCommand.ExecuteReader())
                {
                    ///tx.Commit();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                        // TODO: Link up the notes and crashHistory stuff
                            Component temp = new Component(reader.GetInt32(0), reader.GetString(1),
                                                    reader.GetString(3), reader.GetString(2), reader.GetBoolean(4));
                            components.Add(temp);
                            Console.WriteLine(temp.name);
                        }
                    }
                }
            ///}
            return components;
        }

        /// <summary>
        /// Retrieves all components (in the inventory) in the database
        /// </summary>
        /// <returns>List of all components in the database</returns>
        private List<Component> getAllComponents()
        {
            List<Component> components = new List<Component>();
            using (var tx = conn.BeginTransaction())
            {
                using (SQLiteDataReader reader = getAllComponentsCommand.ExecuteReader())
                {
                    tx.Commit();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            reader.Read();
                            // TODO: Link up the notes and crashHistory stuff
                            components.Add(new Component(reader.GetInt32(0), reader.GetString(1),
                                                         reader.GetString(3), reader.GetString(2), reader.GetBoolean(4)));
                        }
                    }
                }
            }
            return components;
        }

        /// <summary>
        /// remove a component from the specified system or inventory
        /// </summary>
        /// <param name="partNumber"></param>
        /// <param name="systemName"></param>
        public void RemoveComponent(int partNumber, string systemName=InventorySystem)
        {
            // TODO: Incorrect implementation now
            foreach(AFSLSystem system in this.fleet)
            {
                if(system.Name == systemName)
                {
                    for(int i = 0; i < system.Components.Count; i++)
                    {
                        if(system.Components[i].PartNumber == partNumber)
                        {
                            system.Components.RemoveAt(i);
                            return;
                        }
                    }
                    throw new KeyNotFoundException("Specified component not found on system");
                }
            }
            throw new KeyNotFoundException("Specified flight system was not found");
        }

        /// <summary>
        /// find and remove a component
        /// </summary>
        /// <param name="partNumber"></param>
        public void RemoveComponent(int partNumber)
        {
            // TODO: SQL-ize this
            foreach(AFSLSystem system in this.fleet)
            {
                for(int i = 0; i < system.Components.Count; i++)
                {
                    if (system.Components[i].PartNumber == partNumber)
                    {
                        system.Components.RemoveAt(i);
                        return;
                    }
                }
            }
            throw new KeyNotFoundException("Component not found in database");
        }

        /// <summary>
        /// Move a component from a system, equip to new system or inventory by default
        /// </summary>
        /// <param name="partNumber"></param>
        /// <param name="currentSystemName"></param>
        /// <param name="newSystemName"></param>
        public void MoveComponent(int partNumber, string currentSystemName=InventorySystem, string newSystemName=InventorySystem)
        {
            // check for no-op
            if(currentSystemName == newSystemName)
            {
                return;
            }

            int currentIdx = -1;
            int newIdx = -1;
            for(int i = 0; i < this.fleet.Count; i++)
            {
                if(this.fleet[i].Name == currentSystemName)
                {
                    currentIdx = i;
                }
                if(this.fleet[i].Name == newSystemName)
                {
                    newIdx = i;
                }
            }

            if(currentIdx < 0)
            {
                throw new KeyNotFoundException("Specified flight system was not found");
            }
            if(newIdx < 0)
            {
                throw new KeyNotFoundException("Specified flight system was not found");
            }

            Component popped;
            try
            {
                popped = this.fleet[currentIdx].PopComponent(partNumber);
            }
            catch (KeyNotFoundException ex)
            {
                throw ex;
            }
            this.fleet[newIdx].AddComponent(popped);
        }

        /// <summary>
        /// returns true if afslsystem is found in database by name
        /// </summary>
        /// <param name="name"></param>
        public bool ContainsAfslSystem(string name)
        {
            bool returnVal;
            using (var tx = conn.BeginTransaction())
            {
                getSystemCommand.Parameters["@Name"].Value = name;
                using (SQLiteDataReader reader = getSystemCommand.ExecuteReader())
                {
                    tx.Commit();
                    returnVal = reader.HasRows;
                }
            }
            return returnVal;
        }

        /// <summary>
        /// returns true if component is found in database by part number
        /// </summary>
        /// <param name="partNumber"></param>
        public bool ContainsComponent(int partNumber)
        {
            bool returnVal;
            using (var tx = conn.BeginTransaction())
            {
                getComponentCommand.Parameters["@Id"].Value = partNumber;
                using (SQLiteDataReader reader = getComponentCommand.ExecuteReader())
                {
                    tx.Commit();
                    returnVal = reader.HasRows;
                }
            }
            return returnVal;
        }

        /// <summary>
        /// write database to xml file
        /// </summary>
        /// <param name="directoryFileName"></param>
        [Obsolete("WriteToXMLFile is deprecated, database is now using SQLite")]
        public void WriteToXMLFile(string directoryFileName)
        {
            XmlWriter writer = XmlWriter.Create(directoryFileName); // will no-op if already created
            writer.WriteStartDocument();
            writer.WriteStartElement(Equipage.LocalName);

            foreach (AFSLSystem system in this.fleet)
            {
                system.WriteAsXML(writer);
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }

        /// <summary>
        /// load database from xml file
        /// </summary>
        /// <param name="directoryFileName"></param>
        [Obsolete("ReadFromXMLFile is deprecated, database is now using SQLite")]
        public void ReadFromXMLFile(string directoryFileName)
        {
            XmlReader reader;
            
            Fleet.RemoveAll(system => system.Name != InventorySystem);
            Fleet[0].Components.Clear(); // only remaining system should be inventory so clear inventory components

            try
            {
                reader = XmlReader.Create(directoryFileName);
            } catch (FileNotFoundException e)
            {
                throw e;
            }

            try
            {
                reader.Read(); // read xml header
                reader.Read(); // read Equipage start tag
            }
            catch (XmlException e)
            {
                throw e;
            }

            if (reader.LocalName != Equipage.LocalName)
            {
                throw new XmlException("Unexpected XML element. Expected: " + AFSLSystem.LocalName +
                                                        ", Value: " + reader.LocalName);
            }

            try
            {
                reader.Read(); // read first afslsystem start tag
            }
            catch (XmlException e)
            {
                throw e;
            }

            while(reader.LocalName != Equipage.LocalName)
            {
                AFSLSystem system = new AFSLSystem();
                system.ReadFromXML(ref reader);
                this.AddSystem(system);
                try
                {
                    reader.Read(); // read past end tag of AFSLSystem
                }
                catch (XmlException e)
                {
                    throw e;
                }
            }
        }

        #endregion

        #region Private Methods
        private WingTypes ConvertToWingType(string wingType)
        {
            if (wingType.Equals(WingTypes.FixedWing.ToString()))
            {
                return WingTypes.FixedWing;
            }
            else if (wingType.Equals(WingTypes.Octo.ToString()))
            {
                return WingTypes.Octo;
            } else if (wingType.Equals(WingTypes.Quad.ToString()))
            {
                return WingTypes.Quad;
            } else if (wingType.Equals(WingTypes.None.ToString()))
            {
                return WingTypes.None;
            } else
            {
                return WingTypes.Unspecified;
            }
        }

        private void initializeSqlCommands()
        {
            initializeCreateTablesCommand();
            initializeInsertSystemCommand();
            initializeInsertComponentCommand();
            initializeInsertNoteCommand();
            initializeInsertCrashHistoryCommand();
            initializeGetSystemCommand();
            initializeGetComponentCommand();
            initializeGetAllComponentsForSystemCommand();
            initializeGetAllSystemsCommand();
            initializeRemoveSystemsCommand();
        }

        private void initializeRemoveSystemsCommand()
        {
            string removeSystemSql = @"DELETE FROM Systems WHERE name=@Name";
            removeSystemCommand = conn.CreateCommand();
            removeSystemCommand.CommandText = removeSystemSql;
            removeSystemCommand.Parameters.AddWithValue("@Name", "");
        }

        private void initializeCreateTablesCommand()
        {
            string createTablesSql = @"PRAGMA foreign_keys=ON;

            DROP TABLE IF EXISTS Systems;
            CREATE TABLE Systems(
                name TEXT PRIMARY KEY,
                description TEXT,
                wing_type TEXT,
                start_date TEXT
            );

            DROP TABLE IF EXISTS Components;
            CREATE TABLE Components(
                id INTEGER PRIMARY KEY,
                name TEXT,
                description TEXT,
                start_date TEXT,
                active NUMERIC,
                system TEXT NULL REFERENCES Systems(name)
            );

            DROP TABLE IF EXISTS Notes;
            CREATE TABLE Notes(
                id INTEGER PRIMARY KEY,
                summary TEXT,
                description TEXT,
                component INTEGER REFERENCES Components(id)
            );

            DROP TABLE IF EXISTS CrashHistory;
            CREATE TABLE CrashHistory(
                id INTEGER PRIMARY KEY,
                summary TEXT,
                description TEXT,
                time TEXT,
                location TEXT,
                component INTEGER REFERENCES Components(id)
            );";
            createTablesCommand = conn.CreateCommand();
            createTablesCommand.CommandText = createTablesSql;
        }

        private void initializeInsertSystemCommand()
        {
            string insertSystemSql = @"INSERT INTO Systems(name, description, wing_type, start_date)
            VALUES(@Name, @Description, @WingType, @StartDate)";
            insertSystemCommand = conn.CreateCommand();
            insertSystemCommand.CommandText = insertSystemSql;
            insertSystemCommand.Parameters.AddWithValue("@Name", "");
            insertSystemCommand.Parameters.AddWithValue("@Description", "");
            insertSystemCommand.Parameters.AddWithValue("@WingType", "");
            insertSystemCommand.Parameters.AddWithValue("@StartDate", DateTime.Now);
        }

        private void initializeInsertComponentCommand()
        {
            string sql = @"INSERT INTO Components(id, name, description, start_date, active, system) 
            VALUES (@Id, @Name, @Description, @StartDate, @Active, @System)";
            insertComponentCommand = conn.CreateCommand();
            insertComponentCommand.CommandText = sql;
            insertComponentCommand.Parameters.AddWithValue("@Id", "");
            insertComponentCommand.Parameters.AddWithValue("@Name", "");
            insertComponentCommand.Parameters.AddWithValue("@Description", "");
            insertComponentCommand.Parameters.AddWithValue("@StartDate", DateTime.Now);
            insertComponentCommand.Parameters.AddWithValue("@Active", 0);
            insertComponentCommand.Parameters.AddWithValue("@System", DBNull.Value);
        }

        private void initializeInsertNoteCommand()
        {
            string sql = @"INSERT INTO Notes(summary, description, component)
                        VALUES (@Summary, @Description, @Component)";
            insertNoteCommand = conn.CreateCommand();
            insertNoteCommand.CommandText = sql;
            insertNoteCommand.Parameters.AddWithValue("@Summary", "");
            insertNoteCommand.Parameters.AddWithValue("@Description", "");
            insertNoteCommand.Parameters.AddWithValue("@Component", 1);
        }

        private void initializeInsertCrashHistoryCommand()
        {
            string sql = @"INSERT INTO CrashHistory(summary, description, time, location, component)
                        VALUES (@Summary, @Description, @Time, @Location, @Component)";
            insertCrashHistoryCommand = conn.CreateCommand();
            insertCrashHistoryCommand.CommandText = sql;
            insertCrashHistoryCommand.Parameters.AddWithValue("@Summary", "");
            insertCrashHistoryCommand.Parameters.AddWithValue("@Description", "");
            insertCrashHistoryCommand.Parameters.AddWithValue("@Time", DateTime.Now);
            insertCrashHistoryCommand.Parameters.AddWithValue("@Location", "UWCUTS");
            insertCrashHistoryCommand.Parameters.AddWithValue("@Component", 1);
        }

        private void initializeGetSystemCommand()
        {
            string sql = "SELECT * FROM Systems WHERE name=@Name";
            getSystemCommand = conn.CreateCommand();
            getSystemCommand.CommandText = sql;
            getSystemCommand.Parameters.AddWithValue("@Name", "");
        }

        private void initializeGetComponentCommand()
        {
            string sql = "SELECT * FROM Components WHERE id=@Id";
            getComponentCommand = conn.CreateCommand();
            getComponentCommand.CommandText = sql;
            getComponentCommand.Parameters.AddWithValue("@Id", 1);
        }

        private void initializeGetAllComponentsForSystemCommand()
        {
            string sql = "SELECT * FROM Components WHERE system=@System";
            getAllComponentsForSystemCommand = conn.CreateCommand();
            getAllComponentsForSystemCommand.CommandText = sql;
            getAllComponentsForSystemCommand.Parameters.AddWithValue("@System", "");
        }

        private void initializeGetAllComponents()
        {
            string sql = "SELECT * FROM Components";
            getAllComponentsCommand = conn.CreateCommand();
            getAllComponentsCommand.CommandText = sql;
        }

        private void initializeGetAllSystemsCommand()
        {
            string sql = "SELECT * FROM Systems";
            getAllSystemsCommand = conn.CreateCommand();
            getAllSystemsCommand.CommandText = sql;
        }

        #endregion

        #region Private Methods (event handlers)

        #endregion

        #region Private Methods (acquire/release controller, create/dispose commands, subscribe/unsubscribe events, start/stop error handling, initialize/dispose private fields)

        /// <summary>
        /// Obtain controllers from the TRAPISManagercontroller.
        /// </summary>
        private void acquireControllers()
        {
            this.releaseControllers();
            //this.fileController = TRAPISManagerController.Instance.AcquireFileController();
        }

        /// <summary>
        /// Release controllers which do not require persistence
        /// </summary>
        private void releaseControllers()
        {
        }

        private void createCommands()
        {
        }

        private void disposeCommands()
        {
        }

        private void subscribeToEvents()
        {
            this.unsubscribeFromEvents();
        }

        private void unsubscribeFromEvents()
        {
        }

        /// <summary>
        /// Start error handling and logging.  Mostly for displaying "toast" messages and writing messages to the log files.
        /// </summary>
        private void startErrorHandling()
        {
        }

        /// <summary>
        /// Stop error handling and logging.
        /// </summary>
        private void stopErrorHandling()
        {
        }

        /// <summary>
        /// Initialize and instantiate private fields that are not controllers, commands, or error token (these are initialized in other methods)
        /// </summary>
        private void initializeOtherPrivateFields()
        {
            this.fleet = new List<AFSLSystem>() { new AFSLSystem(InventorySystem, "inventory/unequipped", new List<Component>(), WingTypes.None) };
        }

        /// <summary>
        /// Dispose of any other private fields which are unmanaged.  Fields which are controllers, commands, or error tokens are disposed in other methods)
        /// </summary>
        private void disposeUnmanagedOtherPrivateFields()
        {
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Dispose the resources associated with this controller.
        /// </summary>
        public void Dispose()
        {
            this.unsubscribeFromEvents();
            this.releaseControllers();
            this.disposeCommands();
            this.disposeUnmanagedOtherPrivateFields();
            this.stopErrorHandling();
        }

        #endregion
    }
}
