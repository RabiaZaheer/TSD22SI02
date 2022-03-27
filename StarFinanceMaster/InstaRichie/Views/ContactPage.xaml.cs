using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SQLite;
using StartFinance.Models;
using Windows.UI.Popups;
using SQLite.Net;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace StartFinance.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ContactPage : Page
    {
        SQLiteConnection conn; // adding an SQLite connection
        string path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Findata.sqlite");

        public ContactPage()
        {
            try
            {
                this.InitializeComponent();
                NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
                /// Initializing a database
                conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
                Results();
            }
            catch (Exception ex)
            {
                MessageDialog dialog = new MessageDialog(ex.Message);

            }
        }
        public void Results()
        {
            // Creating table
            try
            {
                conn.CreateTable<ContactInfo>();

                /// Refresh Data
                var query = conn.Table<ContactInfo>();
                ContactListView.ItemsSource = query.ToList();
            }
            catch (Exception ex)
            {
                MessageDialog dialog = new MessageDialog(ex.Message);

            }
        }
        private async void AddData(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtFName.Text.ToString() == "" && txtLastName.Text.ToString() == "" && txtCompany.Text.ToString() == "" && txtMobileNo.Text.ToString() == "")
                {
                    MessageDialog dialog = new MessageDialog("All fields are required");
                    await dialog.ShowAsync();
                }
                else
                {
                    conn.Insert(new ContactInfo()
                    {
                        FirstName = txtFName.Text,
                        LastName = txtLastName.Text,
                        CompanyName = txtCompany.Text,
                        MobileNo = txtMobileNo.Text
                    });
                    Results();
                }
            }
            catch (Exception ex)
            {
                if (ex is FormatException)
                {
                    MessageDialog dialog = new MessageDialog("You forgot to enter the Value or entered an invalid data", "Oops..!");
                    await dialog.ShowAsync();
                }
                else if (ex is SQLiteException)
                {
                    MessageDialog dialog = new MessageDialog("A Similar Asset Nane already exists, Try a different name", "Oops..!");
                    await dialog.ShowAsync();
                }
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Results();
            }
            catch (Exception ex)
            {
                MessageDialog dialog = new MessageDialog(ex.Message);

            }
        }
        private async void DeleteAccout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string AccSelection = ((ContactInfo)ContactListView.SelectedItem).FirstName;
                if (AccSelection == "")
                {
                    MessageDialog dialog = new MessageDialog("Not selected the Item", "Oops..!");
                    await dialog.ShowAsync();
                }
                else
                {
                    conn.CreateTable<ContactInfo>();
                    var query1 = conn.Table<ContactInfo>();
                    var query3 = conn.Query<ContactInfo>("DELETE FROM ContactInfo WHERE FirstName ='" + AccSelection + "'");
                    ContactListView.ItemsSource = query1.ToList();
                }
            }
            catch (NullReferenceException)
            {
                MessageDialog dialog = new MessageDialog("Not selected the Item", "Oops..!");
                await dialog.ShowAsync();
            }
        }
    }
}
