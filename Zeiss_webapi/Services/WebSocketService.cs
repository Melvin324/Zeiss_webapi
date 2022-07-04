using System;
using System.Buffers;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Zeiss_webapi.Models;
using Zeiss_webapi.Providers;

namespace Zeiss_webapi.Services {
    public class WebSocketService {
        private readonly MsgProvider _msgProvider;
        private static readonly object obj = new object();

        public WebSocketService(MsgProvider msgProvider)
        {
            _msgProvider = msgProvider;
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="address"></param>
        public async Task Init(string address)
        {
            try{
                var webSocket = new ClientWebSocket();
                await webSocket.ConnectAsync(new Uri(address), CancellationToken.None);
                await RecvAsync(webSocket, CancellationToken.None);

            }
            catch (Exception ex){
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// RecvAsync
        /// </summary>
        /// <param name="websocket"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="WebSocketException"></exception>
        public async Task RecvAsync(WebSocket websocket, CancellationToken cancellationToken)
        {
            var buffer = ArrayPool<byte>.Shared.Rent(1024 * 4);
            try{
                while (websocket.State == WebSocketState.Open){
                    var result = await websocket.ReceiveAsync(buffer, CancellationToken.None);
                    if (result.MessageType == WebSocketMessageType.Close){
                        throw new WebSocketException(WebSocketError.ConnectionClosedPrematurely, result.CloseStatusDescription);
                    }
                    var data = Encoding.UTF8.GetString(buffer.AsSpan(0, result.Count));
                    if (!string.IsNullOrEmpty(data)){
                        var myJsonEntity = JsonConvert.DeserializeObject<MyJsonEntity>(data);
                        if (myJsonEntity != null){
                            lock (obj){
                                //TODO save to db
                                var model = new MsgEntity()
                                {
                                    Id = myJsonEntity.Payload.Id,
                                    MachineId = myJsonEntity.Payload.Machine_Id,
                                    Timestamp = myJsonEntity.Payload.Timestamp,
                                    Status = myJsonEntity.Payload.Status,
                                    Event = myJsonEntity.Event,
                                    Ref = myJsonEntity.Ref,
                                    Topic = myJsonEntity.Topic,
                                };
                                _msgProvider.AddAsync(model);
                            }
                        }
                    }
                    //var sendStr = Encoding.UTF8.GetBytes($"Server: {DateTime.Now}");
                    //await websocket.SendAsync(sendStr, WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
            catch (Exception e){
                Console.WriteLine(e);
                throw;
            }
            finally{
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }
    }
}
