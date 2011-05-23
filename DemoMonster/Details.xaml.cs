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
using Microsoft.Phone.Shell;
using Microsoft.Phone.UserData;

namespace DemoMonster
{
    public partial class Details : PhoneApplicationPage
    {
        public Details()
        {
            InitializeComponent();
            Loaded += Details_Loaded;
        }

        private void Details_Loaded(object sender, RoutedEventArgs e)
        {
            var name = NavigationContext.QueryString["name"];
            Contacts c = new Contacts();
            c.SearchCompleted += c_SearchCompleted;
            c.SearchAsync(name, FilterKind.DisplayName, name);
        }

        private void c_SearchCompleted(object sender, ContactsSearchEventArgs e)
        {
            if (e.Results.Count() > 0)
            {
                var contact = e.Results.First();
                this.DataContext = contact;
            }
            else
            {
                MessageBox.Show("No results found for key " + e.State);
            }
        }

        private void PinButton_Click(object sender, RoutedEventArgs e)
        {
            var contact = this.DataContext as Contact;
            var key = contact.DisplayName;
            Uri uri = new Uri("/Details.xaml?name=" + key, UriKind.Relative);
            var item = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri == uri);
            var initialData = GetInitialData(contact);
            if (item == null) // there is no pinned item for this contact
            {
                ShellTile.Create(uri, initialData);
            }
            else // update the pinned item with a new count
            {
                item.Update(initialData);
            }
        }

        private StandardTileData GetInitialData(Contact contact)
        {
            return new StandardTileData
            {
                Title = contact.DisplayName,
                Count = GetRandomNumber1to10(),
                BackgroundImage = new Uri("/Images/DellFront.png", UriKind.Relative),
                BackBackgroundImage = new Uri("/Images/DellBack.png", UriKind.Relative),
                BackTitle = DateTime.Now.ToShortDateString(),
            };
        }

        private int GetRandomNumber1to10()
        {
            Random r = new Random();
            return r.Next(1, 10);
        }
    }
}