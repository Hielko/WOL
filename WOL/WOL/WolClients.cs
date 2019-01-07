using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace WOL
{
    public class Machine
    {
        public string Name { get; set; }
        public string MAC { get; set; }
        public string Note { get; set; }
    }

    [XmlRootAttribute("ArrayOfMachine")]
    public class ArrayOfMachine
    {
        [XmlElement("Machine")]
        public Machine[] Machines { get; set; }
    }

    public class WolClients
    {
        private ArrayOfMachine machines = null;

        public Machine GetMachine(String name)
        {
            return machines.Machines.ToList().Find(x => x.Name.Equals(name));
        }

        public List<Machine> GetMachines
        {
            get => machines.Machines.ToList();
        }

        public WolClients(String filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ArrayOfMachine));
            StreamReader reader = new StreamReader(filename);
            machines = (ArrayOfMachine)serializer.Deserialize(reader);
            reader.Close();
        }
    }




}