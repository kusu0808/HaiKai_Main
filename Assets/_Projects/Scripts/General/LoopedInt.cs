namespace General
{
    public struct LoopedInt
    {
        private int _value;

        /// <summary>
        /// Exclusive
        /// </summary>
        private readonly int _maxValue;

        public LoopedInt(int length, int initValue = 0)
        {
            _maxValue = (length is >= 2 and <= 20) ? length : 2;
            _value = (initValue >= 0 && initValue <= _maxValue) ? initValue : 0;
        }

        public int Value
        {
            get => _value;
            set
            {
                int val = value;
                while (val < 0) val += _maxValue;
                while (val >= _maxValue) val -= _maxValue;
                _value = val;
            }
        }
    }
}