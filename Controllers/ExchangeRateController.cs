using Döviz_Kuru_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Xml;

namespace Döviz_Kuru_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRateController : ControllerBase
    {
        [HttpPost]
        public DataErrorResponse GetData(DataRequest query)
        {
            DataErrorResponse errorResponse = new DataErrorResponse();

            try
            {
                string link = string.Format("http://www.tcmb.gov.tr/kurlar/{0}.xml",
                   (query.IsToday) ? "today" : string.Format("{2}{1}/{0}{1}{2}",
                    query.Day.ToString().PadLeft(2, '0'),
                    query.Month.ToString().PadLeft(2, '0'), query.Year));


                errorResponse.Errors = new List<DataResponse>();
            
                XmlDocument xmlDoc   = new XmlDocument();
                xmlDoc.Load(link);

                XmlNode tarihNode    = xmlDoc.SelectSingleNode("Tarih_Date") ?? throw new Exception("Data Not Found.");

                //XmlNode tarihNode = xmlDoc.SelectSingleNode("Tarih_Date");
                //if (tarihNode == null)
                //{
                //    errorResponse.Error = "Data Not Found.";
                //    return errorResponse;
                //}

                foreach (XmlNode node in tarihNode.ChildNodes)
                {
                    DataResponse data    = new DataResponse();
                    data.CurrencyCode    = node.Attributes["Kod"].Value;

                    data.Name            = node["Isim"          ].InnerText ;
                    data.Unit            = int.Parse(node["Unit"].InnerText);

                    data.ForexBuying     = Convert.ToDecimal("0" + node["ForexBuying"    ].InnerText.Replace(".", ","));
                    data.ForexSelling    = Convert.ToDecimal("0" + node["ForexSelling"   ].InnerText.Replace(".", ","));
                    data.BanknoteBuying  = Convert.ToDecimal("0" + node["BanknoteBuying" ].InnerText.Replace(".", ","));
                    data.BanknoteSelling = Convert.ToDecimal("0" + node["BanknoteSelling"].InnerText.Replace(".", ","));

                    errorResponse.Errors.Add(data);
                }
            }
            catch (Exception ex) { errorResponse.Error = ex.Message; }

            return errorResponse;
        }
    }
}
