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

    public partial class CreateAccount : ContentPage
    {
        List<User_Type> types;
        public CreateAccount()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            loadPicker();
        }

        private async void btnCreate_Clicked(object sender, EventArgs e)
        {
            if(entryReEnterPassword.Text != entryPassword.Text)
            {
                await DisplayAlert("Create Account", "Please check your password fields! They don't match", "Ok");
            }
            else
            {
                var getUserTypeID = (from x in types
                                     where x.userTypeName == pUserType.SelectedItem.ToString()
                                     select x.userTypeId).FirstOrDefault();
                var User = new User()
                {
                    userId = entryUserID.Text,
                    userName = entryUserName.Text,
                    userPw = entryReEnterPassword.Text,
                    userTypeIdFK = getUserTypeID
                };
                var webApi = new API();
                var response = await webApi.PostAsync("Users/Create", User);
                await DisplayAlert("Create Account", response, "Ok");
                await Navigation.PopAsync();
            }
            
        }
        private async void loadPicker()
        {
            var webApi = new API();
            types = await webApi.GetUserTypes();
            foreach (var item in types)
            {
                pUserType.Items.Add(item.userTypeName);
            }
        }
    }
}