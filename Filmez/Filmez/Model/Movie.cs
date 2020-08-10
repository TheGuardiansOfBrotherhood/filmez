
using Realms;
using System;
using System.Collections.Generic;


namespace Filmez.Model
{
    class Movie : RealmObject
    {
        public int Id { get; set; }
        public String Poster_path { get; set; }
        public Boolean Adult { get; set; }
        public String Overview { get; set; }
        public String Release_date { get; set; }
      
        //public int[] Genre_ids { get; set; }
        
        public String Original_title { get; set; }
        public String Original_language { get; set; }
        public String Title { get; set; }
        public String Backdrop_path { get; set; }
        public float Popularity { get; set; }
        public int Vote_count { get; set; }
        public Boolean Video { get; set; }
        public float Vote_average {get; set;}

        public String Image
        {
            get{ return "https://image.tmdb.org/t/p/w200" + Poster_path.ToString(); }
        }

        public String Image_backdrop
        {
            get { return "https://image.tmdb.org/t/p/w500" + Backdrop_path.ToString(); }
        }


    }
}
