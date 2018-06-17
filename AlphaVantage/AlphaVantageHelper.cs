using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.Reflection;

namespace Kermor.AlphaVantage
{
   public static  class AlphaVantageHelper
    {
		/// <summary>
		/// 
		/// </summary>
		/// <param name="APIParameters"></param>
		/// <returns></returns>
		public static string CreateURL(List<APIParameter> APIParameters)
		{
			return APIParameters.Aggregate(@"https://www.alphavantage.co/query?", (current, param) => current + param.ToApiString());
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		public static string RequestRawData(string url)
		{
			try
			{
				HttpWebResponse response = (HttpWebResponse)WebRequest.Create(url).GetResponse();

				using (StreamReader Stream = new StreamReader(response.GetResponseStream()))
				{
					var result = Stream.ReadToEnd();

					Stream.Close();
					response.Close();

					return (string)result;
				}

			}catch(Exception e)
			{
				return null;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="data"></param>
		/// <param name="bars"></param>
		/// <param name="dataMap"></param>
		public static void ProcessRawData(string[] data, ref Dictionary<DateTime, Bar> bars, int[] dataMap)
		{

			for (int i = 1; i < data.Length; i++)
			{
				string[] barData = data[i].Split(',');

				try
				{
					DateTime barDateTmp = Convert.ToDateTime(barData[dataMap[0]]);

					bars.Add(barDateTmp, new Bar(Math.Round(Convert.ToDouble(barData[dataMap[1]]), 2),
											Math.Round(Convert.ToDouble(barData[dataMap[2]]), 2), Math.Round(Convert.ToDouble(barData[dataMap[3]]), 2),
											Math.Round(Convert.ToDouble(barData[dataMap[4]]), 2), Math.Round(Convert.ToDouble(barData[dataMap[5]]), 0),
											barDateTmp));
				}
				catch (FormatException)
				{
					bars = null;
				}
			}

		}
	}
}
