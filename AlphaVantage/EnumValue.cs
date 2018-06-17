using System;

namespace Kermor.AlphaVantage
{
    public class EnumValue:Attribute
    {
		public string Value { get; }

		public EnumValue(string value)
		{
				this.Value = value;
		}
	}
}
