using System.Data.SqlClient;

namespace ChoTotAsp.Areas.User.Payload
{
    public class NewsRequest
    {
        public string View { get; set; } = "list";
        public int Cid { get; set; } = 0; // category id
        public int Pid { get; set; } = 0; // province id
        public int Yf { get; set; } = 0; // year from
        public int Yt { get; set; } = 0; // year to
        public decimal Pf { get; set; } = 0; // price from
        public decimal Pt { get; set; } = 0; // price to
        public string Keyword { get; set; } // keyword

        // pagination
        public int P { get; set; } = 1; // current page
        public int Pz { get; set; } = 5; // page size

        // sort
        public string Sb { get; set; } = "created_date"; // sort by
        public string St { get; set; } = "desc"; // sort type
    }
}