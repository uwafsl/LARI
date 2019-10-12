using System;
using System.Windows.Media;
using UW.MathFunctions;

namespace UW.WPF
{
    /// <summary>
    /// A class which will allow colors to be interpolated based on a value.
    /// </summary>
    public class ColorInterpolater
    {
        #region Fields

        /// <summary>
        /// starting value
        /// </summary>
        private double _valStart;

        /// <summary>
        /// ending value
        /// </summary>
        private double _valEnd;

        /// <summary>
        /// Red value that corresponds to the _valStart
        /// </summary>
        private byte _startR;

        /// <summary>
        /// Green value that corresponds to the _valStart
        /// </summary>
        private byte _startG;

        /// <summary>
        /// Blue value that corresponds to the _valStart
        /// </summary>
        private byte _startB;

        /// <summary>
        /// Red value that corresponds to the _valEnd
        /// </summary>
        private byte _endR;

        /// <summary>
        /// Green value that corresponds to the _valEnd
        /// </summary>
        private byte _endG;

        /// <summary>
        /// Blue value that corresponds to the _valEnd
        /// </summary>
        private byte _endB;

        #endregion

        #region Constructors

        /// <summary>
        /// Default contructor
        /// </summary>
        public ColorInterpolater()
        {
            //Version History:
            //07/18/12: Created

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------
            

            //--------------Begin Calculations----------------
            this._valStart = 0;
            this._valEnd = 1;
            this._startR = 0;
            this._startG = 0;
            this._startB = 0;
            this._endR = 255;
            this._endG = 255;
            this._endB = 255;
        }



        /// <summary>
        /// Construct from specified values and colors
        /// </summary>
        /// <param name="valStart"></param>
        /// <param name="valEnd"></param>
        /// <param name="colorStart"></param>
        /// <param name="colorEnd"></param>
        public ColorInterpolater(double valStart, double valEnd, SolidColorBrush colorStart, SolidColorBrush colorEnd)
            : this()
        {
            //Version History:
            //07/18/12: Created

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------
            this._valStart = valStart;
            this._valEnd = valEnd;

            this._startR = colorStart.Color.R;
            this._startG = colorStart.Color.G;
            this._startB = colorStart.Color.B;

            this._endR = colorEnd.Color.R;
            this._endG = colorEnd.Color.G;
            this._endB = colorEnd.Color.B;

        }

        #endregion

        #region Public Methods
                
        /// <summary>
        /// Gets the color corresponding to the specified value.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public SolidColorBrush ColorAtSpecifiedValue(double val)
        {
            //Version History:
            //07/18/12: Created

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------
            byte R = 0;
            byte G = 0;
            byte B = 0;

            this._RGBAtSpecifiedValue(val, ref R, ref G, ref B);

            //create a solid colored brush out of the RGB values
            SolidColorBrush solidColorBrush = new SolidColorBrush();
            solidColorBrush.Color = Color.FromRgb(R, G, B);
            return solidColorBrush;
        }
        
        #endregion

        #region Private Methods

        private void _RGBAtSpecifiedValue(double val, ref byte R, ref byte G, ref byte B)
        {
            //Version History:
            //07/18/12: Created

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------
            if (val <= this._valStart)
            {
                R = this._startR;
                G = this._startG;
                B = this._startB;
            }
            else if (val >= this._valEnd)
            {
                R = this._endR;
                G = this._endG;
                B = this._endB;
            }
            else
            {
                //interpolate the color
                double Rinterpolated = UWFunctionsMath.Interp1(this._valStart, this._valEnd, (double)this._startR, (double)this._endR, val, InterpolationExtrapMethods.HoldEndPoints);
                double Ginterpolated = UWFunctionsMath.Interp1(this._valStart, this._valEnd, (double)this._startG, (double)this._endG, val, InterpolationExtrapMethods.HoldEndPoints);
                double Binterpolated = UWFunctionsMath.Interp1(this._valStart, this._valEnd, (double)this._startB, (double)this._endB, val, InterpolationExtrapMethods.HoldEndPoints);

                R = (byte)Math.Round(Rinterpolated);
                G = (byte)Math.Round(Ginterpolated);
                B = (byte)Math.Round(Binterpolated);
            }
        }

        #endregion
    }
}
