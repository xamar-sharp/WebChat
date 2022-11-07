using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using Xamarin.Forms;
using System.Windows.Input;
using System.Threading.Tasks;
using System.IO;
namespace WebChat.Services
{
    public sealed class ChatSessionViewModel:ReactiveObject
    {
        private byte[] _data;
        public byte[] Data { get => _data; set
            {
                this.RaiseAndSetIfChanged(ref _data, value);
            } }
        public ICommand SpeakHubCommand { get; set; }
        public ICommand SendToHubCommand { get; set; }
        private readonly IDisposable _dataChangedSub;
        public ChatSessionViewModel()
        {
            _dataChangedSub = this.ObservableForProperty(v => v.Data).Subscribe(dataObs => { });
            SpeakHubCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                Data = await ChatSession.ExecuteRPC();
            });
            SendToHubCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                await ChatSession.SendToHub("Hello from client!");
            });
        }
    }
}
