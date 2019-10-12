using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using UW.MathFunctions;
using UW.Utilities;

namespace UW.WPF
{
    #region Enums

    /// <summary>
    /// Where should the axis ticks be located
    /// </summary>
    public enum AxisTickLocation
    {
        /// <summary>
        /// Bottom
        /// </summary>
        Bottom,

        /// <summary>
        /// Left
        /// </summary>
        Left,

        /// <summary>
        /// Top
        /// </summary>
        Top,

        /// <summary>
        /// Right
        /// </summary>
        Right,

        /// <summary>
        /// Middle
        /// </summary>
        Middle
    }

    /// <summary>
    /// What type of marker is used
    /// </summary>
    public enum MarkerType
    {
        /// <summary>
        /// Circle
        /// </summary>
        Circle,

        /// <summary>
        /// Square
        /// </summary>
        Square,

        /// <summary>
        /// Cross
        /// </summary>
        Cross
    }

    #endregion



    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:UW.UWCanvas_NS"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:UW.UWCanvas_NS;assembly=UW.UWCanvas_NS"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     &lt;MyNamespace:CustomControl1/&gt;
    ///
    /// </summary>
    public class UWCanvas : Canvas
    {
        static UWCanvas()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(UWCanvas), new FrameworkPropertyMetadata(typeof(UWCanvas)));
        }

        #region Fields

        /// <summary>
        /// Minimum x value on the canvas
        /// </summary>
        private double _xMin;



        /// <summary>
        /// Maximum x value on the canvas
        /// </summary>
        private double _xMax;



        /// <summary>
        /// Minimum y value on the canvas
        /// </summary>
        private double _yMin;



        /// <summary>
        /// Maximum y value on the canvas
        /// </summary>
        private double _yMax;



        /// <summary>
        /// Approximate number of tick marks along the x axis
        /// </summary>
        private int _approximateNumAxisTicksX;



        /// <summary>
        /// Approximate number of tick marks along the y axis
        /// </summary>
        private int _approximateNumAxisTicksY;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public UWCanvas()
            : base()
        {
            //Version History:
            //07/02/12: Created
            //07/05/12: Updated
            //07/12/12: Updated with tick marks

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------
            this._xMin = -1;
            this._xMax = 1;
            this._yMin = -1;
            this._yMax = 1;
            this._approximateNumAxisTicksX = 10;
            this._approximateNumAxisTicksY = 7;
        }



        /// <summary>
        /// Construct from specified values
        /// </summary>
        /// <param name="xMin"></param>
        /// <param name="xMax"></param>
        /// <param name="yMin"></param>
        /// <param name="yMax"></param>
        public UWCanvas(double xMin, double xMax, double yMin, double yMax)
            : this()
        {
            //Version History:
            //07/02/12: Created
            //07/05/12: Updated

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------
            this._xMin = xMin;
            this._xMax = xMax;
            this._yMin = yMin;
            this._yMax = yMax;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Read only property denoting the width of the canvas in the same units as the yMin and yMax properties.
        /// </summary>
        public double CanvasHeight
        {
            //Version History:
            //07/10/12: Created

            get
            {
                return (this.yMax - this.yMin);
            }
        }



        /// <summary>
        /// Read only property denoting the width of the canvas in the same units as the xMin and xMax properties.
        /// </summary>
        public double CanvasWidth
        {
            //Version History:
            //07/10/12: Created

            get
            {
                return (this.xMax - this.xMin);
            }
        }
        

        
        /// <summary>
        /// Minimum x value of the canvas.
        /// </summary>
        public double xMin
        {
            //Version History:
            //07/05/12: Updated
            //07/10/12: Fixed bug

            get
            {
                return this._xMin;
            }

            set
            {
                if (value >= this._xMax)
                {
                    string errorString = "UWCanvas.set: xMin must be less than xMax.";
                    MessageBox.Show(errorString);
                    throw new ArgumentException(errorString);
                }
                else
                {
                    this._xMin = value;
                }
            }
        }



        /// <summary>
        /// Maximum x value of the canvas.
        /// </summary>
        public double xMax
        {
            //Version History:
            //07/05/12: Updated
            //07/10/12: Fixed bug

            get
            {
                return this._xMax;
            }

            set
            {
                if (value <= this._xMin)
                {
                    string errorString = "UWCanvas.set: xMax must be greater than xMin.";
                    MessageBox.Show(errorString);
                    throw new ArgumentException(errorString);
                }
                else
                {
                    this._xMax = value;
                }
            }
        }



        /// <summary>
        /// Minimum y value of the canvas.
        /// </summary>
        public double yMin
        {
            //Version History:
            //07/05/12: Updated
            //07/10/12: Fixed bug

            get
            {
                return this._yMin;
            }

            set
            {
                if (value >= this._yMax)
                {
                    string errorString = "UWCanvas.set: yMin must be less than yMax.";
                    MessageBox.Show(errorString);
                    throw new ArgumentException(errorString);
                }
                else
                {
                    this._yMin = value;
                }
            }
        }



        /// <summary>
        /// Maximum y value of the canvas.
        /// </summary>
        public double yMax
        {
            //Version History:
            //07/05/12: Updated
            //07/10/12: Fixed bug

            get
            {
                return this._yMax;
            }

            set
            {
                if (value <= this._yMin)
                {
                    string errorString = "UWCanvas.set: yMax must be greater than yMin.";
                    MessageBox.Show(errorString);
                    throw new ArgumentException(errorString);
                }
                else
                {
                    this._yMax = value;
                }
            }
        }



        /// <summary>
        /// Approximate number of tick marks in the x direction
        /// </summary>
        public int ApproximateNumAxisTicksX
        {
            //Version History:
            //07/12/12: Created

            get
            {
                return this._approximateNumAxisTicksX;
            }

            set
            {
                if (value < 2)
                {
                    throw new ArgumentException("UWCanvas.ApproximateNumAxisTicksX: should have two or more tick marks.");
                }
                else
                {
                    this._approximateNumAxisTicksX = value;
                }
            }
        }



        /// <summary>
        /// Approximate number of tick marks in the y direction
        /// </summary>
        public int ApproximateNumAxisTicksY
        {
            //Version History:
            //07/12/12: Created

            get
            {
                return this._approximateNumAxisTicksY;
            }

            set
            {
                if (value < 2)
                {
                    throw new ArgumentException("UWCanvas.ApproximateNumAxisTicksY: should have two or more tick marks.");
                }
                else
                {
                    this._approximateNumAxisTicksY = value;
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the x and y min/max boundaries of the UWCanvas object.  This function is useful to use (as opposed to setting individual properties line by line) because it will avoid insidious errors where the object may temporarily be in an unacceptable state.
        /// </summary>
        /// <param name="xMin"></param>
        /// <param name="xMax"></param>
        /// <param name="yMin"></param>
        /// <param name="yMax"></param>
        public void SetCanvasBoundaries(double xMin, double xMax, double yMin, double yMax)
        {
            //Version History:
            //07/05/12: Updated
            //07/09/12: Updated documentation
            //07/10/12: Updated documentation

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------
            if (xMin >= xMax)
            {
                string errorString = "UWCanvas.SetCanvasBoundaries: xMax must be greater than xMin.";
                MessageBox.Show(errorString);
                throw new ArgumentException(errorString);
            }
            
            if (yMin >= yMax)
            {
                string errorString = "UWCanvas.SetCanvasBoundaries: yMax must be greater than yMin.";
                MessageBox.Show(errorString);
                throw new ArgumentException(errorString);
            }

            //--------------Begin Calculations----------------
            //set the fields directly (if you set fields through the properties, you have to be careful about the order in which you set them otherwise it may throw an exception)
            this._xMin = xMin;
            this._xMax = xMax;
            this._yMin = yMin;
            this._yMax = yMax;
        }


        /// <summary>
        /// Sets the canvas boundaries to be slightly larger than the specified xMin, xMax, yMin, and yMax.  
        /// 
        /// The amount of "slightly larger" is determined by the xPercentage and yPercentage.  For example, xPercentage = 0.25 implies that the
        /// x axis should be approximaly 25% larger than the 
        /// 
        /// </summary>
        /// <param name="xMin"></param>
        /// <param name="xMax"></param>
        /// <param name="yMin"></param>
        /// <param name="yMax"></param>
        /// <param name="xPercentage"></param>
        /// <param name="yPercentage"></param>
        public void SetCanvasBoundariesSlightlyLarger(double xMin, double xMax, double yMin, double yMax, double xPercentage = 0.1, double yPercentage = 0.1)
        {
            //Version History:
            //07/12/12: Updated
            
            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------
            if ((xPercentage < 0) || (yPercentage < 0))
            {
                throw new ArgumentException("UWCanvas.SetCanvasBoundariesSlightlyLarger: xPercentage and yPercentage should be positive values.");
            }


            //--------------Begin Calculations----------------
            double Lx = xMax - xMin;
            double Ly = yMax - yMin;

            double extraX = Lx * xPercentage;
            double extraY = Ly * yPercentage;

            //set the fields directly (if you set fields through the properties, you have to be careful about the order in which you set them otherwise it may throw an exception)
            this._xMin = xMin - extraX / 2;
            this._xMax = xMax + extraX / 2;
            this._yMin = yMin - extraY / 2;
            this._yMax = yMax + extraY / 2;
        }


        /// <summary>
        /// Clear the canvas of all drawing.  Similar to Matlab's 'clf' command.
        /// </summary>
        public void ClearCanvas()
        {
            //Version History:
            //07/26/12: Created
            
            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------  
            this.Children.Clear();
        }



        /// <summary>
        /// Converts the x-coordinate (expressed in the UWCanvas frame) to number of pixels from the left of the canvas (origin at left and increasing to the right)
        /// </summary>
        /// <param name="x_uw"></param>
        /// <returns></returns>
        public double ConvertXCoordinateToPixelsFromLeft(double x_uw)
        {
            //Version History:
            //07/18/12: Created
            //07/19/12: Renamed method

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------  
            return this._convertXExpressedInFuwToXExpressedInFc(x_uw);
        }



        /// <summary>
        /// Converts the y-coordinate (expressed in the UWCanvas frame) to number of pixels from the top of the canvas (origin at top and increasing down)
        /// </summary>
        /// <param name="y_uw"></param>
        /// <returns></returns>
        public double ConvertYCoordinateToPixelsFromTop(double y_uw)
        {
            //Version History:
            //07/18/12: Created
            //07/19/12: Renamed method

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations---------------- 
            return this._convertYExpressedInFuwToYExpressedInFc(y_uw);
        }



        /// <summary>
        /// Converts a distance along the UWCanvas x axis to number of pixels.
        /// </summary>
        /// <param name="distance_x"></param>
        /// <returns></returns>
        public double ConvertXDistanceToPixels(double distance_x)
        {
            //Version History:
            //07/19/12: Created

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------
            return this._convertDistanceExpressedInFuwxUnitsToDistanceExpressedInFcmUnits(distance_x);
        }



        /// <summary>
        /// Converts a distance along the UWCanvas y axis to number of pixels.
        /// </summary>
        /// <param name="distance_y"></param>
        /// <returns></returns>
        public double ConvertYDistanceToPixels(double distance_y)
        {
            //Version History:
            //07/19/12: Created

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------
            return this._convertDistanceExpressedInFuwyUnitsToDistanceExpressedInFcmUnits(distance_y);
        }



        /// <summary>
        /// Attempt to force the UWCanvas to render itself when this method is called.
        /// 
        /// This attempts to replicate the functionality of Matlab's 'drawnow' command.
        /// 
        /// Code based on example shown in 'Update the WPF UI now: how to wait for the rendering to finish ?' located at http://www.jonathanantoine.com/2011/08/29/update-my-ui-now-how-to-wait-for-the-rendering-to-finish/
        /// 
        /// another example found in 'Refresh / Update WPF controls' article located at http://geekswithblogs.net/NewThingsILearned/archive/2008/08/25/refresh--update-wpf-controls.aspx
        /// </summary>
        public void DrawNow()
        {
            //Version History:
            //07/26/12: Created

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------
            //this.Dispatcher.Invoke(EmptyDelegate, System.Windows.Threading.DispatcherPriority.Render);

            Dispatcher.Invoke(new Action(() => { }), System.Windows.Threading.DispatcherPriority.ContextIdle, null);
        }



        /// <summary>
        /// Plot the data in the 1D matrices x and y on the UWCanvas.
        /// 
        /// The x and y values specified are in the UW frame (Fuw).
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="solidColorBrush"></param>
        /// <param name="strokeThickness"></param>
        public void Plot(UWMatrix x, UWMatrix y, SolidColorBrush solidColorBrush, double strokeThickness = 1.0)
        {
            //Version History:
            //07/10/12: Created
            //07/11/12: Fixed bug

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------
            //will be checked by UWFunctionsWPF.UWPlotXYExpressedInFcm() method
            
            //--------------Begin Calculations----------------  
            //convert the coordinates to F_cm
            UWMatrix x_cm = this._convertXExpressedInFuwToXExpressedInFcm(x);
            UWMatrix y_cm = this._convertYExpressedInFuwToYExpressedInFcm(y);            
            
            //plot this
            UWFunctionsWPF.PlotXYExpressedInFcm(x_cm, y_cm, this, solidColorBrush, strokeThickness);
        }



        /// <summary>
        /// Plots the data in the 1D matrices x and y on the UWCanvas.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Plot(UWMatrix x, UWMatrix y)
        {
            //Version History:
            //07/17/12: Created

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------
            //will be checked by other version of method

            //--------------Begin Calculations----------------
            this.Plot(x, y, Brushes.Blue);
        }



        /// <summary>
        /// Draw some grid lines on the canvas.  This method is more intelligent than the PlotGridLinesExact method as it tries to draw grid lines at intelligent locations.
        /// 
        /// The number of grid lines drawn are determined by the this.ApproximateNumAxisTicksX and this.ApproximateNumAxisTicksY properties.
        /// 
        /// If textLabels is true, then the grid line values will be printed next to the items.
        /// </summary>
        /// <param name="textLabels"></param>
        public void PlotGridLines(bool textLabels = false)
        {
            //Version History:
            //07/12/12: Created
            
            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations---------------- 
            //set some parameters for the grid lines
            SolidColorBrush gridLineBrush = Brushes.LightGray;
            double gridLineThickness = 1.0;

            //Get the deliniations in the x and y directions
            List<double> deliniationsX = UWFunctionsMath.DetermineNiceDeliniationsBetweenValues(this.xMin, this.xMax, this.ApproximateNumAxisTicksX);
            List<double> deliniationsY = UWFunctionsMath.DetermineNiceDeliniationsBetweenValues(this.yMin, this.yMax, this.ApproximateNumAxisTicksY);

            //draw the x grids (vertical lines)
            for (int k = 0; k < deliniationsX.Count; k++)
            {
                double x = deliniationsX[k];
                double y = this.yMin;

                UWMatrix verticalLine_x = new UWMatrix(2, 1);
                verticalLine_x[0, 0] = x;
                verticalLine_x[1, 0] = x;

                UWMatrix verticalLine_y = new UWMatrix(2, 1);
                verticalLine_y[0, 0] = this.yMin;
                verticalLine_y[1, 0] = this.yMax;

                this.Plot(verticalLine_x, verticalLine_y, gridLineBrush, gridLineThickness);

                if (textLabels)
                {
                    this._plotXAxisTickLabel(x);
                }
            }

            //draw the y grids (horizontal lines)
            for (int k = 0; k < deliniationsY.Count; k++)
            {
                double x = this.xMin;
                double y = deliniationsY[k];

                UWMatrix horizontalLine_x = new UWMatrix(2, 1);
                horizontalLine_x[0, 0] = this.xMin;
                horizontalLine_x[1, 0] = this.xMax;

                UWMatrix horizontalLine_y = new UWMatrix(2, 1);
                horizontalLine_y[0, 0] = y;
                horizontalLine_y[1, 0] = y;

                this.Plot(horizontalLine_x, horizontalLine_y, gridLineBrush, gridLineThickness);

                if (textLabels)
                {
                    this._plotYAxisTickLabel(y);
                }
            }
        }



        /// <summary>
        /// Draw some grid lines on the canvas.  
        /// 
        /// The number of grid lines drawn are determined by the this.ApproximateNumAxisTicksX and this.ApproximateNumAxisTicksY properties.
        /// 
        /// If textLabels is true, then the grid line values will be printed next to the items.
        /// </summary>
        /// <param name="textLabels"></param>
        public void PlotGridLinesExact(bool textLabels = false)
        {
            //Version History:
            //07/09/12: Updated
            //07/10/12: Continued working
            //07/11/12: Continued working and changed name
            //07/12/12: Changed to use the this.ApproximateNumAxisTicksX and this.ApproximateNumAxisTicksY properties

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------
            

            //--------------Begin Calculations----------------  
            //set some parameters for the grid lines
            SolidColorBrush gridLineBrush = Brushes.LightGray;
            double gridLineThickness = 1.0;
            
            //what is the appropriate deltaX and deltaY
            double deltaX = this.CanvasWidth / (double)this.ApproximateNumAxisTicksX;
            double deltaY = this.CanvasHeight / (double)this.ApproximateNumAxisTicksY;

            //draw the x grids (vertical lines)
            for (int k = 0; k < this.ApproximateNumAxisTicksX; k++)
            {
                double x = this.xMin + k*deltaX;
                  
                UWMatrix verticalLine_x = new UWMatrix(2, 1);
                verticalLine_x[0, 0] = x;
                verticalLine_x[1, 0] = x;

                UWMatrix verticalLine_y = new UWMatrix(2, 1);
                verticalLine_y[0, 0] = this.yMin;
                verticalLine_y[1, 0] = this.yMax;

                this.Plot(verticalLine_x, verticalLine_y, gridLineBrush, gridLineThickness);

                if (textLabels)
                {
                    this._plotXAxisTickLabel(x, AxisTickLocation.Bottom);
                }
            }


            //draw the y grids (horizontal lines)
            for (int k = 0; k < this.ApproximateNumAxisTicksY; k++)
            {
                 double y = this.yMin + k * deltaY;
                
                UWMatrix horizontalLine_x = new UWMatrix(2, 1);
                horizontalLine_x[0, 0] = this.xMin;
                horizontalLine_x[1, 0] = this.xMax;

                UWMatrix horizontalLine_y = new UWMatrix(2, 1);
                horizontalLine_y[0, 0] = y;
                horizontalLine_y[1, 0] = y;

                this.Plot(horizontalLine_x, horizontalLine_y, gridLineBrush, gridLineThickness);

                if (textLabels)
                {
                    this._plotYAxisTickLabel(y);
                }
            }
        }
        


        /// <summary>
        /// Plots the origin of the UWCanvas.
        /// </summary>
        public void PlotOrigin()
        {
            //Version History:
            //07/05/12: Updated
            //07/09/12: Updated documentation

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------
            

            //--------------Begin Calculations----------------            
            double x_origin_cm = this._convertXExpressedInFuwToXExpressedInFcm(0);
            double y_origin_cm = this._convertYExpressedInFuwToYExpressedInFcm(0);

            double width_pixels = 50;
            SolidColorBrush solidColorBrush = Brushes.Red;
            double strokeThickness = 5;

            //UWFunctionsWPF.PlotCrossExpressedInFcm(x_origin_cm, y_origin_cm, width_pixels, this, solidColorBrush, strokeThickness);
            UWFunctionsWPF.PlotCrossExpressedInFcm(x_origin_cm, y_origin_cm, width_pixels, this, solidColorBrush, strokeThickness);
        }



        /// <summary>
        /// Plot the specific type of marker on the canvas.  A marker is different from an equivalent shape (ie MarkerType.circle vs. PlotCircle) in the sense that
        /// markers have their size automatically calculated for the size of the canvas.  
        /// 
        /// The marker size is determined by markerSize, which is a percentage of the canvas width (ie markerSize=0.05) means the marker should take up 5% of the canvas width.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="markerType"></param>
        /// <param name="markerSize">in range [0,1] to specify percentage of canvas width that the marker takes up</param>
        /// <param name="path">Path object used to determine how marker is drawn.</param>
        public void PlotMarker(double x, double y, MarkerType markerType, double markerSize, Path path)
        {
            //Version History:
            //07/16/12: Created

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------
            double markerSizeSat = UWFunctionsMisc.Saturate(markerSize, 0, 1);


            //--------------Begin Calculations----------------
            if (markerType == MarkerType.Circle)
            {
                //What is the radius of the circle
                double radius = this.CanvasWidth * markerSizeSat / 2;
                this.PlotEllipseThatLooksLikeCircle(x, y, radius, path);
            }
            else if (markerType == MarkerType.Cross)
            {
                //what is the width of the cross?
                double width = this.CanvasWidth * markerSizeSat;

                //what is the height of the cross so that it looks like a square cross?
                double width_cm = this._convertDistanceExpressedInFuwxUnitsToDistanceExpressedInFcmUnits(width);    //pixels
                double height = this._convertDistanceExpressedInFcmUnitsToDistanceExpressedInFuwyUnits(width_cm);
                
                //draw the vertical piece
                UWMatrix xVertical = new UWMatrix(1, 2);
                xVertical[0] = x;
                xVertical[1] = x;

                UWMatrix yVertical = new UWMatrix(1, 2);
                yVertical[0] = y - height / 2;
                yVertical[1] = y + height / 2;

                this.Plot(xVertical, yVertical, (SolidColorBrush)path.Stroke, path.StrokeThickness);

                //draw the horizontal piece
                UWMatrix xHorizontal = new UWMatrix(1, 2);
                xHorizontal[0] = x - width / 2;
                xHorizontal[1] = x + width / 2;

                UWMatrix yHorizontal = new UWMatrix(1, 2);
                yHorizontal[0] = y;
                yHorizontal[1] = y;

                this.Plot(xHorizontal, yHorizontal, (SolidColorBrush)path.Stroke, path.StrokeThickness);
            }
            else if (markerType == MarkerType.Square)
            {
                //what is the width of the cross?
                double width = this.CanvasWidth * markerSizeSat;

                //what is the height of the cross so that it looks like a square cross?
                double width_cm = this._convertDistanceExpressedInFuwxUnitsToDistanceExpressedInFcmUnits(width);    //pixels
                double height = this._convertDistanceExpressedInFcmUnitsToDistanceExpressedInFuwyUnits(width_cm);

                //draw the square
                double xMin = x - width / 2;
                double xMax = x + width / 2;
                double yMin = y - height / 2;
                double yMax = y + height / 2;

                UWMatrix xPoints = new UWMatrix(5, 1);
                xPoints[0] = xMin;
                xPoints[1] = xMax;
                xPoints[2] = xMax;
                xPoints[3] = xMin;
                xPoints[4] = xMin;

                UWMatrix yPoints = new UWMatrix(5, 1);
                yPoints[0] = yMin;
                yPoints[1] = yMin;
                yPoints[2] = yMax;
                yPoints[3] = yMax;
                yPoints[4] = yMin;

                this.Plot(xPoints, yPoints, (SolidColorBrush)path.Stroke, path.StrokeThickness);
            }
            else
            {
                throw new ArgumentException("UWCanvas.PlotMarker:  Unsupported markerType.");
            }
            
        }



        /// <summary>
        /// Does the same as PlotMarker(double x, double y, MarkerType markerType, double markerSize, Path path) except it uses a standard Path object.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="markerType"></param>
        /// <param name="markerSize">in range [0,1] to specify percentage of canvas width that the marker takes up</param>
        public void PlotMarker(double x, double y, MarkerType markerType, double markerSize = 0.03)
        {
            //Version History:
            //07/25/12: Created

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------
            

            //--------------Begin Calculations----------------
            //use a standard Path object
            Path standardPath = new Path();
            standardPath.Stroke = Brushes.Blue;
            standardPath.StrokeThickness = 1;
            this.PlotMarker(x, y, markerType, markerSize, standardPath);
        }
        

        
        /// <summary>
        /// Plots an ellipse at the specified location.  
        /// 
        /// The semi-major and semi-minor axis of the ellipse is calculated so that this will look like a circle when rendered on the canvas.
        /// To do this, the radius specified is actually the semi-major axis of the ellipse (distance along the x-axis).
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius">The semi-major axis of the ellipse</param>
        /// <param name="path">Path object used to determine how circle is drawn.</param>
        public void PlotEllipseThatLooksLikeCircle(double x, double y, double radius, Path path)
        {
            //Version History:
            //07/16/12: Created

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------
            //convert the x and y to F_c coordinates
            double x_c = this._convertXExpressedInFuwToXExpressedInFc(x);
            double y_c = this._convertYExpressedInFuwToYExpressedInFc(y);

            //also convert the radius to distance in F_c coordinates
            double radius_x_c = this._convertDistanceExpressedInFuwxUnitsToDistanceExpressedInFcmUnits(radius);
            
            Point center_c = new Point(x_c, y_c);
            EllipseGeometry myEllipseGeometry = new EllipseGeometry(center_c, radius_x_c, radius_x_c);      //use x distance as both semi-major and semi-minor axis

            //use the path to draw the EllipseGeometry
            path.Data = myEllipseGeometry;

            //draw this on the canvas
            this.Children.Add(path);
        }




        /// <summary>
        /// Plots an ellipse at the specified location.  
        /// 
        /// The semi-major and semi-minor axis of the ellipse is calculated so that this will look like a circle when rendered on the canvas.
        /// To do this, the radius specified is actually the semi-major axis of the ellipse (distance along the x-axis).
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius"></param>
        public void PlotEllipseThatLooksLikeCircle(double x, double y, double radius)
        {
            //Version History:
            //07/12/12: Created

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------
            //use a standard Path object
            Path path = new Path();
            path.Stroke = Brushes.Black;
            path.StrokeThickness = 1;

            this.PlotEllipseThatLooksLikeCircle(x, y, radius, path);
        }



        /// <summary>
        /// Plots a circle at the specified location and radius.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius"></param>
        /// <param name="path">Path object used to determine how circle is drawn.</param>
        public void PlotCircle(double x, double y, double radius, Path path)
        {
            //Version History:
            //07/12/12: Created

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------
            //convert the x and y to F_c coordinates
            double x_c = this._convertXExpressedInFuwToXExpressedInFc(x);
            double y_c = this._convertYExpressedInFuwToYExpressedInFc(y);

            //also convert the radius to distance in F_c coordinates
            double radius_x_c = this._convertDistanceExpressedInFuwxUnitsToDistanceExpressedInFcmUnits(radius);
            double radius_y_c = this._convertDistanceExpressedInFuwyUnitsToDistanceExpressedInFcmUnits(radius);

            Point center_c = new Point(x_c, y_c);
            EllipseGeometry myEllipseGeometry = new EllipseGeometry(center_c, radius_x_c, radius_y_c);

            //use the path to draw the EllipseGeometry
            path.Data = myEllipseGeometry;

            //draw this on the canvas
            this.Children.Add(path);
        }



        /// <summary>
        /// Plots a circle at the specified location and radius.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius"></param>
        public void PlotCircle(double x, double y, double radius)
        {
            //Version History:
            //07/12/12: Created

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------
            //use a standard Path object
            Path path = new Path();
            path.Stroke = Brushes.Black;
            path.StrokeThickness = 1;

            this.PlotCircle(x, y, radius, path);
        }
        
        /// <summary>
        /// Plot the string (str) at the specified location (x,y).  The postion (x,y) denotes the top left corner of the textblock.
        /// </summary>
        /// <param name="x">x position expressed in F_uw</param>
        /// <param name="y">y position expressed in F_uw</param>
        /// <param name="str"></param>
        /// <param name="textBlock">object to use as a template for things like fontsize, family, etc.</param>
        public void Text(double x, double y, string str, TextBlock textBlock)
        {
            //Version History:
            //07/10/12: Created
            //07/16/12: Updated documentation

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------  
            //convert the coordinate in F_uw to F_c
            double x_cm = this._convertXExpressedInFuwToXExpressedInFcm(x);
            double y_cm = this._convertYExpressedInFuwToYExpressedInFcm(y);

            double x_c = x_cm;
            double y_c = UWFunctionsWPF.ConvertYExpressedInFcmToYExpressedInFc(y_cm, this.ActualHeight);

            //setup a TextBlock at the appropriate position
            textBlock.Text = str;            
            Canvas.SetLeft(textBlock, x_c);
            Canvas.SetTop(textBlock, y_c);
            this.Children.Add(textBlock);
        }



        /// <summary>
        /// Plot the string (str) at the specified location (x,y).  The postion (x,y) denotes the top left corner of the textblock.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="str"></param>
        public void Text(double x, double y, string str)
        {
            //Version History:
            //07/10/12: Created
            //07/16/12: Updated documentation

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------
            TextBlock textBlock = new TextBlock();
            this.Text(x, y, str, textBlock);
        }



        /// <summary>
        /// Add an x-axis label.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="textBlock"></param>
        public void XLabel(string str, TextBlock textBlock)
        {
            //Version History:
            //07/16/12: Created
            //07/17/12: Updated

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------
            //How tall is the textBlock (in pixels)?  Note that we cannot use the .ActualHeight property because the WPF layout engine has not assigned it a value yet so we estimate a value.
            double textBlockHeight_cm = 30;

            //convert this to UWCanvas units
            double textBlockHeight_uw = this._convertDistanceExpressedInFcmUnitsToDistanceExpressedInFuwyUnits(textBlockHeight_cm);
            double xOffset = this._convertDistanceExpressedInFcmUnitsToDistanceExpressedInFuwxUnits(2 * textBlockHeight_cm);
            
            double x = this.xMin + xOffset;
            double y = this.yMin + textBlockHeight_uw;
            
            this.Text(x, y, str, textBlock);
        }



        /// <summary>
        /// Add an x-axis label.
        /// </summary>
        /// <param name="str"></param>
        public void XLabel(string str)
        {
            //Version History:
            //07/16/12: Created

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------
            TextBlock textBlock = new TextBlock();
            this.XLabel(str, textBlock);
        }



        /// <summary>
        /// Add a y-axis label.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="textBlock"></param>
        public void YLabel(string str, TextBlock textBlock)
        {
            //Version History:
            //07/17/12: Created

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------
            //Rotate the textBlock so that the text is vertical
            RotateTransform rotatation = new RotateTransform(270);
            textBlock.RenderTransform = rotatation;
            
            //How tall is the textBlock (in pixels)?  Note that we cannot use the .ActualHeight property because the WPF layout engine has not assigned it a value yet so we estimate a value.
            double textBlockHeight_cm = 30;
            
            //convert this to UWCanvas units
            double textBlockHeight_uw = this._convertDistanceExpressedInFcmUnitsToDistanceExpressedInFuwxUnits(textBlockHeight_cm);
            double yOffset = this._convertDistanceExpressedInFcmUnitsToDistanceExpressedInFuwyUnits(1.5 * textBlockHeight_cm);

            double x = this.xMin + textBlockHeight_uw;
            double y = this.yMin + yOffset;

            this.Text(x, y, str, textBlock);
        }



        /// <summary>
        /// Add a y-axis label.
        /// </summary>
        /// <param name="str"></param>
        public void YLabel(string str)
        {
            //Version History:
            //07/17/12: Created

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------
            TextBlock textBlock = new TextBlock();
            this.YLabel(str, textBlock);
        }

        #endregion

        #region Private Methods

        ///// <summary>
        ///// Empty delegate which may be useful for some situations (such as within the UWCanvas.DrawNow method)
        ///// </summary>
        //private static Action EmptyDelegate = delegate() { };



        /// <summary>
        /// Convert x_uw (expressed in UWCanvas frame) to x_c (Canvas frame).
        /// </summary>
        /// <param name="x_uw"></param>
        /// <returns></returns>
        private double _convertXExpressedInFuwToXExpressedInFc(double x_uw)
        {
            //Version History:
            //07/12/12: Updated
            //07/18/12: Updated documentation
            
            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------  
            double x_cm = this._convertXExpressedInFuwToXExpressedInFcm(x_uw);
            double x_c = x_cm;      //x_c is the same as x_cm
            return x_c;
        }



        /// <summary>
        /// Convert y_uw (expressed in UWCanvas frame) to y_c (Canvas frame).
        /// </summary>
        /// <param name="y_uw"></param>
        /// <returns></returns>
        private double _convertYExpressedInFuwToYExpressedInFc(double y_uw)
        {
            //Version History:
            //07/12/12: Updated
            
            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------  
            double y_cm = this._convertYExpressedInFuwToYExpressedInFcm(y_uw);
            double y_c = UWFunctionsWPF.ConvertYExpressedInFcmToYExpressedInFc(y_cm, this.ActualHeight);
            return y_c;
        }



        /// <summary>
        /// Convert x_uw (expressed in UWCanvas frame) to x_cm (Canvas Modified frame).
        /// </summary>
        /// <param name="x_uw"></param>
        /// <returns></returns>
        private double _convertXExpressedInFuwToXExpressedInFcm(double x_uw)
        {
            //Version History:
            //07/05/12: Updated
            //07/10/12: Updated documentation

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------  
            double x_cm = ((x_uw - this.xMin) / (this.xMax - this.xMin)) * this.ActualWidth;
            return x_cm;
        }



        /// <summary>
        /// Convert x_uw (expressed in UWCanvas frame) to x_cm (Canvas Modified frame).
        /// </summary>
        /// <param name="x_uw"></param>
        /// <returns></returns>
        private UWMatrix _convertXExpressedInFuwToXExpressedInFcm(UWMatrix x_uw)
        {
            //Version History:
            //07/10/12: Created

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations---------------- 
            UWMatrix x_cm = new UWMatrix(x_uw);
            for (int m = 0; m < x_uw.NumRows; m++)
            {
                for (int n = 0; n < x_uw.NumColumns; n++)
                {
                    x_cm[m, n] = this._convertXExpressedInFuwToXExpressedInFcm(x_uw[m, n]);
                }
            }

            return x_cm;
        }


        /// <summary>
        /// Convert x_cm (Canvas Modified frame) to x_uw (expressed in UWCanvas frame).
        /// </summary>
        /// <param name="x_cm"></param>
        /// <returns></returns>
        private double _convertXExpressedInFcmToXExpressedInFuw(double x_cm)
        {
            //Version History:
            //07/10/12: Created

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------  
            double x_percentage = x_cm / this.ActualWidth;
            double x_uw = x_percentage * this.CanvasWidth + this.xMin;
            return x_uw;
        }




        /// <summary>
        /// Convert x_cm (Canvas Modified frame) to x_uw (expressed in UWCanvas frame).
        /// </summary>
        /// <param name="x_cm"></param>
        /// <returns></returns>
        private UWMatrix _convertXExpressedInFcmToXExpressedInFuw(UWMatrix x_cm)
        {
            //Version History:
            //07/10/12: Created

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations---------------- 
            UWMatrix x_uw = new UWMatrix(x_cm);
            for (int m = 0; m < x_cm.NumRows; m++)
            {
                for (int n = 0; n < x_cm.NumColumns; n++)
                {
                    x_uw[m, n] = this._convertXExpressedInFcmToXExpressedInFuw(x_cm[m, n]);
                }
            }

            return x_uw;
        }



        /// <summary>
        /// Convert y_uw (expressed in UWCanvas frame) to y_cm (Canvas Modified frame).
        /// </summary>
        /// <param name="y_uw"></param>
        /// <returns></returns>
        private double _convertYExpressedInFuwToYExpressedInFcm(double y_uw)
        {
            //Version History:
            //07/05/12: Updated
            //07/10/12: Updated documentation

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------  
            double y_cm = ((y_uw - this.yMin) / (this.yMax - this.yMin)) * this.ActualHeight;
            return y_cm;
        }



        /// <summary>
        /// Convert y_uw (expressed in UWCanvas frame) to y_cm (Canvas Modified frame).
        /// </summary>
        /// <param name="y_uw"></param>
        /// <returns></returns>
        private UWMatrix _convertYExpressedInFuwToYExpressedInFcm(UWMatrix y_uw)
        {
            //Version History:
            //07/10/12: Created

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations---------------- 
            UWMatrix y_cm = new UWMatrix(y_uw);
            for (int m = 0; m < y_uw.NumRows; m++)
            {
                for (int n = 0; n < y_uw.NumColumns; n++)
                {
                    y_cm[m, n] = this._convertYExpressedInFuwToYExpressedInFcm(y_uw[m, n]);
                }
            }

            return y_cm;
        }



        /// <summary>
        /// Convert y_cm (Canvas Modified frame) to y_uw (expressed in UWCanvas frame).
        /// </summary>
        /// <param name="y_cm"></param>
        /// <returns></returns>
        private double _convertYExpressedInFcmToYExpressedInFuw(double y_cm)
        {
            {
                //Version History:
                //07/10/12: Created

                //Create local copies of input arguments as needed

                //------------Checking Input Arguments------------
                

                //--------------Begin Calculations----------------  
                double y_percentage = y_cm / this.ActualHeight;
                double y_uw = y_percentage * this.CanvasHeight + this.yMin;
                return y_uw;
            }
        }




        /// <summary>
        /// Convert y_cm (Canvas Modified frame) to y_uw (expressed in UWCanvas frame).
        /// </summary>
        /// <param name="y_cm"></param>
        /// <returns></returns>
        private UWMatrix _convertYExpressedInFcmToYExpressedInFuw(UWMatrix y_cm)
        {
            //Version History:
            //07/10/12: Created

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations---------------- 
            UWMatrix y_uw = new UWMatrix(y_cm);
            for (int m = 0; m < y_cm.NumRows; m++)
            {
                for (int n = 0; n < y_cm.NumColumns; n++)
                {
                    y_uw[m, n] = this._convertYExpressedInFcmToYExpressedInFuw(y_cm[m, n]);
                }
            }

            return y_uw;
        }
        


        /// <summary>
        /// Convert a distance which is expressed in the F_cm units (effectively pixels) to units expressed in F_uw x-axis units (whatever units xMin and xMax use).
        /// </summary>
        /// <param name="distance_cm"></param>
        /// <returns></returns>
        private double _convertDistanceExpressedInFcmUnitsToDistanceExpressedInFuwxUnits(double distance_cm)
        {
            //Version History:
            //07/13/12: Created

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------
            //what percentage of the canvas width is the distance?
            double percentage = distance_cm / this.ActualWidth;
            
            //convert this
            double distance_uwx = percentage * this.CanvasWidth;

            return distance_uwx;
        }



        /// <summary>
        /// Convert a distance which is expressed in the F_cm units (effectively pixels) to units expressed in F_uw y-axis units (whatever units yMin and yMax use).
        /// </summary>
        /// <param name="distance_cm"></param>
        /// <returns></returns>
        private double _convertDistanceExpressedInFcmUnitsToDistanceExpressedInFuwyUnits(double distance_cm)
        {
            //Version History:
            //07/13/12: Created

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------
            //what percentage of the canvas width is the distance?
            double percentage = distance_cm / this.ActualHeight;

            //convert this
            double distance_uwy = percentage * this.CanvasHeight;

            return distance_uwy;
        }



        /// <summary>
        /// Convert a distance in F_uw x-axis units (whatever units xMin and xMax use) to a distance expressed in the F_cm units (effectively pixels).
        /// </summary>
        /// <param name="distance_uwx"></param>
        /// <returns></returns>
        private double _convertDistanceExpressedInFuwxUnitsToDistanceExpressedInFcmUnits(double distance_uwx)
        {
            //Version History:
            //07/13/12: Created

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------
            //What percentage of the canvas width is this distance?
            double percentage = distance_uwx / this.CanvasWidth;

            //convert this
            double distance_cm = percentage * this.ActualWidth;
            return distance_cm;
        }



        /// <summary>
        /// Convert a distance in F_uw y-axis units (whatever units yMin and yMax use) to a distance expressed in the F_cm units (effectively pixels).
        /// </summary>
        /// <param name="distance_uwy"></param>
        /// <returns></returns>
        private double _convertDistanceExpressedInFuwyUnitsToDistanceExpressedInFcmUnits(double distance_uwy)
        {
            //Version History:
            //07/13/12: Created

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------
            //What percentage of the canvas width is this distance?
            double percentage = distance_uwy / this.CanvasHeight;

            //convert this
            double distance_cm = percentage * this.ActualHeight;
            return distance_cm;
        }



        /// <summary>
        /// Plot a x axis axis tick at the specified location.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="textBlock"></param>
        /// <param name="axisTickLocation"></param>
        private void _plotXAxisTickLabel(double x, TextBlock textBlock, AxisTickLocation axisTickLocation = AxisTickLocation.Bottom)
        {
            //Version History:
            //07/12/12: Created

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------
            

            //--------------Begin Calculations----------------
            //what is the approriate y location
            double y = 0;
            switch (axisTickLocation)
            {
                case AxisTickLocation.Bottom:
                    //How tall is the textBlock (in pixels)?  Note that we cannot use the .ActualHeight property because the WPF layout engine has not assigned it a value yet so we estimate a value.
                    double textBlockHeight_cm = 15;

                    //convert this to UWCanvas units
                    double textBlockHeight_uw = this._convertDistanceExpressedInFcmUnitsToDistanceExpressedInFuwyUnits(textBlockHeight_cm);

                    y = this.yMin + textBlockHeight_uw;
                    break;

                case AxisTickLocation.Left:
                    throw new ArgumentException("UWCanvas._plotXAxisTickLabel:  axisTickLocation selection does not make sense in this context.");

                case AxisTickLocation.Top:
                    y = this.yMax;
                    break;

                case AxisTickLocation.Right:
                    throw new ArgumentException("UWCanvas._plotXAxisTickLabel:  axisTickLocation selection does not make sense in this context.");

                case AxisTickLocation.Middle:
                    y = this.CanvasHeight / 2 + this.yMin;
                    break;

                default:
                    throw new ArgumentException("UWCanvas._plotXAxisTickLabel:  Unsupported axisTickLocation.");
            }

            Text(x, y, x.ToString(), textBlock);                    
        }



        /// <summary>
        /// Plot a x axis axis tick at the specified location. 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="axisTickLocation"></param>
        private void _plotXAxisTickLabel(double x, AxisTickLocation axisTickLocation = AxisTickLocation.Bottom)
        {
            //Version History:
            //07/12/12: Created

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------
            //use a default textblock
            TextBlock textBlock = new TextBlock();
            this._plotXAxisTickLabel(x, textBlock, axisTickLocation);
        }



        /// <summary>
        /// Plot a x axis axis tick at the specified location.
        /// </summary>
        /// <param name="y"></param>
        /// <param name="textBlock"></param>
        /// <param name="axisTickLocation"></param>
        private void _plotYAxisTickLabel(double y, TextBlock textBlock, AxisTickLocation axisTickLocation = AxisTickLocation.Left)
        {
            //Version History:
            //07/12/12: Created

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------
            //what is the approriate x location
            double x = 0;
            switch (axisTickLocation)
            {
                case AxisTickLocation.Bottom:
                    throw new ArgumentException("UWCanvas._plotYAxisTickLabel:  axisTickLocation selection does not make sense in this context.");

                case AxisTickLocation.Left:
                    x = this.xMin;
                    break;

                case AxisTickLocation.Top:
                    throw new ArgumentException("UWCanvas._plotYAxisTickLabel:  axisTickLocation selection does not make sense in this context.");

                case AxisTickLocation.Right:
                    //note that this is more difficult to accurately estimate the width of the textblock so let's just leave it as being plotted at xMax
                    x = this.xMax;      
                    break;

                case AxisTickLocation.Middle:
                    x = this.CanvasWidth / 2 + this.xMin;
                    break;

                default:
                    throw new ArgumentException("UWCanvas._plotYAxisTickLabel:  Unsupported axisTickLocation.");
            }

            Text(x, y, y.ToString(), textBlock);
        }



        /// <summary>
        /// Plot a x axis axis tick at the specified location. 
        /// </summary>
        /// <param name="y"></param>
        /// <param name="axisTickLocation"></param>
        private void _plotYAxisTickLabel(double y, AxisTickLocation axisTickLocation = AxisTickLocation.Left)
        {
            //Version History:
            //07/12/12: Created

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------
            //use a default textblock
            TextBlock textBlock = new TextBlock();
            this._plotYAxisTickLabel(y, textBlock, axisTickLocation);
        }

        ///// <summary>
        ///// Determines deliniations which are approximately round numbers and within the range [valMin, valMax]  which would be suitable for plotting.
        ///// 
        ///// For example, if the range of numbers is between [-11.4, 2.23] and numDeliniations = 7, this function might compute deliniations of [-10 -8 -6 -4 -2 0 2].
        ///// </summary>
        ///// <param name="valMin"></param>
        ///// <param name="valMax"></param>
        ///// <param name="numDeliniations"></param>
        ///// <returns></returns>
        //private UWMatrix _determineRoundDeliniationsBetweenValues(double valMin, double valMax, int numDeliniations = 10)
        //{
        //    //Version History:
        //    //07/11/12: Created
        //    //07/12/12: 

        //    //Create local copies of input arguments as needed

        //    //------------Checking Input Arguments------------


        //    //--------------Begin Calculations---------------- 
        //    throw new NotImplementedException("START HERE!!!");

        //}

        #endregion
    }
}
