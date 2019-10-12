using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace UW.WPF
{
    /// <summary>
    /// Class to provide some static methods for performing WPF operations
    /// </summary>
    public static class UWUtilitiesWPF
    {
        /// <summary>
        /// In some situations, the UI may not update until after a method has completed.  
        /// Calling this method after setting a property will allow the UI to update.  
        /// 
        /// Source:
        /// http://social.msdn.microsoft.com/forums/en-US/wpf/thread/6fce9b7b-4a13-4c8d-8c3e-562667851baa/
        /// Post by Samuel Jack on August 02, 2006
        /// </summary>
        public static void AllowUIToUpdate()
        {

            DispatcherFrame frame = new DispatcherFrame();

            Dispatcher.CurrentDispatcher.BeginInvoke(
                DispatcherPriority.Render,
                new DispatcherOperationCallback(delegate(object parameter)
                {
                    frame.Continue = false;
                    return null;

                }), null);

            Dispatcher.PushFrame(frame);
        }
    }
}
