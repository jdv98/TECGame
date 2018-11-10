using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECGames.Diagram_classes
{
    class WorkSection
    {
        private int id;
        private string name;
        private double price;
        private int schedule;
        private String hexId = "";
        public bool linked=false;
        
        public int Id { get => id; set => id= value; }
        public string Name{ get => name; set => name= value; }
        public double Price { get => price; set => price= value; }
        public int Schedule { get => schedule; set => schedule = value; }
        public string HexId { get => hexId; set => hexId = value; }

        public WorkSection(int id, string name, int schedule)
        {
            Id = id;
            Name = name;
            Schedule = schedule;
            HexId = Convert.ToString(id, 16);
        }
    }
}
