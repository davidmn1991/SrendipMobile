using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using CoreGraphics;
using Foundation;
using PorpoiseMobileApp.Converters;
using PorpoiseMobileApp.iOS.Services;
using PorpoiseMobileApp.iOS.Utils;
using PorpoiseMobileApp.Models;
using PorpoiseMobileApp.ViewModels;
using UIKit;

namespace PorpoiseMobileApp.iOS
{
    public partial class PetListViewController : MvvmViewController<PetListViewModel>
    {
        public PetListViewController(IntPtr handle) : base(handle)
        {
        }

        LoadingOverlay loadpop;

        //public static List<Bubbledetails> bubble_details = new List<Bubbledetails>();

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            try
            {

                Console.WriteLine(AddPetViewModel.PetList.Count);

                Console.WriteLine(AddPetViewModel.PetList[0].PetName);

                Console.WriteLine(AddPetViewModel.PetList[0].PetBytes);
            }
            catch (Exception ex)
            {

            }

            addPet_button.TouchUpInside += delegate
            {

                ViewModel.ShowAddPet();


            };

            addBubble_button.TouchUpInside += delegate
            {
                if (PetListViewModel.bubble_details.Count > 0)
                {

                    ViewModel.AddPetsBubble();

                }


            };

            back_button.TouchUpInside += delegate
            {

                ViewModel.ShowBubbleBoard();


            };

            petslbl.TextColor = UIColor.FromRGB(112, 112, 112);

            var GoBack = new UITapGestureRecognizer(() =>
            {
                try
                {
                    ViewModel.ShowBubbleBoard();
                }
                catch (Exception ex)
                {

                }

            });
            petslbl.AddGestureRecognizer(GoBack);

            petslbl.UserInteractionEnabled = true;

            petlist_tableview.Source = new TableSource(AddPetViewModel.PetList);

            addPet_button.BackgroundColor = UIColor.FromRGB(75, 171, 229);

            addBubble_button.BackgroundColor = UIColor.FromRGB(75, 171, 229);

        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            NavigationBarSetUp();



            this.HidesBottomBarWhenPushed = true;
            this.NavigationController.NavigationBarHidden = false;

        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);



