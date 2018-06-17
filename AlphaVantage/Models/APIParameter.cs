using System;
using System.Collections.Generic;
using System.Text;

namespace Kermor.AlphaVantage
{
	public struct APIParameter
	{
		public string name;
		public string value;

		public APIParameter(string name, string value)
		{
			this.name = name;
			this.value = value;
		}

		public string ToApiString()
		{
			return $"&{name}={value}";
		}
	}
}
