using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Filmez.Model;
using Realms;
using Realms.Schema;
using System.Threading;

namespace Filmez.API
{
    class DataService
    {
        private const String URL_BASE = "https://api.themoviedb.org/3";
        private const String API_KEY = "api_key=987f551a17664b656659a6d4e74b37a3";
        private const String URL_BASE_IMAGE = "https://image.tmdb.org/t/p/w500";
        private const String URL_DISCOVER = "/discover/movie";
        private const String LANGUAGE = "language=pt-BR";
        private const String FILTERS = "&sort_by=popularity.desc&include_adult=false&include_video=false&page=1";

        private const String URL_DISCOVER_COMPLETE = URL_BASE + URL_DISCOVER + "?" + API_KEY + "&" + LANGUAGE + FILTERS;
        HttpClient client = new HttpClient();


        public  void DeleteDB()
        {
            Realm.DeleteRealm(RealmConfiguration.DefaultConfiguration);
        }

        private Realm GetRealm()
        {
            var con = RealmConfiguration.DefaultConfiguration;
            con.SchemaVersion = 2;
            Realm RealmDb = Realm.GetInstance();
            return RealmDb;
        }
        /// <summary>
        /// Obtém os itens de produtos
        /// </summary>
        public async Task<ObservableCollection<Movie>> GetDiscoverAsync()
        {
            var response = await client.GetStringAsync(URL_DISCOVER_COMPLETE);
            var discover = JsonConvert.DeserializeObject<Discover>(response);
            return discover.Results;
        }

        public ObservableCollection<Movie> GetAllMovie()
        {
            var RealmDb = GetRealm();
            return RealmDb.All<Movie>() as ObservableCollection<Movie>;

        }

        public Movie GetMovie(int id)
        {
            var RealmDb = GetRealm();
            Movie movie = RealmDb.All<Movie>().First(m => m.Id == id);
            return movie;
        }

        public void SaveAllMovie(ObservableCollection<Movie> MoviesCollection)
        {
            var realm = Realm.GetInstance();
            realm.Write(() =>
            {
                foreach (var movie in MoviesCollection)
                {
                    realm.Add(movie);
                }
            });
        }

        public User GetUser(int idUser)
        {
            var RealmDb = GetRealm();
            User user = RealmDb.All<User>().First(u => u.Id == idUser);
            return user;
          
        }


        public User GetUser(String nickname) 
        {
            var RealmDb = GetRealm();
            var user = RealmDb.All<User>().First (u => u.NickName == nickname);
            return user;

        }

        public void SaveUser(String Name, String Age, String NickName)
        {
            var RealmDb = GetRealm();
            var users = RealmDb.All<User>().ToList();
            int maxUserId = 0;
            if (users.Count != 0)
            {
                maxUserId = users.Max(u => u.Id);
            }

            var User = new User()
            {
                Id = maxUserId + 1,
                Name = Name,
                Age = Age,
                NickName = NickName
            };


            RealmDb.Write(() => {
                RealmDb.Add(User);
            });
        }

        public List<Critical> GetCriticals(Movie movie)
        {
            var RealmDb = GetRealm();
            var criticalSouce = RealmDb.All<Critical>().Where(c => c.Movie == movie).OrderByDescending(c=>c.Id);
            var list = new List<Critical>(criticalSouce);
            return list;
        }

        public void SaveCritical(Critical Critical)
        {
            var RealmDb = GetRealm();
            var count = RealmDb.All<Critical>().Count();

            Critical.Id = count + 1;

            RealmDb.Write(()=>{
                RealmDb.Add(Critical);
            
                
            });
        }

       
    }
}
