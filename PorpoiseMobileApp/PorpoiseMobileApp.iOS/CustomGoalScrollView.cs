// This file has been autogenerated from a class added in the UI designer.

using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace PorpoiseMobileApp.iOS
{
	public partial class CustomGoalScrollView : UIScrollView, IUIGestureRecognizerDelegate
	{
		public CustomGoalScrollView(IntPtr handle) : base(handle)
		{
		}

		//[Export("gestureRecognizer:shouldRecognizeSimultaneouslyWithGestureRecognizer:")]
		//public bool ShouldRecognizeSimultaneously(UIGestureRecognizer gestureRecognizer, UIGestureRecognizer otherGestureRecognizer)
		//{

		//}
		public override void TouchesEnded(NSSet touches, UIEvent evt)
		{
			var touch = touches.AnyObject as UITouch;

			base.TouchesEnded(touches, evt);
		}
		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			base.TouchesBegan(touches, evt);

		}
		public override bool TouchesShouldBegin(NSSet touches, UIEvent withEvent, UIView inContentView)
		{
			//var touch = touches.AnyObject as UITouch;
			//if (touch.LocationInView(this) == inContentView.Frame.Location)
			//{
			//	return false;
			//}
			//else {
			return base.TouchesShouldBegin(touches, withEvent, inContentView);
			//}

		}
		public override bool GestureRecognizerShouldBegin(UIGestureRecognizer gestureRecognizer)
		{

			return base.GestureRecognizerShouldBegin(gestureRecognizer);
		}
		public override void TouchesCancelled(NSSet touches, UIEvent evt)
		{
			base.TouchesCancelled(touches, evt);
		}
		public override UIView HitTest(CGPoint point, UIEvent uievent)
		{
			return base.HitTest(point, uievent);
		}

	}
}