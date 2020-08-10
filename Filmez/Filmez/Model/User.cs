using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filmez.Model
{
    class User : RealmObject
    {
        [PrimaryKey]
        public int Id { get; set; }
        public String Name { get; set; }
        public String Age { get; set; }
        public String NickName { get; set; }
    }
}
