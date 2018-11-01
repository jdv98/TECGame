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
        private Ubication ubication;
        private WorkSection workSection;

        public int Id { get => id; set => id= value; }
        public List<Designer> Designers { get => designers; set => designers= value; }
        public Ubication Ubication { get => ubication; set => ubication= value; }
        public WorkSection WorkSection { get => workSection; set => workSection= value; }

        public Work()
        {
        }
    }
}
