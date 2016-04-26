using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace SpiderBot
{
	public class App : Application
	{
		public App ()
		{
			// The root page of your application
            var viewModel =  new GamePadViewModel();
            MainPage = new GamePadPage { BindingContext = viewModel};

            // Hack to allow the app some time to startup before allowing the joystick to work.
            Device.StartTimer(TimeSpan.FromSeconds (3), () =>
            {
                viewModel.Initialized = true;
                return false;
            });
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
