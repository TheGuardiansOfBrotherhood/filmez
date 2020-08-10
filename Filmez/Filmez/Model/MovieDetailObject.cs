using System;
using System.Collections.Generic;
using System.Text;

namespace Filmez.Model
{
    class MovieDetailObject
    {
        public Movie Movie { get; set; }
        public User User { get; set; }
        public List<Critical> CriticalList { get; set; }

    }
}
