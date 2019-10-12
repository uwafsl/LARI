using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using UW.MathFunctions;

namespace UW.WPF
{
    /// <summary>
    /// A class to provide functionality when interacting with view objects in WPF.
    /// 
    /// These functions are intended to be used with WPF views.  As a consequence of this,
    /// 
    ///     -some errors are printed to a MessageBox which makes it difficult to unit test some functionality.
    /// </summary>
    public class UWFunctionsWPF
    {
        /// <summary>
        /// Convert the specified y_c (y value expressed in the canvas frame which has origin at top left with positive y pointing down) to a y_cm value (y value expressed in the modified canvas frame which has its origin at the bottom left with positive y pointing up).
        /// </summary>
        /// <param name="y_c">y distance in pixels</param>
        /// <param name="canvasHeightPixels">canvas height in pixels</param>
        /// <returns></returns>
        public static double ConvertYExpressedInFcToYExpressedInFcm(double y_c, double canvasHeightPixels)
        {
            //Version History:
            //06/27/12: Updated
            //07/05/12: Renamed to be more specific
            //07/10/12: Updated documentation

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------
            double y_cm = canvasHeightPixels - y_c;
            return y_cm;
        }



        /// <summary>
        /// Convert the specified y_cm (y value expressed in the modified canvas frame which has its origin at the bottom left with positive y pointing up) to a y_c value (y value expressed in the canvas frame which has origin at top left with positive y pointing down) .
        /// </summary>
        /// <param name="y_cm">y distance in pixels</param>
        /// <param name="canvasHeightPixels">canvas height in pixels</param>
        /// <returns></returns>
        public static double ConvertYExpressedInFcmToYExpressedInFc(double y_cm, double canvasHeightPixels)
        {
            //Version History:
            //07/10/12: Created

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------


            //--------------Begin Calculations----------------
            double y_c = canvasHeightPixels - y_cm;
            return y_c;
        }
        


        /// <summary>
        /// Draw a cross at the specified location
        /// </summary>
        /// <param name="xCenter_cm"></param>
        /// <param name="yCenter_cm"></param>
        /// <param name="width_pixels"></param>
        /// <param name="myCanvas"></param>
        /// <param name="solidColorBrush"></param>
        /// <param name="strokeThickness"></param>
        public static void PlotCrossExpressedInFcm(double xCenter_cm, double yCenter_cm, double width_pixels, Canvas myCanvas, SolidColorBrush solidColorBrush, double strokeThickness = 1.0)
        {
            //Version History:
            //06/28/12: Created
            //07/05/12: Changed function name

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------
            if(width_pixels <= 0)
            {
                string errorString = "UWFunctionsWPF.PlotCrossExpressedInFcm: width_pixels should be greater than zero.";
                MessageBox.Show(errorString);
                throw new ArgumentException(errorString);
            }


            //--------------Begin Calculations----------------
            //draw the vertical piece
            UWMatrix xVertical = new UWMatrix(1, 2);
            xVertical[0] = xCenter_cm;
            xVertical[1] = xCenter_cm;

            UWMatrix yVertical = new UWMatrix(1, 2);
            yVertical[0] = yCenter_cm - width_pixels / 2;
            yVertical[1] = yCenter_cm + width_pixels / 2;

            UWFunctionsWPF.PlotXYExpressedInFcm(xVertical, yVertical, myCanvas, solidColorBrush, strokeThickness);

            //draw the horizontal piece
            UWMatrix xHorizontal = new UWMatrix(1, 2);
            xHorizontal[0] = xCenter_cm - width_pixels/2;
            xHorizontal[1] = xCenter_cm + width_pixels/2;

            UWMatrix yHorizontal = new UWMatrix(1, 2);
            yHorizontal[0] = yCenter_cm;
            yHorizontal[1] = yCenter_cm;

            UWFunctionsWPF.PlotXYExpressedInFcm(xHorizontal, yHorizontal, myCanvas, solidColorBrush, strokeThickness);
        }



        /// <summary>
        /// Plot the data in the 1D matrices x_cm and y_cm on the specified Canvas.
        /// 
        /// The x_cm and y_cm values specified are in the modified canvas frame (Fcm) where the origin is at the bottom left corner with positive x_cm pointing towards the right and positive y_cm pointing up.
        /// </summary>
        /// <param name="x_cm"></param>
        /// <param name="y_cm"></param>
        /// <param name="myCanvas"></param>
        /// <param name="solidColorBrush"></param>
        /// <param name="strokeThickness"></param>
        public static void PlotXYExpressedInFcm(UWMatrix x_cm, UWMatrix y_cm, Canvas myCanvas, SolidColorBrush solidColorBrush, double strokeThickness = 1.0)
        {
            //Version History:
            //06/27/12: Created
            //06/28/12: Continued working
            //07/02/12: Updated documentation
            //07/05/12: Renamed function to be more specific

            //Create local copies of input arguments as needed

            //------------Checking Input Arguments------------
            if (UWMatrix.Length(x_cm) != UWMatrix.Length(y_cm))
            {
                string errorString = "UWFunctionsWPF.PlotXYExpressedInFcm: x_cm and y_cm are not the same length.";
                MessageBox.Show(errorString);
                throw new ArgumentException(errorString);
            }


            if (UWMatrix.Length(x_cm) == 1)
            {
                //we can't plot a single point
                return;
            }


            //--------------Begin Calculations----------------
            //What is the canvasHeight of the canvas
            double canvasHeight = myCanvas.ActualHeight;

            //set the first point
            PathFigure myPathFigure = new PathFigure();
            myPathFigure.StartPoint = new Point(x_cm[0], UWFunctionsWPF.ConvertYExpressedInFcToYExpressedInFcm(y_cm[0], canvasHeight));

            //draw lines between the consequetive points
            bool isStroked = true;
            for (int i = 1; i < UWMatrix.Length(x_cm); i++)
            {
                Point point = new Point(x_cm[i], UWFunctionsWPF.ConvertYExpressedInFcToYExpressedInFcm(y_cm[i], canvasHeight));
                LineSegment currentSegment = new LineSegment(point, isStroked);
                myPathFigure.Segments.Add(currentSegment);
            }

            //create a PathGeometry to contain the PathFigure
            PathGeometry myPathGeometry = new PathGeometry();
            myPathGeometry.Figures.Add(myPathFigure);

            //create a Path to contain the PathGeometry
            Path myPath = new Path();
            myPath.Data = myPathGeometry;

            //color the path
            myPath.Stroke = solidColorBrush;
            myPath.StrokeThickness = strokeThickness;

            //draw this on the canvas
            myCanvas.Children.Add(myPath);
        }
        
    }
}
