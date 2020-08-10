
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Filmez
{
    class Discover
    {
        public int Page {get; set;}
        public ObservableCollection<Filmez.Model.Movie> Results { get; set; }
        public int Total_results { get; set; }
        public int Total_pages { get; set; }
    }
}
