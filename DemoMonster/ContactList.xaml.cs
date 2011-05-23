using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Microsoft.Phone.UserData;

namespace DemoMonster
{
    public partial class ContactList : PhoneApplicationPage
    {
        public ContactList()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(ContactList_Loaded);
        }

        private void ContactList_Loaded(object sender, RoutedEventArgs e)
        {
            Contacts c = new Contacts();
            c.SearchCompleted += c_SearchCompleted;
            c.SearchAsync("", FilterKind.None, c);
        }

        private void c_SearchCompleted(object sender, ContactsSearchEventArgs e)
        {
            ContactsListBox.ItemsSource = e.Results;
        }

        private void ContactsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Contact c = ContactsListBox.SelectedItem as Contact;
            if (c != null)
            {
                var name = c.DisplayName;
                Uri uri = new Uri("/Details.xaml?name=" + name, UriKind.Relative);
                NavigationService.Navigate(uri);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddressChooserTask task = new AddressChooserTask();
            task.Completed += (s, a) =>
            {
                if (a.TaskResult == TaskResult.OK)
                    MessageBox.Show(a.Address, a.DisplayName, MessageBoxButton.OK);
            };
            task.Show();
        }
    }
}