using System;
using System.Text.Json.Serialization;

namespace Zeiss_webapi.Models {
    [Serializable]
    public class MyJsonEntity {
        
        [JsonPropertyName("topic")]
        public string Topic { get; set; }

        [JsonPropertyName("ref")]
        public string Ref { get; set; }

        [JsonPropertyName("payload")]
        public MyPayload Payload { get; set; }
        
        [JsonPropertyName("event")]
        public string Event { get; set; }
    }

    [Serializable]
    public class MyPayload {
        [JsonPropertyName("machine_id")]
        public string Machine_Id { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
