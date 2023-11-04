using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Common.IpFinder
{
    public class IpAdressFinder
    {
        public static string GetIpAddress()
        {
            string ip = "";
            string hostName = Dns.GetHostName();
            var addresses = Dns.GetHostAddresses(hostName);
            foreach (var address in addresses)
            {
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ip = address.ToString();
                }
            }


            return ip;
        }
    }
}
