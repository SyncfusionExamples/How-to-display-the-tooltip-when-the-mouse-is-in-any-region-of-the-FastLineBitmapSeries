using Syncfusion.UI.Xaml.Charts;
using System.Reflection;
using System.Windows;

namespace Tooltip_FastLineBitmapSeries
{
    public class CustomFastLineBitmapSeries : FastLineBitmapSeries
    {
        // This is the MethodInfo to get the IsHitTestMethod. IsHitTestMethod Checks whether the mouse is over the series.
        public MethodInfo IsHitTestMethodInfo { get; set; }

        // This is a reference panel to get the nearest chart data point.
        public ChartSeriesPanel SeriesPanel { get; set; }

        // This fieldinfo helps us to update mouse point.
        private FieldInfo mousePointInfo;

        // This is the MethodInfo to get the UpdateSeriesTooltip method. UpdateSeriesTooltip method updates the tooltip according to the data point provided.
        private MethodInfo updateSeriesTooltipMethodInfo;

        public CustomFastLineBitmapSeries()
        {
            mousePointInfo = this.GetType().GetField("mousePos", BindingFlags.NonPublic | BindingFlags.Instance);
            IsHitTestMethodInfo = this.GetType().GetMethod("IsHitTestSeries", BindingFlags.NonPublic | BindingFlags.Instance);
            updateSeriesTooltipMethodInfo = this.GetType().GetMethod("UpdateSeriesTooltip", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        // This method is overriden to get the SeriesPanel.
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            SeriesPanel = this.GetTemplateChild("seriesPanel") as ChartSeriesPanel;
        }

        // Checks whether the mouse has hit the series.
        public bool IsHitTestSeries()
        {
            return (bool)IsHitTestMethodInfo?.Invoke(this, null);
        }

        // Updates the mouse point when it is moved.
        public void SetMousePoint(Point point)
        {
            mousePointInfo?.SetValue(this, point);
        }

        // Updates the tooltip according to the given data point.
        public void UpdateSeriesTooltip(ChartDataPointInfo dataPoint)
        {
            updateSeriesTooltipMethodInfo?.Invoke(this, new object[] { dataPoint });
        }
    }
}
