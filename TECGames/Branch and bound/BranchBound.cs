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
        private List<Work> lnv = new List<Work>();
        double menor = 0;
        int query = 0;int cont = 1;

        public BranchBound(int query)
        {
            this.query = query;
            foreach (Work work in Program.workList)
            {
                if (root == null)
                {
                    root = work;
                }
                else if(work.WorkSection!=null)
                {
                    Assembler(root, work);
                }
            }
            Lnv(root);
            Console.WriteLine("menor: {0}\n\n\n",menor);
            Print(root);
        }

        private void Assembler(Work root,Work nWork)
        {
            if (menor == 0 && root.WorkSection.Price > nWork.WorkSection.Price)
            {
                menor = nWork.WorkSection.Price;
            }
            else if (menor == 0 && root.WorkSection.Price < nWork.WorkSection.Price)
            {
                menor = root.WorkSection.Price;
            }
            else if (menor>nWork.WorkSection.Price)
            {
                menor = nWork.WorkSection.Price;
            }


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
            return ;
        }

        private void Lnv(Work root)
        {
            if (root== null)
            {
                return;
            }
            else if (root.Left==null && root.Right==null && query>=cont)
            {
                Console.WriteLine("Lnv: {0}     price: {1}",root.Id,root.WorkSection.Price);
                lnv.Add(root);
                cont++;
                return;
            }
            else if (query<cont)
            {
                return;
            }
            else
            {
                Lnv(root.Left);
                Lnv(root.Right);
            }
            return;
        }

        private void Print(Work root)
        {
            if (root == null)
            {
                return;
            }
            else
            {
                Console.Write("      Id: {0}  Price:{1}\nLeft= ",root.Id,root.WorkSection.Price);
                Print(root.Left);
                Console.Write("      Id: {0}  Price:{1}\nRight= ", root.Id, root.WorkSection.Price);
                Print(root.Right);
            }
            return;
        }
    }
}
