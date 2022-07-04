namespace Zeiss_webapi.Models {
    public class MsgRequest {
        public string Id { get; set; }

        public string MachineId { get; set; }

        public string Timestamp { get; set; }

        public string Status { get; set; }

        public string Topic { get; set; }

        public string Event { get; set; }

        public string Ref { get; set; }

        public int PageSize { get; set; } = 10;

        public int PageIndex { get; set; } = 1;

    }
}
