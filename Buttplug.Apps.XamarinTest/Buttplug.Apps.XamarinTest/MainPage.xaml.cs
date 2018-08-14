using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Buttplug.Server;
using Buttplug.Server.Connectors.WebsocketServer;
using Xamarin.Forms;

namespace Buttplug.Apps.XamarinTest
{
	public partial class MainPage : ContentPage
	{
        ButtplugWebsocketServer _server = new ButtplugWebsocketServer();

	    class ServerFactory : IButtplugServerFactory
	    {
	        public ButtplugServer GetServer()
	        {
	            return new ButtplugServer("Mobile Server", 0);
	        }
	    }

	    public static string GetLocalIPAddress()
	    {
	        var host = Dns.GetHostEntry(Dns.GetHostName());
	        foreach (var ip in host.AddressList)
	        {
	            if (ip.AddressFamily == AddressFamily.InterNetwork)
	            {
	                return ip.ToString();
	            }
	        }
	        throw new Exception("No network adapters with an IPv4 address in the system!");
	    }

        public MainPage()
		{
			InitializeComponent();
		}

	    private async void ServerBtn_Clicked(object sender, EventArgs e)
	    {
	        NetworkLabel.Text = GetLocalIPAddress();
	        if (!_server.IsConnected)
	        {
	            await _server.StartServer(new ServerFactory());
	            ServerBtn.Text = "Stop Server";
	        }
	        else
	        {
	            await _server.StopServer();
	            ServerBtn.Text = "Start Server";
            }
	    }
    }
}
