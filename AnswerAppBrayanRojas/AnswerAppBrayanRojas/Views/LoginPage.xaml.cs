using AnswerAppBrayanRojas.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AnswerAppBrayanRojas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        UserViewModel Vm;
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = Vm = new UserViewModel();
        }

        private void CmdSeePassword(object sender, ToggledEventArgs e)
        {
            if (SwSeePassword.IsToggled)
            {
                TxtPassword.IsPassword = false;
            }
            else
            {
                TxtPassword.IsPassword = true;
            }
        }

        private async void CmdUserRegister(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserRegisterPage());//colocar el formulario por arriba el otro usuario
        }

        private async void BtnLoginClicked(object sender, EventArgs e)
        {
            bool R = await Vm.ValidateUserAccess(TxtUserName.Text.Trim(), TxtPassword.Text.Trim());
            if (R)
            {
                await DisplayAlert(":)", "User Access Granted!", "OK");
                await Navigation.PushAsync(new ActionsPage());
            }
            else
            {
                await DisplayAlert(":)", "Incorrect User Name or Password!", "OK");
                TxtPassword.Focus();
            }
        }

    }
}