            try
            {

                if (NavigationController.NavigationBar != null)
                {
                    this.HidesBottomBarWhenPushed = false;
                    this.NavigationController.NavigationBarHidden = true;
                }
            }
            catch (Exception)
            {

            }
        }

        public async void NavigationBarSetUp()
        {
            if (this.NavigationController != null)
            {
                //TODO: figure out how to make the status bar opaque.

                this.NavigationController.NavigationBar.BarStyle = UIBarStyle.BlackOpaque;
                this.NavigationController.NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
                this.NavigationController.NavigationBar.ShadowImage = new UIImage();
                this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(100, 170, 224);
            }
            else
            {

            }
            NavigationController.NavigationBar.Hidden = false;
            NavigationController.NavigationBar.Translucent = false;
            float imageSize = 20f;

            float gap = 5f;

            float borderSize = 0f;

            float textHeight = 1f;

            float buttonWidth = 60;

            float buttonHeight = borderSize * 2 + gap * 3 + imageSize + textHeight;

            float imageOrigin = borderSize + gap;

            float textTop = imageOrigin + imageSize + gap;

            float textBottom = borderSize + gap;

            float imageBottom = textBottom + textHeight + gap;

            UIButton rigthButton = UIButton.FromType(UIButtonType.Custom);

            rigthButton.Center = this.View.Center;

            //Image

            UIImage logout = new UIImage("logo.png");

            rigthButton.Frame = new CGRect(0, 15, buttonWidth, buttonHeight + 10);

            rigthButton.SetImage(logout, UIControlState.Normal);

            rigthButton.ImageEdgeInsets = new UIEdgeInsets(0, 15, 0, 10);

            rigthButton.ContentEdgeInsets = new UIEdgeInsets(0, 0, 15, 0);

            rigthButton.SetTitle(Resource.Logout, UIControlState.Normal);

            rigthButton.TitleEdgeInsets = new UIEdgeInsets(textTop, -logout.Size.Width, textBottom, 0.0f);

            rigthButton.TitleLabel.Font = UIFont.FromName("Ubuntu-Light", 15f);

            rigthButton.TouchUpInside += delegate
            {
                var user = NSUserDefaults.StandardUserDefaults;

                user.SetBool(true, "logged");

                //ViewModel.Logout();

            };


            UIImage post = new UIImage("notification_bell.png");

            //UIImage post = new UIImage("HomeIcon.png");

            UIImage right_image = new UIImage("menu_icon.png");

            UIImageView bar_image = new UIImageView(UIImage.FromBundle("DIP_logo.jpg"));

            UIView container = new UIView();

            UIView center_view = new UIView();

            container.Frame = new CGRect(20, 18, this.View.Frame.Width / 14, this.View.Frame.Height / 23);

            UILabel notification = new UILabel();

            //notification.Text = ViewModel.Notificationscount.ToString();

            notification.Font = UIFont.FromName("Helvetica-Bold", 12f);

            notification.TextColor = UIColor.White;

            notification.TextAlignment = UITextAlignment.Center;

            notification.Frame = new CGRect((container.Frame.Width / 2) - ((this.View.Frame.Width / 16) / 2), (container.Frame.Height / 2) - ((this.View.Frame.Height / 23) / 2), this.View.Frame.Width / 16, this.View.Frame.Height / 23);

            container.Layer.CornerRadius = container.Frame.Height / 2;

            //NavigationItem.LeftBarButtonItem = new UIBarButtonItem(notification);

            UIButton leftButton = UIButton.FromType(UIButtonType.Custom);

            UIButton rightButton = UIButton.FromType(UIButtonType.Custom);

            leftButton.UserInteractionEnabled = true;


            //this.View.InsertSubview(new UIImageView(UIImage.FromBundle("Images/bg.jpg")), 0);

            //container.AddSubview(notification);

            //container.BackgroundColor = PorpoiseColors.Magenta;

            leftButton.Bounds = new CGRect(0, 0, post.Size.Width, post.Size.Height);

            leftButton.Frame = new CGRect(0, 0, 30, 30);

            leftButton.SetImage(post, UIControlState.Normal);

            leftButton.SetImage(post, UIControlState.Disabled);

            rightButton.Bounds = new CGRect(0, 0, right_image.Size.Width, right_image.Size.Height);

            rightButton.Frame = new CGRect(0, 0, 30, 30);

            rightButton.SetImage(right_image, UIControlState.Normal);

            rightButton.SetImage(right_image, UIControlState.Disabled);

            var leftButtonWidth = leftButton.WidthAnchor.ConstraintEqualTo(post.Size.Width);

            var leftButtonHeight = leftButton.HeightAnchor.ConstraintEqualTo(post.Size.Height);

            var rightButtonWidth = rightButton.WidthAnchor.ConstraintEqualTo(right_image.Size.Width);

            var rightButtonHeight = rightButton.HeightAnchor.ConstraintEqualTo(right_image.Size.Height);

            leftButtonWidth.Active = true;

            leftButtonHeight.Active = true;

            rightButtonWidth.Active = true;

            rightButtonHeight.Active = true;

            //leftButton.AddSubview(container);


            leftButton.TouchUpInside += delegate


            {
                //ViewModel.ShowSettings();
                Console.WriteLine("pushing here");
                //ViewModel.GoToNotifications();
            };

            rightButton.Hidden = true;


            NavigationItem.LeftBarButtonItem = new UIBarButtonItem(leftButton);

            NavigationItem.RightBarButtonItem = new UIBarButtonItem(rightButton);


            //UILabel label = new UILabel();

            //NavigationItem.LeftBarButtonItem.CustomView.Subviews.Append(label);

            //NavigationItem.Title = "Porpoise";

            NavigationItem.TitleView = new UIImageView(UIImage.FromBundle("DIP_logo.png"));
        }
    }

    public class TableSource : UITableViewSource
    {

        List<PetsList> TableItems;
        string CellIdentifier = "TableCell";

        public TableSource(List<PetsList> items)
        {
            TableItems = items;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return TableItems.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
            string item = TableItems[indexPath.Row].PetName;

            //if there are no cells to reuse, create a new one
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier);
            }

            cell.TextLabel.Text = item;

            cell.TextLabel.TextColor = UIColor.FromRGB(112, 112, 112);

            return cell;
        }
    }
}