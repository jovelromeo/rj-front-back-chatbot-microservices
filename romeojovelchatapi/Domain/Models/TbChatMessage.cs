using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace romeojovelchatapi.Domain.Models
{
    public class TbChatMessage
    {
        [Key]
        public string CdChatMessage { get; set; }
        public string DsMessage { get; set; }
        [Timestamp]
        public byte[] TsCreated { get; set; }

        #region relationships
        public long? CdUser { get; set; }
        [ForeignKey("CdUser")]
        public virtual TbUser TbUser { get; set;}
        public long? CdRoom { get; set; }
        [ForeignKey("CdRoom")]
        public virtual TbChatRoom TbRoom { get; set; }
        #endregion
    }
}
