using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SpiderBot
{
	public class BaseViewModel : INotifyPropertyChanged, IDisposable
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public BaseViewModel ()
		{
			
		}
		public void Dispose ()
		{
			ClearEvents ();
		}

		internal bool ProcPropertyChanged<T> (ref T currentValue, T newValue, [CallerMemberName] string propertyName = "")
		{
			return PropertyChanged.SetProperty (this, ref currentValue, newValue, propertyName);
		}

		internal void ProcPropertyChanged (string propertyName)
		{
			if (PropertyChanged != null) {
				PropertyChanged (this, new PropertyChangedEventArgs (propertyName));
			}
		}

	
		public void ClearEvents ()
		{
			if (PropertyChanged == null)
				return;
			var invocation = PropertyChanged.GetInvocationList ();
			foreach (var p in invocation)
				PropertyChanged -= (PropertyChangedEventHandler)p;
		}
	}
}