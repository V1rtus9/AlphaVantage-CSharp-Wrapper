﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Kermor.AlphaVantage
{
    public enum AVSeriesType
	{
		[EnumValue("open")]open,
		[EnumValue("high")] high,
		[EnumValue("low")] low,
		[EnumValue("close")] close
	}
}
