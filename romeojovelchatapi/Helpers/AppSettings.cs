using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace romeojovelchatapi.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int TokenDays { get; set; }
        public string RabbitMqHost { get; set; }
    }
}
