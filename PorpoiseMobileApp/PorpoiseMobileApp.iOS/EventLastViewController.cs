using System;
using System.Diagnostics;
using System.Drawing;
using CoreGraphics;
using Foundation;
using PorpoiseMobileApp.Converters;
using PorpoiseMobileApp.iOS.Services;
using PorpoiseMobileApp.iOS.Utils;
using PorpoiseMobileApp.ViewModels;
using UIKit;
using System.Collections.Generic;


namespace PorpoiseMobileApp.iOS
{
    public partial class EventLastViewController : MvvmViewController<EventLastViewModel>
    {
        public EventLastViewController(IntPtr handle) : base(handle)
        {
        }


        public int male_state = 1;

        public int female_state = 1;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();


            distancelbl.TextColor = UIColor.FromRGB(112, 112, 112);

            agelbl.TextColor = UIColor.FromRGB(112, 112, 112);

            totalCostlbl.TextColor = UIColor.FromRGB(112, 112, 112);

            event_settingslbl.TextColor = UIColor.FromRGB(112, 112, 112);

            durationlbl.TextColor = UIColor.FromRGB(112, 112, 112);

            targetlbl.TextColor = UIColor.FromRGB(112, 112, 112);


            ageRange.RangeEnd = 70;
            ageRange.RangeStart = 18;
            ageRange.ShowRange = true;
            ageRange.Minimum = 18;
            ageRange.Maximum = 70;
            ageRange.ToolTipPlacement = Syncfusion.SfRangeSlider.iOS.SFToolTipPlacement.SFToolTipPlacementTopLeft;
            ageRange.TickPlacement = Syncfusion.SfRangeSlider.iOS.SFTickPlacement.SFTickPlacementNone;
            ageRange.TickFrequency = 0;

            distanceRange.RangeEnd = 2.0f;
            distanceRange.RangeStart = 0.1f;

            distanceRange.Minimum = 0.1f;
            distanceRange.Maximum = 2.0f;

            distanceRange.ToolTipPrecision = 2;

            distanceRange.ShowRange = false;

            distanceRange.ToolTipPlacement = Syncfusion.SfRangeSlider.iOS.SFToolTipPlacement.SFToolTipPlacementTopLeft;

            distanceRange.TickFrequency = 0;

            duration.RangeEnd = 30;
            duration.RangeStart = 1;
            duration.ShowRange = false;
            duration.Minimum = 1;
            duration.Maximum = 30;
            duration.ToolTipPlacement = Syncfusion.SfRangeSlider.iOS.SFToolTipPlacement.SFToolTipPlacementTopLeft;
            duration.TickPlacement = Syncfusion.SfRangeSlider.iOS.SFTickPlacement.SFTickPlacementNone;
            duration.TickFrequency = 0;
            duration.Value = 15;


            maleButton.TouchUpInside += (sender, e) =>
            {

                if (male_state == 1)
                {
                    male_state = 2;
                    maleButton.SetBackgroundImage(UIImage.FromBundle("male_selected"), UIControlState.Normal);
                    //femaleButton.SetBackgroundImage(UIImage.FromBundle("Female"), UIControlState.Normal);

                }

                else if (male_state == 2)
                {
                    male_state = 1;
                    maleButton.SetBackgroundImage(UIImage.FromBundle("Male"), UIControlState.Normal);
                }
            };


            femaleButton.TouchUpInside += (sender, e) =>
            {

                if (female_state == 1)
                {

                    female_state = 2;
                    femaleButton.SetBackgroundImage(UIImage.FromBundle("female_selected"), UIControlState.Normal);
                    //maleButton.SetBackgroundImage(UIImage.FromBundle("Male"), UIControlState.Normal);

                }

                else if (female_state == 2)
                {
                    female_state = 1;
                    femaleButton.SetBackgroundImage(UIImage.FromBundle("Female"), UIControlState.Normal);
                    //maleButton.SetBackgroundImage(UIImage.FromBundle("Male"), UIControlState.Normal);
                }

            };

            NavigationController.NavigationBarHidden = true;

            backButton.TouchUpInside += (sender, e) =>
            {
                ViewModel.BackSignup1();
            };

            create_event_button.TouchUpInside += (sender, e) =>
            {



                if (male_state == 1 && female_state == 1)
                {

                    var okAlertController = UIAlertController.Create("Error message", "Please select target sexe", UIAlertControllerStyle.Alert);

                    //Add Action
                    okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                    // Present Alert
                    PresentViewController(okAlertController, true, null);
                }



                else
                {

                    ViewModel.@event.genders = new List<string>();

                    if (male_state == 2)
                    {
                        ViewModel.@event.genders.Add("MALE");
                    }

                    if (female_state == 2)
                    {
                        ViewModel.@event.genders.Add("FEMALE");
                    }


                    ViewModel.@event.radius = distanceRange.RangeEnd;

                    ViewModel.@event.min_age = Convert.ToInt16(ageRange.RangeStart);

                    ViewModel.@event.max_age = Convert.ToInt16(ageRange.RangeEnd);

                    ViewModel.@event.duration_days = Convert.ToInt16(duration.RangeEnd);


                    Console.WriteLine(distanceRange.RangeEnd);

                    Console.WriteLine(Convert.ToInt16(ageRange.RangeStart));

                    Console.WriteLine(Convert.ToInt16(ageRange.RangeEnd));


                }

            };




            totalCostlbl.Layer.BorderColor = UIColor.Black.CGColor;

            totalCostlbl.Layer.BorderWidth = 1f;


        }




    }
}