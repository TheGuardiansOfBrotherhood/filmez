using Filmez.API;
using Filmez.Model;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Filmez.ViewController
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        readonly DataService DataService;
        public Login()
        {
            InitializeComponent();
            DataService = new DataService();
        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            User User = null;
            
            if (NickName.Text != "")
            {
               User = DataService.GetUser(NickName.Text);
            }
            else
            {
                DisplayAlert("Aviso", "Por favor, preencha o nickname.", "Ok");
            }
            
            if(User == null)
            {
                DisplayAlert("Aviso", "Realize o cadastro para poder entrar no Filmez.", "Ok");
            }else
            {
                Navigation.PushAsync(new MainPage(User.Id));
            }
        }

        private void Register_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterUser());
        }
    }
}