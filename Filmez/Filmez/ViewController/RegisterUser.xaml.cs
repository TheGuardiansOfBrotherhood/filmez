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
    public partial class RegisterUser : ContentPage
    {
        readonly DataService dataService;
        public RegisterUser()
        {
            InitializeComponent();
            dataService = new DataService();
        }

        private void Save_User_Clicked(object sender, EventArgs e)
        {
            if(name.Text != "" && age.Text !=  ""  && nickname.Text != "")
            {
                dataService.SaveUser(name.Text, age.Text, nickname.Text);
                //DisplayAlert("Parabéns", "Agora vc é um usuário FILMEZ!", "Ok");
                Navigation.PopAsync();
            }
            else
            {
                DisplayAlert("Aviso!", "Todos os campos são obrigatórios", "Ok");
            }
        }
    }
}