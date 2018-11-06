using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TECGames.Diagram_classes;
namespace TECGames.Branch_and_bound
{
    class Branch
    {
        public Work root=null;

        public Branch()
        {
            foreach (Work work in Program.workList)
            {
                if (root == null)
                {
                    root = work;
                }
                else
                {

                }
            }
        }

        private int Assembler(Work root,Work nWork)
        {
            return 0;
        }
    }
}
