using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace romeojovelchatapi.Domain.Models
{
    public class TbUser:AuditedEntity
    {
        [Key]
        public long CdUser { get; set; }
        public string DsEmail { get; set; }
        public string DsPassword { get; set; }

        #region repatioinships
        public virtual IEnumerable<TbChatMessage>TbMessage { get; set; }

        #endregion
    }
}
