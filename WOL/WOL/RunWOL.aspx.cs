using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;

namespace WOL
{

    public partial class RunWOL : System.Web.UI.Page
    {
        private WOLClients wolClients;
        private Machine machine;

        private void BackgroundTask()
        {
            WOLUtil.SendWOL(machine.MAC);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string name = Request.QueryString["name"];//.ToString();
            wolClients = new WOLClients(HttpContext.Current.Server.MapPath("~") + @"wol.xml");
            machine = wolClients.GetMachine(name);
            if (machine != null)
            {
                Label1.Text = $"Waking up {machine.Name}, MAC {machine.MAC} .... ";

                Thread obj = new Thread(new ThreadStart(delegate ()
                                            {
                                                WOLUtil.SendWOL(machine.MAC);
                                            }
                    ));
                obj.IsBackground = true;
                obj.Start();

            }
            else
            {
                Label1.Text = $"Unknown machine '{name}'";
            }
        }
    }
}