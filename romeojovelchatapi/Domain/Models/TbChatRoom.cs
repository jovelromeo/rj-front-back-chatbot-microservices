using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace romeojovelchatapi.Domain.Models
{
    public class TbChatRoom
    {
        [Key]
        public long CdRoom { get; set; }
        public string DsRoom { get; set; }

        #region relationships
        public virtual IEnumerable<TbChatMessage> TbMessage { get; set; }
        #endregion
    }
}
