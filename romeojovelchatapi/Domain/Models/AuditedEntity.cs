using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace romeojovelchatapi.Domain.Models
{
    public abstract class AuditedEntity
    {
        public DateTime DtCreated { get; set; }
    }
}
