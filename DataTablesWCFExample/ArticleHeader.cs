using System;

namespace DataTablesWCFExample
{
    public class ArticleHeader
    {
        public int PromoImageId { get; set; }
        public int ArticleId { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public int StatusId { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
