using System;
using Xamarin.Forms;

namespace Xamarin.Forms
{
	public class RoundedBoxView : BoxView
	{
		public RoundedBoxView()
		{
			HeightRequest = 100;
			WidthRequest = 100;
		}
		public static readonly BindableProperty BorderColorProperty =
			BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(RoundedBoxView), Color.Default);

		public Color BorderColor
		{
			get { return (Color)GetValue(BorderColorProperty); }
			set { SetValue(BorderColorProperty, value); }
		}

		public static readonly BindableProperty BorderWidthProperty =
			BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(RoundedBoxView), 0.0);

		public double BorderWidth
		{
			get { return (double)GetValue(BorderWidthProperty); }
			set { SetValue(BorderWidthProperty, value); }
		}

		public static readonly BindableProperty BorderRadiusProperty =
			BindableProperty.Create(nameof(BorderRadius), typeof(double), typeof(RoundedBoxView), 0.0);

		public double BorderRadius
		{
			get { return (double)GetValue(BorderRadiusProperty); }
			set { SetValue(BorderRadiusProperty, value); }
		}

	}
}

