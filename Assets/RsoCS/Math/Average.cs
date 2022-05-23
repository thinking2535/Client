namespace rso.math
{
    public class CAverage
    {
		double _Count = 1.0;
		double _Sum = 0.0;

		public CAverage(double Count_)
		{
			_Count = Count_;
		}
        public CAverage(double Count_, double Average_)
        {
			_Count = Count_;
            _Sum = Count_ * Average_;
        }
        public void SetAverage(double Average_)
        {
            _Sum = _Count * Average_;
        }
        public double GetCount()
        {
            return _Count;
        }
        public double GetSum()
        {
            return _Sum;
        }
        public static CAverage operator +(CAverage Self_, double Value_)
        {
            Self_._Sum *= (Self_._Count - 1.0);
            Self_._Sum /= Self_._Count;
            Self_._Sum += Value_;
            return Self_;
        }
        public static explicit operator double(CAverage Self_)
        {
            return Self_._Sum / Self_._Count;
        }
    }
}
