using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookStore
{
    public partial class BookSearch : System.Web.UI.Page
    {
        private string GetAllAsJson()
        {
            IEnumerable<IBook> books = BookStoreAPI.GetService().GetBooksAsync().Result;

            return Newtonsoft.Json.JsonConvert.SerializeObject( books );
        }

        private string GetSearchAsJson(string search)
        {
            IEnumerable<IBook> books = BookStoreAPI.GetService().GetBooksAsync(search).Result;

            return Newtonsoft.Json.JsonConvert.SerializeObject( books );
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string search = Request["search"];
            if( search != null)
                Response.Write( this.GetSearchAsJson( search ) );
            else
                Response.Write( this.GetAllAsJson() );
        }
    }
}