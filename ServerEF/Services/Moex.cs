using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace ServerEF.Services
{
    public class Moex:IStockPrice
    {
        
        public Dictionary<string,double> GetStock(List<string> companies)
        {
            XmlDocument xDoc = new XmlDocument();
            Dictionary<string, double> prices = new Dictionary<string, double>();
            xDoc.Load("https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities.xml?iss.meta=off&iss.only=securities&securities.columns=SECID,PREVADMITTEDQUOTE");

            XmlElement xRoot = xDoc.DocumentElement;
            foreach (var item in companies)
            {

                XmlNodeList nodes = xRoot.SelectNodes($"data/rows/row[@SECID='{item}']/@PREVADMITTEDQUOTE");
                string value = nodes.Item(0).Value;
                try
                {
                    double price = double.Parse(value, CultureInfo.InvariantCulture);
                    prices.Add(item, price);
                }
                catch
                {
                    prices.Add(item, 0);
                }
            }
            return prices;
        }
    }
}
