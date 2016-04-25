using System;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
namespace SpiderBot
{
	public class SpiderBotApi
	{

		public static SpiderBotApi Shared {get;} = new SpiderBotApi();

		ClientWebSocket Socket;
		const string DefaultHost = "192.168.1.1";
		const int DefaultPort = 9300;
		public SpiderBotApi()
		{

		}

		public event EventHandler StateChanged;
		CancellationTokenSource cancelToken;
		public async Task<bool> Connect(string host = DefaultHost, int port = DefaultPort)
		{
			try {
				IsConnecting = true;
				StateChanged?.Invoke (this, EventArgs.Empty);
				Socket = new ClientWebSocket ();
				Socket.Options.KeepAliveInterval = TimeSpan.FromMinutes (10);
				cancelToken = new CancellationTokenSource ();
				var connectTask = Socket.ConnectAsync (new Uri ($"ws://{host}:{port}"), CancellationToken.None);
				await connectTask;
				//Listening blows up on mono
				//listentingTask = StartListening();
				heartBeat = HeartBeat ();
				StateChanged?.Invoke (this, EventArgs.Empty);
				return Socket.State == WebSocketState.Open;
			} catch (Exception ex) {
				Console.WriteLine (ex);
			} finally {
				IsConnecting = false;
				StateChanged?.Invoke (this, EventArgs.Empty);
			}
			return false;
		}
		public bool IsConnecting { get; private set;}

		public bool IsConnected
		{
			get { return Socket?.State == WebSocketState.Open;}
		}

		public async Task<bool> Close()
		{
			await Socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
			cancelToken.Cancel();
			return true;
		}

		public async Task<bool> SendCommand(BotCommand command)
		{
			var message = $"[\"command\",{{\"command\":\"{command.ToString()}\"}}]";
            var success = await SendMessage(message);
			if (success && command.CommandDurration > 0)
				await Task.Delay(command.CommandDurration);
			return success;
		}

		public async Task<bool> SendMessage(string message)
		{
			try
			{
				var packet = Encoding.UTF8.GetBytes(message);
				await Socket.SendAsync(new ArraySegment<byte>(packet), WebSocketMessageType.Text, true, cancelToken.Token);
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			return false;
		}

		Task heartBeat;
		readonly BotCommand HeartBeatCommand = new BotCommand {Command = BotCommandsConstants.HeartBeat};
		async Task HeartBeat()
		{
			while (Socket.State == WebSocketState.Open)
			{
				await SendCommand(HeartBeatCommand);
				await Task.Delay(1000);
			}
			StateChanged?.Invoke(this, EventArgs.Empty);
		}

		public Action<string> MessageRecieved { get; set; }
		bool IsListening { get; set; }
		Task listentingTask;
		async Task StartListening()
		{
			if (IsListening)
				return;
			IsListening = true;
			try
			{
				var buffer = new byte[1024];
				while (Socket.State == WebSocketState.Open)
				{
					cancelToken.Token.ThrowIfCancellationRequested();
					var sb = new StringBuilder();
					try
					{
						bool isComplete = false;
						while (!isComplete)
						{
							var result = await Socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
							isComplete = result.EndOfMessage;
							if (result.MessageType == WebSocketMessageType.Close)
							{
								await Close();
								return;
							}

							var str = Encoding.UTF8.GetString(buffer,0, result.Count);
							sb.Append(str);
						}

						MessageRecieved?.Invoke(sb.ToString());

					}
					catch (Exception ex)
					{
						Console.WriteLine(ex);
						Console.WriteLine(sb);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
			finally
			{
				IsListening = false;
			}
		}
	}
}

