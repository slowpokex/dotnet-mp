namespace CustomerClassLib.Threshold
{
    using System.Collections.Generic;
    using CustomerClassLib.Exceptions;

    public class ThresholdValueBuilder
    {
        private Dictionary<ThresholdEnum, (double, double)> _thresholds;

        public ThresholdValueBuilder()
        {
            _thresholds = new Dictionary<ThresholdEnum, (double, double)>();
        }

        public ThresholdValueBuilder SetLowCostThresholdValue(double from, double to)
        {
            return SetCostThresholdValue(ThresholdEnum.LowCost, from, to);
        }

        public ThresholdValueBuilder SetAverageCostThresholdValue(double from, double to)
        {
            return SetCostThresholdValue(ThresholdEnum.AverageCost, from, to);
        }

        public ThresholdValueBuilder SetHighCostThresholdValue(double from, double to)
        {
            return SetCostThresholdValue(ThresholdEnum.HighCost, from, to);
        }

        private ThresholdValueBuilder SetCostThresholdValue(ThresholdEnum rank, double from, double to)
        {
            if (from > to)
            {
                throw new InvalidThresholdException(nameof(rank), from, to);
            }
            _thresholds.Add(ThresholdEnum.HighCost, (from, to));
            return this;
        }

        public Dictionary<ThresholdEnum, (double, double)> GetThresholds()
        {
            return _thresholds;
        }
    }
}
