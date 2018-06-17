using System;
using Kermor.AlphaVantage;


namespace Kermor.AlphaVantage.Example
{
    class Program
    {
		private static readonly string AlphaVantageAPIKey = ""; //insert yoyr key here

        static void Main(string[] args)
        {
			Console.WriteLine("Insert stock symbol: ");

			string stkSymbol = Console.ReadLine();

			MarketData stock = AlphaVantage.Stock(stkSymbol, AVTimeSeries.Stock_Daily, AVOutputSize.compact, AlphaVantageAPIKey);

			Console.WriteLine(Environment.NewLine + "Symbol: {0}, Currency: {1}", stock.Symbol, stock.Currency + Environment.NewLine);

			foreach (var item in stock.Bars)
			{
				Console.WriteLine("Date:{0},Open:{1},High:{2},Low:{3},Close:{4},Volume:{5}", item.Key.ToString("dd/MM/yyyy"), item.Value.open, item.Value.high, item.Value.low, item.Value.close, item.Value.volume);
			}



			Console.WriteLine(Environment.NewLine + "----------------------------------------" + Environment.NewLine + "Insert cryptocurrency symbol");

			string ccSymbol = Console.ReadLine();

			MarketData crypto = AlphaVantage.Digital_Currency(ccSymbol, "USD", AVTimeSeries.Digital_Currency_Daily, AlphaVantageAPIKey);

			Console.WriteLine(Environment.NewLine + "Symbol: {0}, Currency: {1}", crypto.Symbol, crypto.Currency + Environment.NewLine);

			foreach (var item in stock.Bars)
			{
				Console.WriteLine("Date:{0},Open:{1},High:{2},Low:{3},Close:{4},Volume:{5}", item.Key.ToString("dd/MM/yyyy"), item.Value.open, item.Value.high, item.Value.low, item.Value.close, item.Value.volume);
			}


			Console.WriteLine(Environment.NewLine + "----------------------------------------" + Environment.NewLine + "Indicator example");

			IndicatorData indicator = AlphaVantage.Indicator(new BasicIndicator("DEMA"), "MSFT", AVInterval.Daily, AVSeriesType.close, AlphaVantageAPIKey);

			Console.WriteLine(Environment.NewLine + "Indicator: {0}, Interval: 30, Symbol: MSFT" + Environment.NewLine, indicator.name);

			int i = 0;

			foreach (var item in indicator.Values)
			{
				Console.WriteLine("Date:{0}, Value:{1}", item.Key.ToString("dd/MM/yyyy"), item.Value[0].Value);

				i++;

				if (i >= 25)
					break;
			}

			Console.ReadLine();
		}
    }
}
