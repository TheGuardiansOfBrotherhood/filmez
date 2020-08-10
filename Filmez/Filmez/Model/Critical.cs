
using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filmez.Model
{
    class Critical : RealmObject
    {
        [PrimaryKey]
        public int Id { get; set; }
        public int Ranking { get; set; }
        public string Comment { get; set; }
        public Movie Movie { get; set; }
        public User User { get; set; }
    }
}
