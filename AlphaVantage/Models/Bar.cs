using System;
using System.Collections.Generic;
using System.Text;

namespace Kermor.AlphaVantage
{
    public struct Bar
    {
		public double open;
		public double high;
		public double low;
		public double close;
		public double volume;
		public DateTime timestamp;

		public Bar(double open, double high, double low, double close, double volume, DateTime timestamp)
		{
			this.open = open;
			this.high = high;
			this.low = low;
			this.close = close;
			this.volume = volume;
			this.timestamp = timestamp;
		}

		public string ToLine(string DateTimeFormat = "dd/MM/yyyy HH:mm")
		{
			return timestamp.ToString(DateTimeFormat) + "," + open + "," + high + "," + low + "," + close + "," + volume;
		}
    }
}
