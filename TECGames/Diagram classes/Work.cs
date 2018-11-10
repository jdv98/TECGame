using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECGames.Diagram_classes
{
    class Work
    {
        private int id;
        private List<Designer> designers = new List<Designer>();
        private Ubication ubication=null;
        private WorkSection workSection=null;
        private String hexId = "";  //posible a eliminar
        public bool linked = false;

        public int Id { get => id; set => id= value; }
        public List<Designer> Designers { get => designers; set => designers= value; }
        public Ubication Ubication { get => ubication; set => ubication= value; }
        public WorkSection WorkSection { get => workSection; set => workSection= value; }
        public string HexId { get => hexId; set => hexId = value; }

        private Work left=null;
        private Work right=null;
        public Work Left { get => left; set => left = value; }
        public Work Right { get => right; set => right = value; }

        public Work(int id)
        {
            Id = id;
            HexId = Convert.ToString(id, 16);
        }

        /*return a copy of work*/
        public Work Copy()
        {
            return (Work)base.MemberwiseClone();
        }

        public void Price()
        {
            foreach(Designer designer in Designers)
            {
                WorkSection.Price += designer.Price[WorkSection.Schedule];
            }
        }
    }
}
