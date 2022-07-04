using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Zeiss_webapi.Models;
using Zeiss_webapi.Providers;
using Zeiss_webapi.Services;

namespace Zeiss_webapi.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class ZeissController {

        private readonly ILogger<ZeissController> _logger;
        private readonly MsgProvider _msgProvider;
        private readonly WebSocketService _webSocketService;

        public ZeissController(ILogger<ZeissController> logger, MsgProvider msgProvider, WebSocketService webSocketService)
        {
            _logger = logger;
            _msgProvider = msgProvider;
            _webSocketService = webSocketService;
        }

        [HttpGet("Test")]
        public string Test()
        {
            return "hello world";
        }

        [HttpGet("Add")]
        public bool Add()
        {
            var model = new MsgEntity()
            {
                Id = Guid.NewGuid().ToString(),
                MachineId = Guid.NewGuid().ToString(),
                Timestamp = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Topic = "",
                Event = "",
                Ref = null,
                Status = ""
            };
            _msgProvider.Add(model);
            return true;
        }

        [HttpPost("Query")]
        public DataResponse<MsgEntity> Query(MsgRequest request)
        {
            return _msgProvider.Query(request);
        }

        [HttpPost("/listen")]
        public async Task ListenWebSocket(string address = "ws://machinestream.herokuapp.com/ws")
        {
            _webSocketService.Init(address);

        }
    }
}
