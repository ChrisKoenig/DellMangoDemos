using System;
using System.Device.Location;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.UserData;

namespace DemoMonster
{
    public class AddressDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            CivicAddress address = value as CivicAddress;
            return String.Format("{0}{1}{2}, {3} {4}", address.AddressLine1, Environment.NewLine, address.City, address.StateProvince, address.PostalCode);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}