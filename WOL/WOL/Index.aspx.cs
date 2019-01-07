using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace WOL
{
    public partial class Index : System.Web.UI.Page
    {
        private WOLClients wolClients;
        const string cookieName = "selectedMachine";

        protected void Page_Init(object sender, EventArgs e)
        {
            wolClients = new WOLClients(HttpContext.Current.Server.MapPath("~") + @"wol.xml");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                wolClients.GetMachines.ForEach(machine => lbWolClients.Items.Add(new ListItem(machine.Note, machine.Name)));

                try
                {
                    if (int.TryParse(Request.Cookies[cookieName].Value, out int i))
                    {
                        lbWolClients.SelectedIndex = i;
                    }
                }
                catch (Exception ex)
                {
                    // Don't care
                }
            }
        }

        protected void lbWolClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnWakeUp.Enabled = lbWolClients.SelectedIndex >= 0;
        }

        protected void BtnWakeUp_Click(object sender, EventArgs e)
        {
            if (lbWolClients.SelectedIndex >= 0)
            {
                Response.Cookies[cookieName].Value = lbWolClients.SelectedIndex.ToString();

                var Item = lbWolClients.Items[lbWolClients.SelectedIndex];
                var machine = wolClients.GetMachine(Item.Value);
                if (machine != null)
                {
                    Response.Redirect($"runWOL.aspx?name={machine.Name}");
                }
            }
        }

    }
}