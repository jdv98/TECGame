using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TECGames.Diagram_classes;
namespace TECGames.Branch_and_bound
{
    class BranchBound
    {
        public Work root=null;

        public BranchBound()
        {
            foreach (Work work in Program.workList)
            {
                if (root == null)
                {
                    root = work;
                }
                else
                {
                    Assembler(root, work);
                }
            }
            root = Bound(root);
        }

        private void Assembler(Work root,Work nWork)
        {
            if (root.WorkSection.Price>nWork.WorkSection.Price && root.Left==null)
            {
                root.Left = nWork;
            }
            else if (root.WorkSection.Price < nWork.WorkSection.Price && root.Right == null)
            {
                root.Right = nWork;
            }
            else if (root.WorkSection.Price == nWork.WorkSection.Price && root.Left==null)
            {
                root.Left = nWork;
            }
            else if (root.WorkSection.Price == nWork.WorkSection.Price && root.Left != null)
            {
                Assembler(root.Left, nWork);
            }
            else if (root.WorkSection.Price > nWork.WorkSection.Price && root.Left != null)
            {
                Assembler(root.Left,nWork);
            }
            else if (root.WorkSection.Price < nWork.WorkSection.Price && root.Right != null)
            {
                Assembler(root.Right, nWork);
            }
            return 0;
        }

        private Work Bound(Work root)
        {

            return root;
        }
    }
}
