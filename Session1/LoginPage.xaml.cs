using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Session1GlobalLibrary;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Session1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            entryUsername.Text = string.Empty;
            entryPassword.Text = string.Empty;
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            var webApi = new API();
            var getUser = await webApi.Login(entryUsername.Text, entryPassword.Text);
            if (getUser != null)
            {
                await DisplayAlert("Login", $"Login successful! Welcome {getUser.userName}!", "Ok");
                await Navigation.PushAsync(new AddResources());
            }
            else
            {
                await DisplayAlert("Login", $"Login unsuccessful! Please check your credentials and try again!", "Ok");
            }
        }
    }
}