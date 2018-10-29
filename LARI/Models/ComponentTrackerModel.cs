using System;
using System.Collections.Generic;
using System.Xml;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using UW.LARI.Datatypes;

namespace LARI.Models
{
    /// <summary>
    /// This controller holds information with the system view such as
    /// 
    ///     - system and component data
    /// </summary>
    public class ComponentTrackerModel : IDisposable
    {
        //Version History
        //05/25/18: Created

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ComponentTrackerModel()
        {
            this.startErrorHandling();
            this.initializeOtherPrivateFields();
            this.createCommands();
            this.acquireControllers();
            this.subscribeToEvents();
        }

        #endregion

        #region Fields

        private ManagerModel manager;
        private object afslsystem;

        #endregion

        #region Properties

        // TODO: getter and setter for systems datastructure

        #endregion

        #region Events

        // TODO: needed?

        #endregion

        #region Private Methods (acquire/release controller, create/dispose commands, subscribe/unsubscribe events, start/stop error handling, initialize/dispose private fields)

        /// <summary>
        /// Obtain controllers
        /// </summary>
        private void acquireControllers()
        {
            var doc = XDocument.Parse("Equipage.xml");
            var roster = new Equipage();
            foreach (var afslsystem in doc.Root.Element("equipage").Elements("afslsystem"))
            {
                AFSLSystem newSystem = new AFSLSystem(afslsystem.Attribute("Name").Value, afslsystem.Attribute("WingType").Value);
                roster.AddSystem(newSystem);
                foreach (var component in doc.Root.Elements("afslsystem"))
                {
                    UW.LARI.Datatypes.Component newComponent = new UW.LARI.Datatypes.Component(component.Attribute("description").Value, int.Parse(component.Attribute("id").Value));
                    newSystem.AddComponent(newComponent);
                }
            }
            manager.AcquireEquipage();
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
            // TODO: initialize systems datastructure
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
