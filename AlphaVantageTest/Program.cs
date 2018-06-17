using System;
using Kermor.AlphaVantage;

namespace AlphaVantageTest
{
	class Program
    {
		public static string AlphaVantageKey = "Y3NSQOSLO04NVEMD";


		static void Main(string[] args)
        {

			//AlphaVantage.Digital_Currency("BTC", "USD", AVTimeSeries.Digital_Currency_Daily, AlphaVantageKey);
			//AlphaVantage.Indicator(new BasicIndicator("DEMA"), "MSFT",AVInterval.Daily, AVSeriesType.close, AlphaVantageKey);
			AlphaVantage.SectorPerfomances(AlphaVantageKey);

			Console.ReadLine();
        }
    }
}
