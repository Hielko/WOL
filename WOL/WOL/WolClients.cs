using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    public class WOLClients
    {
        private ArrayOfMachine machines = null;

        public Machine GetMachine(String name)
        {
            return GetMachines.Find(machine => machine.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
        }

        public List<Machine> GetMachines
        {
            get => machines.Machines.ToList();
        }

        public WOLClients(String filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ArrayOfMachine));
            StreamReader reader = new StreamReader(filename);
            machines = (ArrayOfMachine)serializer.Deserialize(reader);
            reader.Close();
        }
    }

}