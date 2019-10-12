using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

using UW.MathFunctions.GeometryFunctions;
using UW.MathFunctions;

namespace UW.WPF
{
    /// <summary>
    /// A class to plot on a Canvas.  Note that this is a general purpose plotter which is not specific to any application.
    /// </summary>
    public class CanvasPlotter
    {
        #region Enums

        /// <summary>
        /// Different types of symbol that can be used
        /// </summary>
        public enum PlotSymbols
        {
            /// <summary>
            /// Circle
            /// </summary>
            Circle,

            /// <summary>
            /// Cross or x
            /// </summary>
            X,

            /// <summary>
            /// Triangle
            /// </summary>
            Triangle
        }

        #endregion

        #region Fields

        private Canvas plotHost;

        #region Settings

        private Brush stroke;
        private int strokeThickness;
        private double symbolDiameter;

        #region Text
        
        private double fontSize;
        private FontFamily font;
        private Brush fontColor;

        #endregion

        #endregion

        #endregion

        #region Constructors

        private CanvasPlotter()
        {
            //we want to force user to specify an elementToDrawOn so we prevent the default constructor
        }

        /// <summary>
        /// Construct a plotter which can plot objects on the specified Canvas element.
        /// </summary>
        /// <param name="elementToDrawOn"></param>
        public CanvasPlotter(Canvas elementToDrawOn)
        {
            this.plotHost = elementToDrawOn;
            this.ResetSettings();
        }

        #endregion

        #region Properties

        /// <summary>
        /// The Canvas object which is actually doing the plotting
        /// </summary>
        public Canvas PlotHost
        {
            get
            {
                return this.plotHost;
            }
        }

        #region Settings

        /// <summary>
        /// The brush used to plot.
        /// </summary>
        public Brush Stroke
        {
            get
            {
                return this.stroke;
            }

            set
            {
                this.stroke = value;
            }
        }

        /// <summary>
        /// Thickness of the line to plot, in pixels.
        /// </summary>
        public int StrokeThickness
        {
            get
            {
                return this.strokeThickness;
            }

            set
            {
                if (value > 0)
                {
                    this.strokeThickness = value;
                }
            }
        }

        /// <summary>
        /// The diameter of the sybols to plot (x, circle, square, etc.), in pixels.
        /// </summary>
        public double SymbolDiameter
        {
            get
            {
                return this.symbolDiameter;
            }

            set
            {
                if (value > 0)
                {
                    this.symbolDiameter = value;
                }
            }
        }

        /// <summary>
        /// The size of the font to use when plotting text
        /// </summary>
        public double FontSize
        {
            get
            {
                return this.fontSize;
            }

            set
            {
                if (value > 0)
                {
                    this.fontSize = value;
                }
            }
        }

        /// <summary>
        /// The family of the font to use when plotting text
        /// </summary>
        public FontFamily Font
        {
            get
            {
                return this.font;
            }

            set
            {
                this.font = value;
            }
        }

        /// <summary>
        /// The color of the font for text plotting
        /// </summary>
        public Brush FontColor
        {
            get
            {
                return this.fontColor;
            }

            set
            {
                this.fontColor = value;
            }
        }

        #endregion

        #endregion

        #region Drawing Methods

        #region Public Methods

        /// <summary>
        /// Clear the plot
        /// </summary>
        public void Clear()
        {
            this.PlotHost.Children.Clear();
        }

        /// <summary>
        /// Plots the pathGeometry as a line
        /// </summary>
        /// <param name="pathGeometry"></param>
        public void Plot(PathGeometry pathGeometry)
        {
            Path myPath = new Path();
            this.setPathToCurrentSettings(myPath);
            myPath.Data = pathGeometry;

            //draw this on the canvas
            this.PlotHost.Children.Add(myPath);
        }

        /// <summary>
        /// Plots the list of Point objects as a line
        /// </summary>
        /// <param name="points"></param>
        public void Plot(List<Point> points)
        {
            this.Plot(points.ToPathGeometry());
        }

        /// <summary>
        /// Plots the list of Point objects as symbols
        /// </summary>
        /// <param name="points"></param>
        /// <param name="symbol"></param>
        public void Plot(List<Point> points, PlotSymbols symbol)
        {
            foreach (Point pt in points)
            {
                this.PlotSymbol(pt, symbol);
            }
        }

        /// <summary>
        /// Plots the LineGeometry as a line
        /// </summary>
        /// <param name="lineGeometry"></param>
        public void Plot(LineGeometry lineGeometry)
        {
            List<Point> points = new List<Point>()
            {
                lineGeometry.StartPoint,
                lineGeometry.EndPoint
            };

            this.Plot(points);
        }

