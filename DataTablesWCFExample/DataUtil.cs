using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;


namespace DataTablesWCFExample
{
    
    public class DataUtil
    {
        public static IEnumerable<ArticleHeader> GetData()
        {
            var xml = XDocument.Load(HttpContext.Current.Server.MapPath("~/data.xml"));
            var list = from x in xml.Descendants("ArticleHeader")
                       select new ArticleHeader
                                  {
                                      ArticleId = int.Parse(x.Element("ArticleId").Value),
                                      PromoImageId = int.Parse(x.Element("PromoImageId").Value),
                                      PublicationDate = DateTime.Parse(x.Element("PublicationDate").Value),
                                      Slug = x.Element("Slug").Value,
                                      StatusId = int.Parse(x.Element("StatusId").Value),
                                      Title = x.Element("Title").Value
                                  };

            return list;
        }

        public static IEnumerable<T> GetPage<T>(int pageNumber, int pageSize, IEnumerable<T> list)
        {
            var output = list
                  .Skip((pageNumber - 1) * pageSize)
                  .Take(pageSize);
            return output;
        }
    }
}
