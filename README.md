# How to display the tooltip when the mouse is in any region of the FastLineBitmapSeries in WPF Chart (SfChart)?

This article explains how to display a [tooltip](https://help.syncfusion.com/cr/wpf/Syncfusion.SfChart.WPF~Syncfusion.UI.Xaml.Charts.ChartTooltip_members.html) when the mouse is over any region of the [FastLineBitmapSeries](https://help.syncfusion.com/cr/cref_files/wpf/Syncfusion.SfChart.WPF~Syncfusion.UI.Xaml.Charts.FastLineBitmapSeries.html).

By default, the [tooltip](https://help.syncfusion.com/cr/wpf/Syncfusion.SfChart.WPF~Syncfusion.UI.Xaml.Charts.ChartTooltip_members.html) is displayed over the detect area of the chart data point. This detect area is calculated based on the chart size.

![Fast Chart with tooltip detect area over a data point](https://user-images.githubusercontent.com/53489303/200633939-da670b11-8155-4027-b882-8b2464495433.png)

You can display the [tooltip](https://help.syncfusion.com/cr/wpf/Syncfusion.SfChart.WPF~Syncfusion.UI.Xaml.Charts.ChartTooltip_members.html) in all regions of the series by manually updating it in the **MouseMove** event of the [SfChart](https://help.syncfusion.com/cr/wpf/Syncfusion.SfChart.WPF~Syncfusion.UI.Xaml.Charts.SfChart.html). The following steps will assist you to update the tooltip manually for [FastLineBitmapSeries](https://help.syncfusion.com/cr/cref_files/wpf/Syncfusion.SfChart.WPF~Syncfusion.UI.Xaml.Charts.FastLineBitmapSeries.html).

**Step 1:** Create a custom chart by inheriting it from [SfChart](https://help.syncfusion.com/cr/wpf/Syncfusion.SfChart.WPF~Syncfusion.UI.Xaml.Charts.SfChart.html) and declare **AdorningCanvas** property as type Canvas. Override the **OnApplyTemplate()** template method and define the **AdorningCanvas** property in it. This **AdorningCanvas** helps in calculating the position of the mouse in the series.

```
public class CustomSfChart : SfChart
{
    public Canvas AdorningCanvas { get; set; }
 
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        AdorningCanvas = GetTemplateChild("adorningCanvas") as Canvas;
    }
}
```

Defining the custom chart.
```
<local:CustomSfChart>
   …
</local:CustomSfChart>
```

**Step 2:** Create custom series to access certain internal elements of the chart series. The internal field **mousePos**, internal methods **IsHitTestSeries()**, **UpdateSeriesTooltip()**, and [SeriesPanel](https://help.syncfusion.com/cr/cref_files/wpf/Syncfusion.SfChart.WPF~Syncfusion.UI.Xaml.Charts.ChartSeriesPanel.html) are updated and used to calculate and display the tooltip manually.
```
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
 
    // This method is overridden to get the SeriesPanel.
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
```

Defining the custom series
```
<local:CustomSfChart>
    ...
    <local:CustomFastLineBitmapSeries />
</local:CustomSfChart>
```

**Step 3:** Create the **DataModel** that passes data to update the tooltip manually.
```
public class ChartDataPointInfo : ChartSegment
{ 
    public double XData { get; set; }
    public double YData { get; set; }
}
```

**Step 4:** By raising the **MouseMove** event of the chart and in its handler, the tooltip position can be calculated and displayed. The chart data near the mouse position is calculated using the **FindNearestChartPoint()** method of the series and this point is used in the **UpdateSeriesTooltip()** method to show the tooltip. 
```
<local:CustomSfChart x:Name="Chart" MouseMove="Chart_MouseMove" Margin="5">
```

## Output:
The following chart shows the [tooltip](https://help.syncfusion.com/cr/wpf/Syncfusion.SfChart.WPF~Syncfusion.UI.Xaml.Charts.ChartTooltip_members.html) when the mouse is placed in any part of the series.

![Fast Chart with tooltip displayed between two data points](https://user-images.githubusercontent.com/53489303/200634652-3487a0dd-df83-44c7-bfd3-6fd21ab67058.png)

KB article - [How to display the tooltip when the mouse is in any region of the FastLineBitmapSeries in WPF Chart](https://www.syncfusion.com/kb/10921/how-to-display-the-tooltip-when-the-mouse-is-in-any-region-of-the-fastlinebitmapseries-in)

## See also

[How to export chart as image](https://help.syncfusion.com/wpf/charts/exporting)

[How to add trackball in WPF Chart](https://help.syncfusion.com/wpf/charts/interactive-features/trackball)

[How to do zooming and panning in WPF Chart](https://help.syncfusion.com/wpf/charts/interactive-features/zoompan)
