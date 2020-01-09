using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Tooltip_FastLineBitmapSeries
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ChartDataPointInfo dataPointInfo = new ChartDataPointInfo();
        double x, y, stackY;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Chart_MouseMove(object sender, MouseEventArgs e)
        {
            var chart = (sender as SfChart);
            foreach (CustomFastLineBitmapSeries series in chart.Series)
            {
                if (series.ShowTooltip && series.IsHitTestSeries())
                {
                    // Finding the nearest chart data for the mouse point.
                    series.FindNearestChartPoint(e.GetPosition(series.SeriesPanel), out x, out y, out stackY);
                    dataPointInfo = new ChartDataPointInfo();
                    dataPointInfo.XData = x;
                    dataPointInfo.YData = y;

                    // Setting the internal mouse position for the series.
                    series.SetMousePoint(e.GetPosition(Chart.AdorningCanvas));

                    // Invoking the tooltip by passing the nearest data.
                    series.UpdateSeriesTooltip(dataPointInfo);
                }
            }
        }
    }

    public class Model
    {
        public double XValue { get; set; }
        public double YValue { get; set; }
    }

    public class ViewModel
    {
        public ViewModel()
        {
            GenerateData();
        }

        public void GenerateData()
        {
            Data = new ObservableCollection<Model>();
            Random rd = new Random();
            for (int i = 0; i < 30; i++)
            {
                Data.Add(new Model()
                {
                    XValue = i,
                    YValue = rd.Next(0, 50)
                });
            }
        }

        private ObservableCollection<Model> data;

        public ObservableCollection<Model> Data
        {
            get { return data; }
            set { data = value; }
        }

    }

    public class ChartDataPointInfo : ChartSegment
    {
        public double XData { get; set; }
        public double YData { get; set; }

        public override UIElement CreateVisual(Size size)
        {
            throw new NotImplementedException();
        }

        public override UIElement GetRenderedVisual()
        {
            throw new NotImplementedException();
        }

        public override void OnSizeChanged(Size size)
        {
            throw new NotImplementedException();
        }

        public override void Update(IChartTransformer transformer)
        {
            throw new NotImplementedException();
        }
    }
}
