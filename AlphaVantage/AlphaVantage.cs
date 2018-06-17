using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kermor.AlphaVantage
{
    public static class AlphaVantage
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="symbol"></param>
		/// <param name="timeseries"></param>
		/// <param name="outputsize"></param>
		/// <param name="apikey"></param>
		/// <returns></returns>
		public static MarketData Stock(string symbol, AVTimeSeries timeseries, AVOutputSize outputsize, string apikey)
		{
			MarketData dataTmp = new MarketData(symbol);

			List<APIParameter> parameters = new List<APIParameter>()
			{
				new APIParameter("function", timeseries.GetValue()),
				new APIParameter("symbol", symbol),
				new APIParameter("outputsize", outputsize.GetValue()),
				new APIParameter("apikey", apikey),
				new APIParameter("datatype", "csv")
			};

			int[] dataMap = new int[] { 0, 1, 2, 3, 4, 5 };

			AlphaVantageHelper.ProcessRawData(AlphaVantageHelper.RequestRawData(AlphaVantageHelper.CreateURL(parameters)).Split(Environment.NewLine), ref dataTmp.Bars, dataMap);

			return dataTmp;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="symbol"></param>
		/// <param name="timeseries"></param>
		/// <param name="interval"></param>
		/// <param name="outputsize"></param>
		/// <param name="apikey"></param>
		/// <returns></returns>
		public static MarketData Stock(string symbol, AVTimeSeries timeseries, AVInterval interval, AVOutputSize outputsize, string apikey)
		{
			MarketData dataTmp = new MarketData(symbol);

			List<APIParameter> parameters = new List<APIParameter>()
			{
				new APIParameter("function", timeseries.GetValue()),
				new APIParameter("symbol", symbol),
				new APIParameter("interval", interval.GetValue()),
				new APIParameter("outputsize", outputsize.GetValue()),
				new APIParameter("apikey", apikey),
				new APIParameter("datatype", "csv")
			};

			int[] dataMap = new int[] { 0, 1, 2, 3, 4, 5 };

			AlphaVantageHelper.ProcessRawData(AlphaVantageHelper.RequestRawData(AlphaVantageHelper.CreateURL(parameters)).Split(Environment.NewLine), ref dataTmp.Bars, dataMap);

			return dataTmp;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="symbol"></param>
		/// <param name="market"></param>
		/// <param name="timeseries"></param>
		/// <param name="apikey"></param>
		/// <returns></returns>
		public static MarketData Digital_Currency(string symbol, string market, AVTimeSeries timeseries, string apikey)
		{

			MarketData dataTmp = new MarketData(symbol, market);

			List<APIParameter> parameters = new List<APIParameter>()
			{
				new APIParameter("function", timeseries.GetValue()),
				new APIParameter("symbol", symbol),
				new APIParameter("market", market),
				new APIParameter("apikey", apikey),
				new APIParameter("datatype", "csv")
			};

			int[] dataMap = new int[] { 0, 1, 2, 3, 4, 9 };

			AlphaVantageHelper.ProcessRawData(AlphaVantageHelper.RequestRawData(AlphaVantageHelper.CreateURL(parameters)).Split(Environment.NewLine), ref dataTmp.Bars, dataMap);

			return dataTmp;
		}

		public static IndicatorData Indicator(TechnicalIndicator indicator, string symbol, AVInterval interval, AVSeriesType seriestype, string apikey)
		{
			IndicatorData dataTmp = new IndicatorData(indicator.Name);

			List<APIParameter> parameters = new List<APIParameter>()
			{
				new APIParameter("function", indicator.Name.ToUpper()),
				new APIParameter("symbol", symbol),
				new APIParameter("interval", interval.GetValue()),
				new APIParameter("series_type", seriestype.GetValue()),
				new APIParameter("apikey", apikey),
			};

			parameters.InsertRange(3, indicator.parameters);

			JObject data = JsonConvert.DeserializeObject<JObject>(AlphaVantageHelper.RequestRawData(AlphaVantageHelper.CreateURL(parameters)));

			dataTmp.Values = data.Last.Values().OfType<JProperty>().Select(x => new KeyValuePair<DateTime, List<IndicatorSingleValue>>(Convert.ToDateTime(x.Name),
				
				x.Value.OfType<JProperty>().Select(y => new IndicatorSingleValue
					{
						Key = y.Name,
						Value = Convert.ToDouble(y.Value.ToString())

					}).ToList()

				)).ToDictionary(x => x.Key, x => x.Value);
				
			return dataTmp;
		}

		public static SectorData SectorPerfomances(string apikey)
		{
			SectorData dataTmp = new SectorData();

			List<APIParameter> parameters = new List<APIParameter>()
			{
				new APIParameter("function", "SECTOR"),
				new APIParameter("apikey", apikey),
			}; 

			JObject data = JsonConvert.DeserializeObject<JObject>(AlphaVantageHelper.RequestRawData(AlphaVantageHelper.CreateURL(parameters)));


			dataTmp.RankA_Realtime.Sectors = data["Rank A: Real-Time Performance"].OfType<JProperty>().Select(x => new KeyValuePair<string, string>(x.Name, x.Value.ToString())).ToDictionary(x => x.Key, x => x.Value);
			dataTmp.RankB_1_Day.Sectors = data["Rank B: 1 Day Performance"].OfType<JProperty>().Select(x => new KeyValuePair<string, string>(x.Name, x.Value.ToString())).ToDictionary(x => x.Key, x => x.Value);
			dataTmp.RankC_5_Days.Sectors = data["Rank C: 5 Day Performance"].OfType<JProperty>().Select(x => new KeyValuePair<string, string>(x.Name, x.Value.ToString())).ToDictionary(x => x.Key, x => x.Value);
			dataTmp.RankD_1_Month.Sectors = data["Rank D: 1 Month Performance"].OfType<JProperty>().Select(x => new KeyValuePair<string, string>(x.Name, x.Value.ToString())).ToDictionary(x => x.Key, x => x.Value);
			dataTmp.RankE_3_Months.Sectors = data["Rank E: 3 Month Performance"].OfType<JProperty>().Select(x => new KeyValuePair<string, string>(x.Name, x.Value.ToString())).ToDictionary(x => x.Key, x => x.Value);
			dataTmp.RankF_YTD.Sectors = data["Rank F: Year-to-Date (YTD) Performance"].OfType<JProperty>().Select(x => new KeyValuePair<string, string>(x.Name, x.Value.ToString())).ToDictionary(x => x.Key, x => x.Value);
			dataTmp.RankG_1_Year.Sectors = data["Rank G: 1 Year Performance"].OfType<JProperty>().Select(x => new KeyValuePair<string, string>(x.Name, x.Value.ToString())).ToDictionary(x => x.Key, x => x.Value);
			dataTmp.RankH_3_Year.Sectors = data["Rank H: 3 Year Performance"].OfType<JProperty>().Select(x => new KeyValuePair<string, string>(x.Name, x.Value.ToString())).ToDictionary(x => x.Key, x => x.Value);
			dataTmp.RankI_5_Years.Sectors = data["Rank I: 5 Year Performance"].OfType<JProperty>().Select(x => new KeyValuePair<string, string>(x.Name, x.Value.ToString())).ToDictionary(x => x.Key, x => x.Value);
			dataTmp.RankJ_10_Years.Sectors = data["Rank J: 10 Year Performance"].OfType<JProperty>().Select(x => new KeyValuePair<string, string>(x.Name, x.Value.ToString())).ToDictionary(x => x.Key, x => x.Value);


			return dataTmp;

		}
	}
}
