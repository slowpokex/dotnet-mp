using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerClassLib.CostThreshold
{
    public class CostThresholdValueEntity
    {
        public CostThresholdEnum CostThresholdGroup { get; set; }

        public (int, int) CostThresholdValue { get; set; }
    }
}
