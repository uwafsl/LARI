using System;
using System.Xml;

namespace LARI.Models
{
    /// <summary>
    /// This controller holds information with user settings such as
    /// 
    ///     - XML file for saving and loading XML equipage data
    /// </summary>
    public sealed class UserSettingsModel : IDisposable
    {
        //Version History
        //05/25/18: Created

        #region Fields

        /// <summary>
        /// Singleton instance.
        /// </summary>
        private static volatile UserSettingsModel instance;

        /// <summary>
        /// Lock for multi-threaded singleton.
        /// </summary>
        private static object syncRoot = new object();

        /// <summary>
        /// See EquipageeFilePath property.
        /// </summary>
        private string equipageFilePath;

        #region Events

        /// <summary>
        /// Event that fires when a user setting has changed
        /// </summary>
        /* TODO: Consider having seperate events for different settings.
         * Having a single even for ever settings change could add a lot of
         * unnecessary overhead once the project and it's number of settings
         * grows sufficiently large.
         */
        public event EventHandler<EventArgs> UserSettingsChanged;

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public UserSettingsModel()
        {
            this.startErrorHandling();
            this.initializeOtherPrivateFields();
            this.createCommands();
            this.acquireControllers();
            this.subscribeToEvents();
        }

        #endregion

        #region Properties

        /// <summary>
        /// The unique, singleton instance of the UserSettingsModel.
        /// </summary>
        public static UserSettingsModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new UserSettingsModel();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Path of file where equipage data is stored.
        /// </summary>
        public string EquipageFilePath
        {
            get
            {
                return this.equipageFilePath;
            }

            set
            {
                this.equipageFilePath = value;
                this.RaiseUserSettingsChanged(new EventArgs());
            }
        }

        #endregion

        #region Public Methods (Safely Raise/Fire Events)

        /// <summary>
        /// Safely raise the UserSettingsChanged event
        /// </summary>
        /// <param name="e"></param>
        public void RaiseUserSettingsChanged(EventArgs e)
        {
            if (this.UserSettingsChanged != null)
            {
                this.UserSettingsChanged(this, e);
            }
        }

        /// <summary>
        /// Reads settings from an XML file.
        /// </summary>
        /// <param name="fn">Filename of the settings file.</param>
        public void ReadSettings(string fn)
        {
            // TODO: error catching and reporting
            // TODO: unit test
            XmlReader reader = XmlReader.Create(fn);
            reader.MoveToContent();
            reader.Read();
            Boolean inConfig = false;
            Boolean readingEquipageFilePath = false;
            while (!reader.EOF && reader.ReadState == ReadState.Interactive)
            {
                if (!inConfig)
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "LARIConfig")
                    {
                        inConfig = true;
                    }
                    else
                    {
                        // Invalid tag encountered.
                        // TODO: report error and terminate
                    }
                }
                else
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.Name == "Settings" && !readingEquipageFilePath)
                            {
                                // TODO: check "name" attribute
                            }
                            else
                            {
                                // Invalid tag encountered.
                                // TODO: report and terminate
                            }
                            break;
                        case XmlNodeType.Text:
                            if (readingEquipageFilePath)
                            {
                                this.equipageFilePath = reader.Value;
                            }
                            else
                            {
                                // Irrelevant text encountered.
                                // TODO: report and terminate
                            }
                            break;
                        case XmlNodeType.EndElement:
                            if (readingEquipageFilePath)
                            {
                                readingEquipageFilePath = false;
                            }
                            else
                            {
                                inConfig = false;
                            }
                            break;
                        default:
                            // Invalid node encountered.
                            // TODO: report and terminate
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Reads settings from an XML file.
        /// </summary>
        /// <param name="fn">Filename of the settings file.</param>
        public void WriteSettings(string fn)
        {
            // TODO: error catching and reporting
            // TODO: manually test
            // TODO: write unit test
            XmlWriter writer = XmlWriter.Create(fn);
            writer.WriteStartDocument();
            writer.WriteStartElement("LARIConfig");

            writer.WriteStartElement("Setting");
            writer.WriteAttributeString("name", "EquipageFilePath");
            writer.WriteString(this.equipageFilePath);
            writer.WriteEndElement();

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }


        #endregion

        #region Public Overrides

        /// <summary>
        /// Generates a string representation of the object, including the values of each setting.
        /// This should not be used for saving the object, merely for display and for debugging.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // Display settings
            string str = "EquipageFilePath, " + this.equipageFilePath;
            
            return str;
        }

        #endregion

        #region Private Methods (acquire/release controller, create/dispose commands, subscribe/unsubscribe events, start/stop error handling, initialize/dispose private fields)

        /// <summary>
        /// Obtain controllers
        /// </summary>
        private void acquireControllers()
        {
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
            this.equipageFilePath = string.Empty;
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
