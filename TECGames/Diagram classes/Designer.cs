using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECGames.Diagram_classes
{
    class Designer
    {
        private int id;
        private string name;
        private double price;
        private int workSection;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public double Price { get => price; set => price = value; }
        public int WorkSection { get => workSection; set => workSection = value; }

        public Designer()
        {
        }
        public Designer(int id)
        {
            Id = id;
        }

        public Designer(int id, string name, double price, int workSection) : this(id)
        {
            Name = name;
            Price = price;
            WorkSection = workSection;
        }
    }
}
