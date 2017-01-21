using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookStore.Web
{
    public partial class AjaxBookSearch : System.Web.UI.UserControl
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender( e );

            var scripts = ScriptManager.GetCurrent(this.Page).Scripts;
            scripts.Add( new ScriptReference( "~/Scripts/jquery-3.1.1.min.js" ) );
            scripts.Add( new ScriptReference( "~/Scripts/booksearch-ajax.js" ) );
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}