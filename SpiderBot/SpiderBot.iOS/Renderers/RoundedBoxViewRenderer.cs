using System;
using System.ComponentModel;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(Xamarin.Forms.RoundedBoxView), typeof(Xamarin.Forms.Platform.iOS.RoundedBoxViewRenderer))]
namespace Xamarin.Forms.Platform.iOS
{
	public class RoundedBoxViewRenderer : VisualElementRenderer<RoundedBoxView>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<RoundedBoxView> e)
		{
			base.OnElementChanged(e);

			if (Element == null)
				return;
			SetBackgroundColor(Element.BackgroundColor);
			SetBorderColor(Element.BorderColor);
			SetBorderWidth(Element.BorderWidth);
			SetBorderRadius(Element.BorderRadius);
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			if (e.PropertyName == BoxView.ColorProperty.PropertyName)
				SetBackgroundColor(Element.BackgroundColor);
			else if (e.PropertyName == RoundedBoxView.BorderColorProperty.PropertyName)
				SetBorderColor(Element.BorderColor);
			else if (e.PropertyName == RoundedBoxView.BorderWidthProperty.PropertyName)
				SetBorderWidth(Element.BorderWidth);
			else if (e.PropertyName == RoundedBoxView.BorderRadiusProperty.PropertyName)
				SetBorderRadius(Element.BorderRadius);
			else if (e.PropertyName == BoxView.IsVisibleProperty.PropertyName && Element.IsVisible)
				SetNeedsDisplay();
		}

		protected override void SetBackgroundColor(Color color)
		{
			this.BackgroundColor = color.ToUIColor();
		}
		protected virtual void SetBorderColor(Color color)
		{
			this.Layer.BorderColor = color.ToCGColor();
		}

		protected virtual void SetBorderWidth(double borderWidth)
		{
			this.Layer.BorderWidth = (nfloat)borderWidth;
		}

		protected virtual void SetBorderRadius(double radius)
		{
			this.Layer.CornerRadius = (nfloat)radius;
		}
	}
}