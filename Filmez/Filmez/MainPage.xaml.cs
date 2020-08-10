using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filmez.API;
using Filmez.Model;
using Filmez.ViewController;
using Xamarin.Forms;

namespace Filmez
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private const String URL_BASE = "https://api.themoviedb.org/3";
        private const String API_KEY = "api_key=987f551a17664b656659a6d4e74b37a3";
        private const String URL_BASE_IMAGE = "https://image.tmdb.org/t/p/w500";
        private const String URL_DISCOVER = "/discover/movie";
        private const String LANGUAGE = "language=pt-BR";
        private const String FILTERS = "&sort_by=popularity.desc&include_adult=false&include_video=false&page=1";

        private const String URL_DISCOVER_COMPLETE = URL_BASE + URL_DISCOVER + "?" + API_KEY + "&" + LANGUAGE + FILTERS;
        static ObservableCollection<Movie> movieList;
        DataService dataService;
        private int IdUser; 
        static ObservableCollection<Movie> MoviesCollection { get; set; }
        public MainPage(int Id)
        {
            InitializeComponent();
            dataService = new DataService();
            IdUser = Id;
            //listViewMovies.ItemsSource = (System.Collections.IEnumerable)GetMoviesAsycn();

            //LoadDiscover();

            CarregarDados();

            if (MoviesCollection == null)
            {
                AtualizaDados();
            }

            //BindingContext = this;
            User user = dataService.GetUser(Id);
            LabelName.Text = "Você está logado como "+user.Name;
            


        }

        private void CarregarDados()
        {
            MoviesCollection = dataService.GetAllMovie();

        }

        private async void AtualizaDados()
        {
            MoviesCollection = new ObservableCollection<Movie>();
            movieList = await dataService.GetDiscoverAsync();
            foreach (var movie in movieList)
            {
                String Release_date = GetFormatterDate(movie.Release_date);

                Movie m = new Movie()
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Overview = movie.Overview,
                    Poster_path = movie.Poster_path,
                    Adult = movie.Adult,
                    Release_date = Release_date,
                    Original_language = movie.Original_language,
                    Backdrop_path = movie.Backdrop_path,
                    Popularity = movie.Popularity,
                    Vote_count = movie.Vote_count,
                    Vote_average = movie.Vote_average,
                    Original_title = movie.Original_title

                };
                MoviesCollection.Add(m);
            }

           dataService.SaveAllMovie(MoviesCollection);
            //listViewMovies.ItemsSource = movieList;
            listViewMovies.ItemsSource = MoviesCollection;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var movie = e.SelectedItem as Movie;
            await Navigation.PushAsync(new MovieDetail(movie.Id, IdUser));
            //DisplayAlert("Item Selecionado (SelectedItem) ", movie.Title, "Ok");
        }

        private async void Logoff_Clicked(object sender, EventArgs e)
        {
            //await Navigation.RemovePage(Page.);
            //var _navigation = Application.Current.MainPage.Navigation;
            //var _lastPage = _navigation.NavigationStack.LastOrDefault();
            //Remove last page
            //_navigation.RemovePage(_lastPage);


            await Navigation.PushAsync(new Filmez.ViewController.Login());

        }

        public String GetFormatterDate(String ReleaseDate)
        {
            DateTime date = Convert.ToDateTime(ReleaseDate);
            String Day = date.Day.ToString();
            int Month = date.Month;
            String MonthString = "";
            String Year = date.Year.ToString();

            if(date.Day.ToString().Length == 1)
            {
                Day = "0" + date.Day;
            }
            switch (Month)
            {
                case 1: 
                    MonthString = "janeiro";
                    break;
                case 2:
                    MonthString = "fevereiro";
                    break;
                case 3:
                    MonthString = "março";
                    break;
                case 4:
                    MonthString = "abril";
                    break;
                case 5:
                    MonthString = "maio";
                    break;
                case 6:
                    MonthString = "junhho";
                    break;
                case 7:
                    MonthString = "julho";
                    break;
                case 8:
                    MonthString = "agosto";
                    break;
                case 9:
                    MonthString = "setembro";
                    break;
                case 10:
                    MonthString = "outubro";
                    break;
                case 11:
                    MonthString = "novembro";
                    break;
                case 12:
                    MonthString = "dezembro";
                    break;
            }

            return Day+" de "+MonthString+" de "+Year;
        }
    }
}
