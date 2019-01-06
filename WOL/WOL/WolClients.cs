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


    public class WOLUtil
    {
        public void  SendWOL(String macAddress)
        {

            //    var macAddress = "01-00-00-00-00-02";                      // Our device MAC address
            macAddress = Regex.Replace(macAddress, "[-|:]", "");       // Remove any semicolons or minus characters present in our MAC address

            var sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
            {
                EnableBroadcast = true
            };

            int payloadIndex = 0;

            /* The magic packet is a broadcast frame containing anywhere within its payload 6 bytes of all 255 (FF FF FF FF FF FF in hexadecimal), followed by sixteen repetitions of the target computer's 48-bit MAC address, for a total of 102 bytes. */
            byte[] payload = new byte[1024];    // Our packet that we will be broadcasting

            // Add 6 bytes with value 255 (FF) in our payload
            for (int i = 0; i < 6; i++)
            {
                payload[payloadIndex] = 255;
                payloadIndex++;
            }

            // Repeat the device MAC address sixteen times
            for (int j = 0; j < 16; j++)
            {
                for (int k = 0; k < macAddress.Length; k += 2)
                {
                    var s = macAddress.Substring(k, 2);
                    payload[payloadIndex] = byte.Parse(s, NumberStyles.HexNumber);
                    payloadIndex++;
                }
            }

            sock.SendTo(payload, new IPEndPoint(IPAddress.Parse("255.255.255.255"), 0));  // Broadcast our packet
            sock.Close(10000);

        }

    }

}