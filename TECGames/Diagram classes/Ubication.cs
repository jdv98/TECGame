using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECGames.Diagram_classes
{
    class Ubication
    {
        private int id;
        private string name;
        private int schedule;

        public int Id{ get => id; set => id= value; }
        public string UbicationName{ get => name; set => name= value; }
        public int Schedule { get => schedule; set => schedule = value; }

        public Ubication()
        {
        }
    }
}
