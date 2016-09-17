using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Azure.Devices;

namespace LightController
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static ServiceClient serviceClient;
        static string connectionString = "HostName=Alleniot.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=u/stG6hywr4Vxnr2sYKXeqLoTZTg39WKF8fj1RVaVyc=";

        public MainWindow()
        {
            InitializeComponent();

            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
        }

        private async Task TurnLight(bool isOn)
        {
            await SendCloudToDeviceMessageAsync(isOn);
        }

        private static async Task SendCloudToDeviceMessageAsync(bool isOn)
        {
            var commandMessage = new Message(Encoding.ASCII.GetBytes(isOn ? "on" : "off"));
            await serviceClient.SendAsync("raspberry3-iot", commandMessage);//
        }

        private async void BtnTurnOn_OnClick(object sender, RoutedEventArgs e)
        {
            await TurnLight(true);
        }

        private async void BtnTurnOff_OnClick(object sender, RoutedEventArgs e)
        {
            await TurnLight(false);
        }
    }
}
