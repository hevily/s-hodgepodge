using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Receiver
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Files.Count > 0)
            {
                var path = Server.MapPath("~") + Guid.NewGuid().ToString() + ".txt";
                Request.Files[0].SaveAs(path);
            }
        }
    }
}