namespace CustomerClassLib.Threshold
{
    public class ThresholdValueEntity
    {
        public ThresholdEnum CostThresholdGroup { get; set; }

        public (int, int) CostThresholdValue { get; set; }
    }
}
