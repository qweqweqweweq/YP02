using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP02.Models
{
    public class NetworkSettings
    {
        [Key]
        public int Id { get; set; }
        public string IpAddress { get; set; }
        public string SubnetMask { get; set; }
        public string MainGateway { get; set; }
        public string DNSServers { get; set; }
        public int OborudovanieId { get; set; }
    }
}
