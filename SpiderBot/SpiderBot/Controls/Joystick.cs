using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace SpiderBot
{
	public class Joystick : Layout<View>
	{
		protected RoundedBoxView Thumb;
		protected RoundedBoxView BackgroundView;

		public Joystick ()
		{
			this.Children.Add (BackgroundView = new RoundedBoxView {
				WidthRequest = 100,
				BackgroundColor = Color.Gray.MultiplyAlpha (.1),
				BorderColor = Color.Black,
				BorderWidth = 1,
			});

			this.Children.Add (Thumb = new RoundedBoxView {
				WidthRequest = 50,
				BackgroundColor = Color.Black.MultiplyAlpha (.3),
				BorderColor = Color.Black.MultiplyAlpha (.5),
				BorderWidth = 1,
			});
			var panGesture = new PanGestureRecognizer ();
			panGesture.PanUpdated += OnPanUpdated;
			GestureRecognizers.Add (panGesture);

		}
		void OnPanUpdated (object sender, PanUpdatedEventArgs e)
		{
			var p = Center;
			p.X += e.TotalX;
			p.Y += e.TotalY;
			switch (e.StatusType) {
			case GestureStatus.Started:
				this.BeingTouch (p);
				return;
			case GestureStatus.Running:
				this.MoveJoystick (p);
				return;
			default:
				this.Stop ();
				return;
			}
		}

		double thumbRadius;
		Rectangle lastBounds;

		protected override SizeRequest OnSizeRequest (double widthConstraint, double heightConstraint)
		{
			var r = new SizeRequest (new Size (Math.Max (WidthRequest, 100), Math.Max (100, HeightRequest)), new Size (100, 100));
			return r;
		}
		protected override void LayoutChildren (double x, double y, double width, double height)
		{
			var bounds = new Rectangle (x, y, width, height);
			if (lastBounds == bounds)
				return;
			lastBounds = bounds;
			var w = Math.Min (width, height);
			var center = bounds.Center;
			var half = w / 2;
			var bRect = new Rectangle (center.X - half, center.Y - half, Radius * 2, Radius * 2);

			BackgroundView.Layout (bRect);
			thumbRadius = width / 4;
			Thumb.Layout (new Rectangle (0, 0, Radius, Radius));
			UpdateThumb ();
		}

		void UpdateThumb ()
		{
			if (Radius == 0)
				return;
			var xOffset = double.IsNaN (XValue) ? 0 : XValue * thumbRadius;
			var yOffset = double.IsNaN (YValue) ? 0 : YValue * thumbRadius;
			var center = lastBounds.Center;
			var bounds = new Rectangle (ThumbCenter.X, ThumbCenter.Y, Radius, Radius);

			bounds.X -= (thumbRadius + xOffset);
			bounds.Y -= thumbRadius - yOffset;
			Thumb.Layout (bounds);
		}

		public static readonly BindableProperty XValueProperty = BindableProperty.Create (nameof (XValue), typeof (float), typeof (Joystick), 0f);

		public float XValue {
			get { return (float)GetValue (XValueProperty); }
			set {
				SetValue (XValueProperty, value);
				this.OnPropertyChanged ();
			}
		}

		public static readonly BindableProperty YValueProperty = BindableProperty.Create (nameof (YValue), typeof (float), typeof (Joystick), 0f);

		public float YValue {
			get { return (float)GetValue (YValueProperty); }
			set {
				SetValue (YValueProperty, value);
				this.OnPropertyChanged ();
			}
		}


		public static readonly BindableProperty ThumbCenterProperty = BindableProperty.Create (nameof (ThumbCenter), typeof (Point), typeof (Joystick), Point.Zero);

		public Point ThumbCenter {
			get { return (Point)GetValue (ThumbCenterProperty); }
			set {
				SetValue (ThumbCenterProperty, value);
				UpdateValues ();
				this.OnPropertyChanged ();
			}
		}

		public double Radius { get; set; }
		protected override void OnSizeAllocated (double width, double height)
		{
			base.OnSizeAllocated (width, height);
			if (width <= 0 || height <= 0)
				return;
			var bounds = new Rectangle (0, 0, width, height);
			var w = Math.Min (width, height);
			var center = bounds.Center;
			var half = w / 2;
			var bRect = new Rectangle (center.X - half, center.Y - half, w, w);

			BackgroundView.BorderRadius = half;
			BackgroundView.Layout (bRect);
			thumbRadius = w / 4;
			Thumb.HeightRequest = Thumb.WidthRequest = half;
			Thumb.BorderRadius = thumbRadius;
			ThumbCenter = Center;
			Radius = half;


			UpdateThumb ();

		}

		public Point Center => new Point (Width / 2, Height / 2);
		public bool IsMoving { get; set; }

		public double DistanceFromJoyPad (Point point)
		{
			return point.Distance (Center);
		}

		public bool IsTouchingJoystick (Point point)
		{
			var length = DistanceFromJoyPad (point);
			return length < Radius;
		}
		Point touchPos;

		public void MoveJoystick (Point point)
		{
			MoveJoystick (point, IsMoving ? Center : ThumbCenter);
		}
		public void MoveJoystick (Point point, Point prev)
		{
			var offset = point.Subtract (prev);

			//touchPos = touchPos.Add(offset);
			touchPos = point;
			var delta = touchPos.Subtract (Center);
			var newPos = touchPos;
			var angle = delta.ToAngle ();
			var joybtnDist = DistanceFromJoyPad (newPos);
			if (joybtnDist > Radius) {
				var direction = PointHelper.ForAngle (angle);
				newPos = Center.Add (direction.Multiply (Radius));
				joybtnDist = Radius;
			}
			ThumbCenter = newPos;
		}

		public void BeingTouch (Point point)
		{
			if (IsMoving)
				return;
			IsMoving = true;
			touchPos = Center;
			MoveJoystick (point);

		}

		public void Stop ()
		{

			IsMoving = false;
			ThumbCenter = Center;
			UpdateValues ();
			this.InvalidateLayout ();
		}

		void UpdateValues ()
		{
			var distance = ThumbCenter.Subtract (Center).Multiply (1 / Radius);
			XValue = (float)Math.Round (distance.X, 2);
			YValue = (float)Math.Round (distance.Y, 2) * -1;
			UpdateThumb ();
			Debug.WriteLine ($"X = {XValue} Y = {YValue}");
		}


	}
}

