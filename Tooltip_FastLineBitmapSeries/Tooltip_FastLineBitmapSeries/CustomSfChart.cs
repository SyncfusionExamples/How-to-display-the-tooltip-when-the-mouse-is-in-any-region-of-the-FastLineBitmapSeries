using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Tooltip_FastLineBitmapSeries
{
    public class CustomSfChart : SfChart
    {
        // This panel is a positional reference panel to update the tooltip.
        public Canvas AdorningCanvas { get; set; }

        // The adorning canvas is get in this method.
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            AdorningCanvas = GetTemplateChild("adorningCanvas") as Canvas;
        }
    }
}
