using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

namespace UW.WPF
{
    /// <summary>
    /// Interaction logic for SliderDisplayControl.xaml
    /// </summary>
    public partial class SliderDisplayControl : UserControl
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public SliderDisplayControl()
        {
            InitializeComponent();
        }

        #region Dependency Properties

        /// <summary>
        /// SliderValueProperty
        /// </summary>
        public static readonly DependencyProperty SliderValueProperty = DependencyProperty.Register(
            "SliderValue", typeof(double), typeof(SliderDisplayControl), new PropertyMetadata(0.0));

        /// <summary>
        /// SliderValue
        /// </summary>
        [Bindable(true)]
        public double SliderValue
        {
            get { return (double)this.GetValue(SliderValueProperty); }
            set { this.SetValue(SliderValueProperty, value); }
        }

        /// <summary>
        /// MinimumProperty
        /// </summary>
        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(
            "Minimum", typeof(double), typeof(SliderDisplayControl), new PropertyMetadata(-10.0));

        /// <summary>
        /// Minimum
        /// </summary>
        [Bindable(true)]
        public double Minimum
        {
            get { return (double)this.GetValue(MinimumProperty); }
            set { this.SetValue(MinimumProperty, value); }
        }

        /// <summary>
        /// MaximumProperty
        /// </summary>
        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(
            "Maximum", typeof(double), typeof(SliderDisplayControl), new PropertyMetadata(10.0));

        /// <summary>
        /// Maximum
        /// </summary>
        [Bindable(true)]
        public double Maximum
        {
            get { return (double)this.GetValue(MaximumProperty); }
            set { this.SetValue(MaximumProperty, value); }
        }

        /// <summary>
        /// TickFrequencyProperty
        /// </summary>
        public static readonly DependencyProperty TickFrequencyProperty = DependencyProperty.Register(
            "TickFrequency", typeof(double), typeof(SliderDisplayControl), new PropertyMetadata(2.0));

        /// <summary>
        /// TickFrequency
        /// </summary>
        [Bindable(true)]
        public double TickFrequency
        {
            get { return (double)this.GetValue(TickFrequencyProperty); }
            set { this.SetValue(TickFrequencyProperty, value); }
        }

        #endregion
    }
}
