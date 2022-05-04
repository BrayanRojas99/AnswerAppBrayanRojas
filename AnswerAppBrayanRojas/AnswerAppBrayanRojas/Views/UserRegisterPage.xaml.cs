using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AnswerAppBrayanRojas.Models;
using AnswerAppBrayanRojas.ViewModels;

namespace AnswerAppBrayanRojas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserRegisterPage : ContentPage
    {
        UserViewModel viewModel;
        public UserRegisterPage()
        {
            InitializeComponent();

            //estamos anclando la vista con el viewmodel respectivo
            BindingContext = viewModel = new UserViewModel();

            //llmar al metodo
            LoadRoles();
        }

        private async void CmdRegisterUser(object sender, EventArgs e)
        {
            await DisplayAlert("Success","The Registration was Successful","OK");
            //await Navigation.PopAsync();
        }

        private async void CmdCancelRegister(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
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

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            //todo: se debe de validar datos minimos, estructura del email, complejidad de contraseña

            bool R = await viewModel.AddUser(TxtUserName.Text.Trim(),
                                             TxtFisrtName.Text.Trim(),
                                             TxtLastName.Text.Trim(),
                                             TxtPhoneNumber.Text.Trim(),
                                             TxtPassword.Text.Trim(),
                                             TxtBackUpEmail.Text.Trim(),
                                             TxtJobDescription.Text.Trim());

            if (R)
            {
                await DisplayAlert("!!!","The user was added!","OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert(":(", "Something went wrong!", "OK");

            }
        }


        //cargar roles
        public async void LoadRoles()
        {
            CboUserRole.ItemsSource = await viewModel.LoadRoles();
        }
    }
}