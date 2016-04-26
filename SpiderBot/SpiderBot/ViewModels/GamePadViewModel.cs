using System;

using Xamarin.Forms;
using SpiderBot.BotCommands;
using System.Windows.Input;

namespace SpiderBot
{
	public class GamePadViewModel : BaseViewModel
	{
		SpiderBotApi Api = new SpiderBotApi ();

		public ICommand ConnectCommand {get;set;}
		public ICommand WaveCommand { get; set; }
		public ICommand WiggleCommand { get; set; }
		public ICommand WormCommand { get; set; }
		public ICommand DougieCommand { get; set; }
		public ICommand SalsaCommand { get; set; }
        public bool Initialized { get; set; }

		public GamePadViewModel ()
		{
			Api.StateChanged += (s, e) => ConnectionStateUpdated ();
			WaveCommand = new Command (Wave);
			WiggleCommand = new Command (Wiggle);
			WormCommand = new Command(Worm);
			DougieCommand = new Command(Dougie);
			SalsaCommand = new Command(Salsa);
		}

		void ConnectionStateUpdated ()
		{
			ProcPropertyChanged (nameof (IsConnected));
			ProcPropertyChanged (nameof (IsDisconnected));
			ProcPropertyChanged (nameof (IsConnecting));
			ProcPropertyChanged (nameof (ConnectionText));
			ProcPropertyChanged (nameof (CanConnect));
		}

        public bool NotInTransition => !IsConnecting && Initialized;
        public bool IsConnected => Api?.IsConnected ?? false;
		public bool IsDisconnected  => !IsConnected;
		public bool IsConnecting => Api?.IsConnecting ?? false;
		public bool CanConnect => !IsConnecting;
		public string ConnectionText => IsConnecting ? "Connecting" : IsConnected ? "Connected" : "Disconnected";

		float walkXValue;
		public float WalkXValue {
			get { return walkXValue; }

			set {
				if (ProcPropertyChanged (ref walkXValue, value))
					UpdateXValue();
			}
		}

		float walkYValue;
		public float WalkYValue {
			get { return walkYValue; }

			set {
				if (ProcPropertyChanged (ref walkYValue, value))
					UpdateYValue ();
			}
		}

		int heightValue;
		public int HeightValue
		{
			get { return heightValue; }
			set
			{
				if (ProcPropertyChanged(ref heightValue, value))
					UpdateHeightValue();
			}
		}

		int redColorValue;
		public int RedColorValue
		{
			get { return redColorValue; }
			set
			{
				if (ProcPropertyChanged(ref redColorValue, value))
					UpdateEyeColor();
			}
		}

		int greenColorValue;
		public int GreenColorValue
		{
			get { return greenColorValue; }
			set
			{
				if (ProcPropertyChanged(ref greenColorValue, value))
					UpdateEyeColor();
			}
		}

		int blueColorValue;
		public int BlueColorValue
		{
			get { return blueColorValue; }
			set
			{
				if (ProcPropertyChanged(ref blueColorValue, value))
					UpdateEyeColor();
			}
		}

		async void UpdateXValue ()
		{
            if (NotInTransition)
				await Api.SendCommand (new WalkCommand (Axis.X) { Amount = WalkXValue });
		}

		async void UpdateYValue ()
		{
            if (NotInTransition)
				await Api.SendCommand (new WalkCommand (Axis.Y) { Amount = WalkYValue });
		}

		async void UpdateHeightValue()
		{
            if (NotInTransition)
				await Api.SendCommand(new HeightCommand { Amount = HeightValue });
		}

		async void UpdateEyeColor()
		{
			if (IsConnected)
				await Api.SendCommand(new ChangeColorCommand() { Red = RedColorValue, Green = GreenColorValue, Blue = BlueColorValue });
		}

		public async void Wave ()
		{
			await Api.SendCommand (new SequenceCommand { Sequence = Sequences.Wave });
		}
		public async void Wiggle ()
		{
			await Api.SendCommand (new SequenceCommand { Sequence = Sequences.Wiggle });
		}
		public async void Worm()
		{
			await Api.SendCommand(new SequenceCommand { Sequence = Sequences.Worm });
		}
		public async void Dougie ()
		{
			await Api.SendCommand(new SequenceCommand { Sequence = Sequences.Dougie });
		}
		public async void Salsa()
		{
			await Api.SendCommand(new SequenceCommand { Sequence = Sequences.Salsa });
		} 
	}
}


