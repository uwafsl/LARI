using System;
using System.ComponentModel;
using System.Diagnostics;

namespace UW.WPF
{
    /// <summary>
    /// Captures some of the base functionality of a ViewModel.  This class should be inherited by any ViewModel objects.
    /// 
    /// Recall that the function and features of the view-model object should be
    ///     -The ViewModel should act as a converter that changes model information into view information and passes commands from the view into the model.
    ///     -The view binds to properties on a ViewModel, which, in turn, exposes data contained in model objects and other state specific to the view.
    ///     -The bindings between view and ViewModel are simple to construct because a ViewModel object is set as the DataContext of a view.
    ///     -The ViewModel, never the View, performs all modifications made to the model data. 
    ///     -The ViewModel and model are unaware of the view. In fact, the model is completely oblivious to the fact that the ViewModel and view exist. 
    /// </summary>
    public abstract class ViewModelBaseUW : INotifyPropertyChanged
    {
        #region Protected Methods

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        #endregion

        #region Implement INotifyPropertyChanged Interface

        /// <summary>
        /// PropertyChanged event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Debugging Aides

        /// <summary>
        /// Warns the developer if this object does not have
        /// a public property with the specified name. This 
        /// method does not exist in a Release build.
        /// 
        /// Taken from the article "WPF Apps With The Model-View-ViewModel Design Pattern" by Josh Smith
        /// "...provides the ability to verify that a property with a given name actually exists on the ViewModel object. 
        /// This is very useful when refactoring, because changing a property's name via the Visual Studio 2008 refactoring 
        /// feature will not update strings in your source code that happen to contain that property's name (nor should it). 
        /// Raising the PropertyChanged event with an incorrect property name in the event argument can lead to subtle bugs 
        /// that are difficult to track down, so this little feature can be a huge timesaver. "
        /// </summary>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }

        /// <summary>
        /// Returns whether an exception is thrown, or if a Debug.Fail() is used
        /// when an invalid property name is passed to the VerifyPropertyName method.
        /// The default value is false, but subclasses used by unit tests might 
        /// override this property's getter to return true.
        /// </summary>
        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

        #endregion
    }
}
