using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AnswerAppBrayanRojas.ViewModels;

namespace AnswerAppBrayanRojas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyQuestionsPage : ContentPage
    {
        AskViewModel askViewModel;

        public MyQuestionsPage()
        {
            InitializeComponent();

            BindingContext = askViewModel = new AskViewModel();

            //todo: hasta no tener el usuario global vamos a tener que quemar el id del usuario
            //en mi caso es el id 30

            askViewModel.MyQuetion.UserId = 30;
            LoadList();

        }


        public async void LoadList()
        {
            LstMyQuestions.ItemsSource = await askViewModel.GetQlist();
        }
    }
}