using Filmez.API;
using Filmez.Model;
using Realms;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Filmez
{
    public partial class App : Application
    {
        DataService Dataservice;
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            MainPage = new NavigationPage(new Filmez.ViewController.Login());
            //MainPage = new NavigationPage(new Filmez.MainPage(1));
        }

        protected override void OnStart()
        {
            Dataservice = new DataService();
            //Dataservice.DeleteDB();
            Dataservice.SaveUser("admin", "28", "admin");
            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
