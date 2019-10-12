using System;
using System.Net;

namespace UW.WPF
{
    /// <summary>
    /// Custom event args for the IPAddressChanged event
    /// </summary>
    public class IPAddressChangedEventArgs : EventArgs
    {
        //Version History
        //12/11/17: Created

        #region Fields

        private IPAddress ipAddr;
        
        #endregion

        #region Constructors

        
        public IPAddressChangedEventArgs(IPAddress ipAddr)
            : base()
        {   
            this.IPAddr = ipAddr;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The IPAddress
        /// </summary>
        public IPAddress IPAddr
        {
            get
            {
                return this.ipAddr;
            }

            set
            {
                this.ipAddr = value;
            }
        }

        #endregion
    }
}

