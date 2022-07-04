using System.Collections.Generic;

namespace Zeiss_webapi.Models {
    public class DataResponse<T> {

        public long Total { get; set; }

        public List<T> Data { get; set; }
    }
}
