using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TECGames.Diagram_classes
{
    class Designer //: IDisposable
    {
        private int id;
        private string name;
        private Dictionary<int, double> price;
        private int workSection;
        private String hexId = "";  //posible a eliminar
        public bool linked = false;


        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public Dictionary<int, double> Price { get => price; set => price = value; }
        public int WorkSection { get => workSection; set => workSection = value; }
        public string HexId { get => hexId; set => hexId = value; }

        public Designer(int id)
        {
            Id = id;
            HexId = Convert.ToString(id, 16);
        }

        public Designer(int id, string name, Dictionary<int, double> price) : this(id)
        {
            Name = name;
            Price = price;
        }

        /*public Designer(int id, string name, Dictionary<int, double> price, int workSection) : this(id, name, price)
        {
            WorkSection = workSection;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }*/         //posible a eliminar
    }
}
