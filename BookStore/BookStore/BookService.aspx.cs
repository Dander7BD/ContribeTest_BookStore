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
            public string callerID;
            public IEnumerable<IBook> books;
        }

        private static string AsJson(Data data)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject( data );
        }

        private static string GetAsJson(string callerID, string bookID, int quantity = 0)
        {
            IBook book = BookStoreAPI.GetService().GetBookAsync( bookID, quantity ).Result;
            List<IBook> result = new List<IBook>(1);

            if( book != null )
                result.Add( book );

            return AsJson( new Data()
            {
                callerID = callerID,
                books = result
            } );
        }

        private static string GetAsJson(string callerID, IDictionary<string, int> d)
        {
            Data response = new Data()
            {
                callerID = callerID,
                books = BookStoreAPI.GetService().GetBookAsync( d ).Result
            };

            return AsJson( response );
        }

        private static string GetAllAsJson(string callerID)
        {
            Data response = new Data()
            {
                callerID = callerID,
                books = BookStoreAPI.GetService().GetBooksAsync().Result
            };

            return AsJson( response );
        }

        private static string GetSearchAsJson(string callerID, string search)
        {
            Data response = new Data()
            {
                callerID = callerID,
                books = BookStoreAPI.GetService().GetBooksAsync(search).Result
            };

            return AsJson( response );
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request["action"],
                   callerID = Request["callerID"];
            if( action == null ) return;
            else action = action.ToLower();
            if( callerID == null ) callerID = "";

            if( action.Equals( "search" ) )
            {
                string search = Request["search"];

                if( search == null || search.Equals( "" ) )
                    Response.Write( GetAllAsJson( callerID ) );
                else
                    Response.Write( GetSearchAsJson( callerID, search ) );
            }
            else if( action.Equals( "update-cart-item" ) )
            {
                string bookID = Request["bookID"];
                int quantity = 0;

                if( bookID != null && int.TryParse( Request["quantity"], out quantity ) )
                {
                    if( Session[callerID] == null && quantity > 0 )
                        Session[callerID] = new Dictionary<string, int>();

                    //<"bookID", quantity>
                    IDictionary<string, int> d = Session[callerID] as IDictionary<string, int>;

                    if( quantity > 0 )
                        d[bookID] = quantity;
                    else
                        d.Remove( bookID );

                    Response.Write( GetAsJson( callerID, bookID, quantity ) );
                }
            }
            else if( action.Equals( "get-shopping-cart-items" ) )
            {
                IDictionary<string, int> d = Session[callerID] as IDictionary<string, int>;
                if( d == null )
                    d = new Dictionary<string, int>();

                Response.Write( GetAsJson( callerID, d ) );
            }
        }
    }
}