using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECGames.Diagram_classes
{
    class Designer
    {
        private long id;
        private string name;
        private Dictionary<int,double> price;
        private int workSection;
        private String hexId="";


        public long Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public Dictionary<int, double> Price { get => price; set => price = value; }
        public int WorkSection { get => workSection; set => workSection = value; }
        public string HexId { get => hexId; set => hexId = value; }

        public Designer()
        {
        }
        public Designer(long id)
        {
            Id = id;
            HexId = Convert.ToString(id, 16);
        }

        public Designer(long id, string name, Dictionary<int, double> price) : this(id)
        {
            Name = name;
            Price = price;
        }

        public Designer(long id, string name, Dictionary<int, double> price, int workSection) : this(id, name, price)
        {
            WorkSection = workSection;
        }
    }
}
