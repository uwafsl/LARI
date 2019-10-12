using System;
using System.Net;
using UW.Utilities;

namespace UW.WPF
{
    /// <summary>
    /// IPAddressControl view model.
    /// </summary>
    public class IPAddressControlViewModel : ViewModelBaseUW, IDisposable
    {
        //Version History
        //??/??/??: Created
        //12/07/17: Added event to notify when IPAddress changes
        //12/11/17: Changed to have custom event args

        #region Fields

        private IPAddress ipAddr = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public IPAddressControlViewModel()
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

        /// <summary>
        /// First portion of the IP address
        /// </summary>
        public int IPAddressPart1
        {
            get
            {
                byte[] bytes = this.IPAddr.GetAddressBytes();
                return (int)bytes[0];
            }

            set
            {
                if (UWFunctionsMisc.IsObjectInRange(value, 0, 255))
                {
                    int part1 = value;
                    int part2 = this.IPAddressPart2;
                    int part3 = this.IPAddressPart3;
                    int part4 = this.IPAddressPart4;

                    //convert to an IPAddress
                    IPAddress newAddress = IPAddress.Parse(part1.ToString() + "." + part2.ToString() + "." + part3.ToString() + "." + part4.ToString());
                    this.IPAddr = newAddress;
                    this.RaiseIPAddressChanged(new IPAddressChangedEventArgs(this.IPAddr));

                    this.OnPropertyChanged("IPAddressPart1");
                }
                else
                {
                    throw new ArgumentException("This portion of the IPAddress must be in the range of [0,255]");
                }
            }
        }

        /// <summary>
        /// Second portion of the IP address
        /// </summary>
        public int IPAddressPart2
        {
            get
            {
                byte[] bytes = this.IPAddr.GetAddressBytes();
                return (int)bytes[1];
            }

            set
            {
                if (UWFunctionsMisc.IsObjectInRange(value, 0, 255))
                {
                    int part1 = this.IPAddressPart1;
                    int part2 = value;
                    int part3 = this.IPAddressPart3;
                    int part4 = this.IPAddressPart4;

                    //convert to an IPAddress
                    IPAddress newAddress = IPAddress.Parse(part1.ToString() + "." + part2.ToString() + "." + part3.ToString() + "." + part4.ToString());
                    this.IPAddr = newAddress;
                    this.RaiseIPAddressChanged(new IPAddressChangedEventArgs(this.IPAddr));

                    this.OnPropertyChanged("IPAddressPart2");
                }
                else
                {
                    throw new ArgumentException("This portion of the IPAddress must be in the range of [0,255]");
                }
            }
        }

        /// <summary>
        /// Third portion of the IP address
        /// </summary>
        public int IPAddressPart3
        {
            get
            {
                byte[] bytes = this.IPAddr.GetAddressBytes();
                return (int)bytes[2];
            }

            set
            {
                if (UWFunctionsMisc.IsObjectInRange(value, 0, 255))
                {
                    int part1 = this.IPAddressPart1;
                    int part2 = this.IPAddressPart2;
                    int part3 = value;
                    int part4 = this.IPAddressPart4;

                    //convert to an IPAddress
                    IPAddress newAddress = IPAddress.Parse(part1.ToString() + "." + part2.ToString() + "." + part3.ToString() + "." + part4.ToString());
                    this.IPAddr = newAddress;
                    this.RaiseIPAddressChanged(new IPAddressChangedEventArgs(this.IPAddr));

                    this.OnPropertyChanged("IPAddressPart3");
                }
                else
                {
                    throw new ArgumentException("This portion of the IPAddress must be in the range of [0,255]");
                }
            }
        }

        /// <summary>
        /// Fourth portion of the IP address
        /// </summary>
        public int IPAddressPart4
        {
            get
            {
                byte[] bytes = this.IPAddr.GetAddressBytes();
                return (int)bytes[3];
            }

            set
            {
                if (UWFunctionsMisc.IsObjectInRange(value, 0, 255))
                {
                    int part1 = this.IPAddressPart1;
                    int part2 = this.IPAddressPart2;
                    int part3 = this.IPAddressPart3;
                    int part4 = value;

                    //convert to an IPAddress
                    IPAddress newAddress = IPAddress.Parse(part1.ToString() + "." + part2.ToString() + "." + part3.ToString() + "." + part4.ToString());
                    this.IPAddr = newAddress;
                    this.RaiseIPAddressChanged(new IPAddressChangedEventArgs(this.IPAddr));

                    this.OnPropertyChanged("IPAddressPart4");
                }
                else
                {
                    throw new ArgumentException("This portion of the IPAddress must be in the range of [0,255]");
                }
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Event that the IP Address changed.
        /// </summary>
        public event EventHandler<IPAddressChangedEventArgs> IPAddressChanged;

        #endregion

        #region Public Methods (Safely Raise/Fire Events)

        /// <summary>
        /// Safely raise the IPAddressChanged event
        /// </summary>
        /// <param name="e"></param>
        public void RaiseIPAddressChanged(IPAddressChangedEventArgs e)
        {
            if (this.IPAddressChanged != null)
            {
                this.IPAddressChanged(this, e);
            }
        }
        
        #endregion

        #region Public Methods

        /// <summary>
        /// The IP Address that this control contains
        /// </summary>
        public IPAddress ObtainIPAddress()
        {
            return this.IPAddr;
        }

        #endregion

        #region Private Methods

        #endregion

        #region Private Methods (acquire/release controller, create/dispose commands, subscribe/unsubscribe events, start/stop error handling, initialize/dispose private fields)

        /// <summary>
        /// Obtain controllers from the TRAPISManagerController
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
            //TODO: Perhaps use the MessageLogger for error handling
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
            this.ipAddr = IPAddress.Parse("127.0.0.1");
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
        /// Dispose the resources associated with this view model.
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
