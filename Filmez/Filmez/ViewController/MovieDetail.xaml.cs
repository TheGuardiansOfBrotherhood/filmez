using Filmez.API;
using Filmez.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Filmez.ViewController
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieDetail : ContentPage
    {
        DataService dataService;
        Movie movie { get; set; }
        User user { get; set; }


        public MovieDetail(int idMovie, int idUser)
        {
            InitializeComponent();
            dataService = new DataService();
            movie = dataService.GetMovie(idMovie);

            
            user = dataService.GetUser(idUser);
            MovieDetailObject mdo = new MovieDetailObject();
            mdo.Movie = movie;
            mdo.User = user;
            mdo.CriticalList = dataService.GetCriticals(movie);

            if (Device.RuntimePlatform == Device.UWP)
            {
                favorite.Source = ImageSource.FromFile("Imagem/favorite.png");
                calendar.Source = ImageSource.FromFile("Imagem/calendar.png");
                heart.Source = ImageSource.FromFile("Imagem/heart.png");
            }
            else
            {
                favorite.Source = ImageSource.FromFile("favorite.png");
                calendar.Source = ImageSource.FromFile("calendar.png");
                heart.Source = ImageSource.FromFile("heart.png");
            }


            BindingContext = mdo;
            

        }

        public void Comment_Clicked(object sender, EventArgs e)
        {
            int note = pickerRating.SelectedIndex;
            String comment = editorComment.Text;
            if (comment != "")
            {
                Critical critical = new Critical();
                critical.User = user;
                critical.Movie = movie;
                critical.Ranking = note + 1;
                critical.Comment = comment;
                dataService.SaveCritical(critical);
                editorComment.Text = string.Empty;
                pickerRating.SelectedIndex = 0;
            }
            else
            {
                DisplayActionSheet("Aviso", "Preencha todos os campos.", "Ok");
            }

            listViewCriticals.ItemsSource = dataService.GetCriticals(movie);
        }


        
       

    }
}