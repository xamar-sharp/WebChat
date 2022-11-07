using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using System.Globalization;
namespace WebChat
{
    public partial class MainPage : ContentPage
    {
        public Services.ChatSessionViewModel ViewModel { get; set; }
        public MainPage()
        {
            Resources = new ResourceDictionary() { ["byteConv"] = new BytesToImageConverter() };
            InitializeComponent();
            this.BindingContext = ViewModel = new Services.ChatSessionViewModel();
        }
    }
    public sealed class BytesToImageConverter : IValueConverter
    {
        public object Convert(object path,Type target,object param,CultureInfo info)
        {
            return ImageSource.FromStream(() => new MemoryStream(path as byte[]));
        }
        public object ConvertBack(object path, Type target, object param, CultureInfo info)
        {
            throw new NotImplementedException();
        }
    }
}
