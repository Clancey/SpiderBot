using System;
using System.ComponentModel;
using Android.Graphics;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(Xamarin.Forms.RoundedBoxView), typeof(Xamarin.Forms.Platform.Android.RoundedBoxViewRenderer))]
namespace Xamarin.Forms.Platform.Android
{
	public class RoundedBoxViewRenderer: VisualElementRenderer<RoundedBoxView>
	{
		public RoundedBoxViewRenderer()
		{
			AutoPackage = false;
			Background = new global::Android.Graphics.Drawables.GradientDrawable();
		}

		protected override void OnElementChanged(ElementChangedEventArgs<RoundedBoxView> e)
		{
			base.OnElementChanged(e);
			if (Element == null)
				return;
			UpdateBackgroundColor();
			UpdateRadius();
			UpdateBorder();
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == BoxView.ColorProperty.PropertyName || e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName)
				UpdateBackgroundColor();
			else if (e.PropertyName == RoundedBoxView.BorderRadiusProperty.PropertyName)
				UpdateRadius();
			else if(e.PropertyName == RoundedBoxView.BorderWidthProperty.PropertyName || e.PropertyName == RoundedBoxView.BorderColorProperty.PropertyName)
				UpdateBorder();
		}


		global::Android.Graphics.Drawables.GradientDrawable BackgroundGradient => Background as global::Android.Graphics.Drawables.GradientDrawable;
		void UpdateBackgroundColor()
		{
			var colorToSet = Element.Color;

			if (colorToSet == Color.Default)
			{
				colorToSet = Element.BackgroundColor;
			}
			Background = new global::Android.Graphics.Drawables.GradientDrawable();
			BackgroundGradient?.SetColor(colorToSet.ToAndroid());
		}
		void UpdateBorder()
		{
			BackgroundGradient?.SetStroke((int)Element.BorderWidth, Element.BorderColor.ToAndroid());
		}
		void UpdateRadius()
		{
			BackgroundGradient?.SetCornerRadius((float)Element.BorderRadius * 4);
		}
	}
}
