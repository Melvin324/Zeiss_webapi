using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zeiss_webapi.Models {
    [Table("Msg")]
    [Serializable]
    public class MsgEntity {
        
        public string Id { get; set; }

        public string MachineId { get; set; }

        public string Timestamp { get; set; }

        public string Status { get; set; }

        public string Topic { get; set; }

        public string Event { get; set; }

        public string Ref { get; set; }
    }
}
