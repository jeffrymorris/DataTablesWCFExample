using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Web.Script.Serialization;

namespace DataTablesWCFExample.webservices
{
#if DEBUG
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
#endif
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ArticleListService
    {
        public void DoWork()
        {
        }

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest, Method = "GET")]
        public string GetArticles(int? iDisplayStart,
                                  int iDisplayLength,
                                  string sSearch,
                                  bool bEscapeRegex,
                                  int iColumns,
                                  int iSortingCols,
                                  int iSortCol_0,
                                  string sSortDir_0,
                                  int sEcho,
                                  int websiteId,
                                  int? categoryId)
        {
            var data = DataUtil.GetData();

            var results = data.
                Skip(iDisplayStart.Value).
                Take(iDisplayLength);

            //Get the "aaData"
            var json = from r in results
                                         select Convert(new
                                                            {
                                                                r.PromoImageId,
                                                                r.ArticleId,
                                                                r.Slug,
                                                                r.Title,
                                                                r.StatusId,
                                                                r.PublicationDate
                                                            });

            //Generate the return json string
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(new
                                            {
                                                sEcho,
                                                iTotalRecords = data.Count(),
                                                iTotalDisplayRecords = data.Count(),
                                                aaData = json
                                            });
        }

        public string[] Convert(object record)
        {
            var properties = from p in record.GetType().GetProperties()
                             select p.GetValue(record, null) == null ? 
                             string.Empty: p.GetValue(record, null).ToString();

            return properties.ToArray();
        }
    }
}
