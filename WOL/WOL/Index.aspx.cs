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

        protected void Page_Init(object sender, EventArgs e)
        {
            wolClients = new WOLClients(HttpContext.Current.Server.MapPath("~") + @"wol.xml");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                wolClients.GetMachines.ForEach(machine => lbWolClients.Items.Add(new ListItem(machine.Note, machine.Name)));
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
                var machine = wolClients.GetMachine(Item.Value);
                if (machine != null)
                {
                    BtnWakeUp.Enabled = false;
                    TBLog.Text += $"Waking up {machine.Name}, MAC {machine.MAC} .... ";
                    try
                    {
                        WOLUtil.SendWOL(machine.MAC);
                    }
                    catch (Exception ex)
                    {
                        TBLog.Text += ex.ToString();
                    }
                    BtnWakeUp.Enabled = true;
                }
                else
                {
                    TBLog.Text += $"Unknown machine {Item.Text}";
                }
            }
        }

    }
}