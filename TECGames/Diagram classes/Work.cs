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
        private String hexId = "";
        public bool linked = false;

        public int Id { get => id; set => id= value; }
        public List<Designer> Designers { get => designers; set => designers= value; }
        public Ubication Ubication { get => ubication; set => ubication= value; }
        public WorkSection WorkSection { get => workSection; set => workSection= value; }
        public string HexId { get => hexId; set => hexId = value; }

        private Work left;
        private Work right;
        public Work Left { get => left; set => left = value; }
        public Work Right { get => right; set => right = value; }

        public Work()
        {
        }

        public Work(int id)
        {
            Id = id;
            HexId = Convert.ToString(id, 16);
        }

        public Work(int id, List<Designer> designers, Ubication ubication, WorkSection workSection):this(id)
        {
            Designers = designers;
            Ubication = ubication;
            WorkSection = workSection;
        }
    }
}
