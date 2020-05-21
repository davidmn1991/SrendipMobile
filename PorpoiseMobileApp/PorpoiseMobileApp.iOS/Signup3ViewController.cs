
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

namespace PorpoiseMobileApp.iOS
{
    public partial class Signup3ViewController : MvvmViewController<Signup3ViewModel>
    {
    
        public Signup3ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();


            birthlbl.TextColor = UIColor.FromRGB(112, 112, 112);
        

            NavigationController.NavigationBarHidden = true;


            backButton.TouchUpInside += (sender, e) => {
                ViewModel.BackSignup2();
            };

            var screenTap1 = new UITapGestureRecognizer(() =>
            {
                datePicker.ResignFirstResponder();


            });

            this.View.AddGestureRecognizer(screenTap1);

            nextButton.TouchUpInside += (sender, e) => {
                if(!string.IsNullOrEmpty(ViewModel.gender) && !string.IsNullOrEmpty(datePicker.Text))
                {
                    //ViewModel.date_of_birth = datePicker.Text;

                    ViewModel.ShowSignup4();

                }
                else if(string.IsNullOrEmpty(ViewModel.gender))

                {
                    var okAlertController = UIAlertController.Create("Error message", "Please select sexe", UIAlertControllerStyle.Alert);

                    //Add Action
                    okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                    // Present Alert
                    PresentViewController(okAlertController, true, null);
                }

                else if (string.IsNullOrEmpty(datePicker.Text))

                {
                    var okAlertController = UIAlertController.Create("Error message", "Please select date of birth", UIAlertControllerStyle.Alert);

                    //Add Action
                    okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                    // Present Alert
                    PresentViewController(okAlertController, true, null);
                }

            };

            malebutton.TouchUpInside += (sender, e) => {
                malebutton.SetBackgroundImage(UIImage.FromBundle("male_selected"), UIControlState.Normal);
                femaleButton.SetBackgroundImage(UIImage.FromBundle("Female"), UIControlState.Normal);
                otherButton.SetBackgroundImage(UIImage.FromBundle("Other"), UIControlState.Normal);
                ViewModel.gender = "MALE";
                
            };

            femaleButton.TouchUpInside += (sender, e) => {
                femaleButton.SetBackgroundImage(UIImage.FromBundle("female_selected"), UIControlState.Normal);
                otherButton.SetBackgroundImage(UIImage.FromBundle("Other"), UIControlState.Normal);
                malebutton.SetBackgroundImage(UIImage.FromBundle("Male"), UIControlState.Normal);
                ViewModel.gender = "FEMALE";
            };

            otherButton.TouchUpInside += (sender, e) => {
                otherButton.SetBackgroundImage(UIImage.FromBundle("other_selected"), UIControlState.Normal);
                femaleButton.SetBackgroundImage(UIImage.FromBundle("Female"), UIControlState.Normal);
                malebutton.SetBackgroundImage(UIImage.FromBundle("Male"), UIControlState.Normal);
                ViewModel.gender = "OTHER";
            };

 

            UIDatePicker dpPurchaseDate;
            dpPurchaseDate = new UIDatePicker(new CGRect(0, 44.0f, this.View.Bounds.Width, 260));
            dpPurchaseDate.Mode = UIDatePickerMode.Date;
            dpPurchaseDate.MaximumDate = NSDate.Now;
            dpPurchaseDate.TimeZone = NSTimeZone.LocalTimeZone;
            dpPurchaseDate.UserInteractionEnabled = true;
            dpPurchaseDate.BackgroundColor = UIColor.White;
            DateTime Date = new DateTime(1999, 01, 30);
            dpPurchaseDate.Date = DateTimeToNSDate(Date);




            var toolbar = new UIToolbar(new CGRect(0.0f, 0.0f, dpPurchaseDate.Frame.Size.Width, 44.0f));

            toolbar.BackgroundColor = UIColor.White;

            UIView pDateView;

            pDateView = new UIView(new CGRect(0, 0, this.View.Bounds.Width, 260 + 44.0f));

            toolbar.Items = new[]
            {
            new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
            new UIBarButtonItem("Done",
                UIBarButtonItemStyle.Done,
                delegate {

                    Console.WriteLine("presing here");

                datePicker.ResignFirstResponder();
                dpPurchaseDate.ResignFirstResponder();
                dpPurchaseDate.Hidden = true;
                pDateView.Hidden = true;
                Console.WriteLine(dpPurchaseDate.Date.ToString());

                var dateString = dpPurchaseDate.Date.ToString();
                var formatted = DateTime.Parse(dateString);
                Console.WriteLine(formatted.ToString("dd,MM,yyyy"));
                datePicker.Text = formatted.ToString("dd-MMMM-yyyy");

                ViewModel.date_of_birth = formatted.ToString("yyyy-MM-dd");
                    //ResignFirstResponder();
                })
                };

            pDateView.AddSubview(dpPurchaseDate);
            pDateView.AddSubview(toolbar);

         
            datePicker.EditingDidBegin += delegate {
                pDateView.Hidden = false;
                dpPurchaseDate.Hidden = false;
                datePicker.InputView = pDateView;

                
            };
            var screenTap = new UITapGestureRecognizer(() =>
            {
                datePicker.ResignFirstResponder();
                dpPurchaseDate.ResignFirstResponder();
                dpPurchaseDate.Hidden = true;
                pDateView.Hidden = true;
                Console.WriteLine(dpPurchaseDate.Date.ToString());

                var dateString = dpPurchaseDate.Date.ToString();
                var formatted = DateTime.Parse(dateString);
                Console.WriteLine(formatted.ToString("dd,MM,yyyy"));
                datePicker.Text = formatted.ToString("dd-MMMM-yyyy");

                ViewModel.date_of_birth = formatted.ToString("yyyy-MM-dd");

            });


            View.AddGestureRecognizer(screenTap);


        }

        public static NSDate DateTimeToNSDate(DateTime date)
        {
            DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(
                new DateTime(2000, 1, 1, 0, 0, 0));
            return NSDate.FromTimeIntervalSinceReferenceDate(
                (date - reference).TotalSeconds);
        }
    }
}