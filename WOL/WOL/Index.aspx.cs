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
        private WolClients wolClients;

        protected void Page_Init(object sender, EventArgs e)
        {
            wolClients = new WolClients(HttpContext.Current.Server.MapPath("~") + @"wol.xml");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                wolClients.GetMachines.ForEach(machine => lbWolClients.Items.Add(machine.Name));
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
                var Item = lbWolClients.Items[lbWolClients.SelectedIndex];
                var machine = wolClients.GetMachine(Item.Text);
                if (machine != null)
                {
                    TBLog.Text += $"Waking up {Item.Text}, MAC {machine.MAC} .... ";
                    try
                    {
                        new WOLUtil().SendWOL(machine.MAC);
                    } catch (Exception ex)
                    {
                        TBLog.Text += ex.ToString();
                    }
                }
                else
                {
                    TBLog.Text += $"Unknown machine {Item.Text}";
                }

            }

        }
    }
}