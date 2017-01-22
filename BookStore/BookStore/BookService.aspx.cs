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
        public struct Data
        {
            public string elementID;
            public IEnumerable<IBook> books;
        }

        private string GetAllAsJson(string elementID)
        {
            Data response = new Data()
            {
                elementID = elementID,
                books = BookStoreAPI.GetService().GetBooksAsync().Result
            };

            return Newtonsoft.Json.JsonConvert.SerializeObject( response );
        }

        private string GetSearchAsJson(string elementID, string search)
        {
            Data response = new Data()
            {
                elementID = elementID,
                books = BookStoreAPI.GetService().GetBooksAsync(search).Result
            };

            return Newtonsoft.Json.JsonConvert.SerializeObject( response );
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string search = Request["search"],
                   elementID = Request["elementID"];

            if( search != null)
                Response.Write( this.GetSearchAsJson( elementID != null ? elementID : "", search ) );
            else
                Response.Write( this.GetAllAsJson( elementID != null ? elementID : "") );
        }
    }
}