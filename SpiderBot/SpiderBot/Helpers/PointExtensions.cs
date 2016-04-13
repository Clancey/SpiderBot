using System;
using Xamarin.Forms;
namespace Xamarin.Forms
{
	public static class PointHelper
	{
		public static Point Subtract(this Point p, Point point)
		{
			return new Point(p.X - point.X, p.Y - point.Y);
		}

		public static Point Add(this Point p, Point point)
		{
			return new Point(p.X + point.X, p.Y + point.Y);
		}


		public static Point Multiply(this Point p, Point point)
		{
			return new Point(p.X * point.X, p.Y * point.Y);
		}

		public static Point Multiply(this Point p, double value)
		{
			return new Point(p.X * value, p.Y * value);
		}


		public static Point ForAngle(double angle)
		{
			return new Point(Math.Cos(angle), Math.Sin(angle));
		}


		public static double ToAngle(this Point p)
		{
			return Math.Atan2(p.Y, p.X);
		}

		public static Point LengthSq(this Point p)
		{
			return p.Multiply(p);
		}
	}
}

