using AnswerAppBrayanRojas.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace AnswerAppBrayanRojas.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}