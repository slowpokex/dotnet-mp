namespace CustomerClassLib.CostThreshold
{
    using System.Collections.Generic;
    using CustomerClassLib.Exceptions;

    public class CostThresholdValueBuilder
    {
        private Dictionary<CostThresholdEnum, (double, double)> _thresholds;

        public CostThresholdValueBuilder()
        {
            _thresholds = new Dictionary<CostThresholdEnum, (double, double)>();
        }

        public CostThresholdValueBuilder SetLowCostThresholdValue(double from, double to)
        {
            return SetCostThresholdValue(CostThresholdEnum.LowCost, from, to);
        }

        public CostThresholdValueBuilder SetAverageCostThresholdValue(double from, double to)
        {
            return SetCostThresholdValue(CostThresholdEnum.AverageCost, from, to);
        }

        public CostThresholdValueBuilder SetHighCostThresholdValue(double from, double to)
        {
            return SetCostThresholdValue(CostThresholdEnum.HighCost, from, to);
        }

        private CostThresholdValueBuilder SetCostThresholdValue(CostThresholdEnum rank, double from, double to)
        {
            if (from > to)
            {
                throw new InvalidThresholdException(nameof(rank), from, to);
            }
            _thresholds.Add(CostThresholdEnum.HighCost, (from, to));
            return this;
        }

        public Dictionary<CostThresholdEnum, (double, double)> GetThresholds()
        {
            return _thresholds;
        }
    }
}