        /// <summary>
        /// Plot the point using the desired symbol
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="symbol"></param>
        public void PlotSymbol(Point pt, PlotSymbols symbol)
        {
            Path myPath = new Path();
            this.setPathToCurrentSettings(myPath);

            //set the myPath.Data as apporpriate for the desired symbol
            if (symbol == PlotSymbols.Circle)
            {
                EllipseGeometry circle = new EllipseGeometry()
                {
                    Center = pt,
                    RadiusX = this.SymbolDiameter / 2,
                    RadiusY = this.symbolDiameter / 2
                };

                myPath.Data = circle;
            }
            else if (symbol == PlotSymbols.X)
            {
                double L = (this.SymbolDiameter / 2) * Math.Cos(Math.PI / 4);

                //add the segments to a PathFigure object
                PathFigure myPathFigure = new PathFigure();

                //set start point to minimum x and maximum y
                myPathFigure.StartPoint = new Point(pt.X - L, pt.Y + L);

                //draw diagonal to the maximum x and minimum y
                LineSegment lineSegment1 = new LineSegment(new Point(pt.X + L, pt.Y - L), isStroked: true);
                myPathFigure.Segments.Add(lineSegment1);

                //pick up pencil and move to the maximum x and maximum y (don't stroke the line)
                LineSegment lineSegment2 = new LineSegment(new Point(pt.X + L, pt.Y + L), isStroked: false);
                myPathFigure.Segments.Add(lineSegment2);

                //draw diagonal to the minimum x and minimum y
                LineSegment lineSegment3 = new LineSegment(new Point(pt.X - L, pt.Y - L), isStroked: true);
                myPathFigure.Segments.Add(lineSegment3);

                //create a PathGeometry to contain the PathFigure
                PathGeometry myPathGeometry = new PathGeometry();
                myPathGeometry.Figures.Add(myPathFigure);

                myPath.Data = myPathGeometry;
            }
            else if (symbol == PlotSymbols.Triangle)
            {
                double x = (this.SymbolDiameter / 2) * Math.Cos(UWFunctionsMath.DegreesToRadians(30));
                double y = (this.SymbolDiameter / 2) * Math.Sin(UWFunctionsMath.DegreesToRadians(30));

                //add the segments to a PathFigure object
                PathFigure myPathFigure = new PathFigure();

                //set start point to 0 x and maximum y
                myPathFigure.StartPoint = new Point(pt.X + 0, pt.Y + (this.SymbolDiameter / 2));

                //draw diagonal to the maximum x and minimum y
                LineSegment lineSegment1 = new LineSegment(new Point(pt.X + x, pt.Y - y), isStroked: true);
                myPathFigure.Segments.Add(lineSegment1);

                //draw diagonal to the minimum x and minimum y
                LineSegment lineSegment2 = new LineSegment(new Point(pt.X - x, pt.Y - y), isStroked: true);
                myPathFigure.Segments.Add(lineSegment2);

                //draw diagonal to the 0 x and maximum y
                LineSegment lineSegment3 = new LineSegment(new Point(pt.X, pt.Y + (this.SymbolDiameter / 2)), isStroked: true);
                myPathFigure.Segments.Add(lineSegment3);
                
                //create a PathGeometry to contain the PathFigure
                PathGeometry myPathGeometry = new PathGeometry();
                myPathGeometry.Figures.Add(myPathFigure);

                myPath.Data = myPathGeometry;
            }
            else
            {
                throw new NotImplementedException("Unsupported symbol");
            }
            
            //draw this on the canvas
            this.PlotHost.Children.Add(myPath);
        }

        /// <summary>
        /// Plots the specified text at the desired location
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="text"></param>
        public void PlotText(Point pt, string text)
        {
            Label label = new Label();
            label.Content = text;
            label.FontSize = this.FontSize;
            label.FontFamily = this.Font;
            label.Foreground = this.FontColor;

            Canvas.SetLeft(label, pt.X);
            Canvas.SetTop(label, pt.Y);

            this.PlotHost.Children.Add(label);
        }

        #endregion

        #endregion

        #region Plot Settings

        #region Public Methods

        /// <summary>
        /// Reset all the plotting settings to their default values.
        /// </summary>
        public void ResetSettings()
        {
            this.stroke = Brushes.Blue;
            this.strokeThickness = 2;
            this.symbolDiameter = 10;
            this.fontSize = 10;
            this.font = new FontFamily("Times New Roman");
            this.fontColor = Brushes.Black;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets the properties on the specified Path object to the current settings of this object.
        /// </summary>
        /// <param name="path"></param>
        private void setPathToCurrentSettings(Path path)
        {
            path.Stroke = this.Stroke;
            path.StrokeThickness = this.StrokeThickness;
        }

        #endregion

        #endregion
    }
}
