using System;
using System.Diagnostics;
using System.Globalization;
using Foundation;
using MvvmCross.Platform.Converters;
using UIKit;

namespace PorpoiseMobileApp.iOS.Converters
{
    public class ByteUImageConverter : IMvxValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null){

				var data = NSData.FromArray(value as byte[]);
				var uiimage = UIImage.LoadFromData(data);
                Debug.WriteLine("Returning valid image");
                return uiimage;

            }
            Debug.WriteLine("RETURNING NULL");
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
