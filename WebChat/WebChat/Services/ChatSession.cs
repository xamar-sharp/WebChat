using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Microsoft.AspNetCore.SignalR.Client;
namespace WebChat.Services
{
    public class ChatSession
    {
        public static readonly ChatSession _singleton = new ChatSession();
        private static readonly HubConnection _connection;
        private static bool _ready;
        private static byte[] _data;
        static ChatSession()
        {
            _connection = new HubConnectionBuilder().WithUrl("http://192.168.43.179:5000/chat").Build();
            _connection.On<byte[]>("GetImage", _singleton.GetImage);
        }
        public async Task GetImage(byte[] data)
        {
            _data = data;
            _ready = true;
        }
        public static async Task<byte[]> ExecuteRPC()
        {
            await _connection.StartAsync();
            while(!_ready && _connection.State != HubConnectionState.Disconnected)
            {
                await Task.Delay(10);
            }
            _ready = false;
            return _data ?? throw new NotImplementedException();
        }
        public static async Task SendToHub(string value)
        {
            await _connection.InvokeAsync("GetDataFromClient", value);
        }
    }
}
