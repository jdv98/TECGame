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
        int query = 0;int cont = 0;

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
            Lnv(root); //Print(root);
            Console.Write("\n\n\n\nMain= "); Print(root); Console.Write("\n\n\n\nMain= ");
            root =Ordenar(root);

            Console.Write("\n\n\n\nMain= "); Print(root);

            cont = 0;
            root = Bound(root,root);
            Console.Write("\n\n\nBounded\nMain= "); Print(root);
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
            if (root== null || cont==query)
            {
                return;
            }
            else if (root.Left==null && root.Right==null && query>=cont)
            {
                lnv.Add(root);
                cont++;
                return;
            }
            else
            {
                Lnv(root.Left);
                Lnv(root.Right);

                if (cont != query)
                {
                    lnv.Add(root);
                    cont++;
                }
            }
            return;
        }
        private void Print(Work root)
        {
            if (root == null)
            {
                Console.Write("null");
                return;
            }
            else
            {

                Console.SetCursorPosition(7, Console.CursorTop);
                Console.Write("Id: {0}",root.Id);
                Console.SetCursorPosition(14, Console.CursorTop);
                Console.Write("Price:{0}\nLeft= ",root.WorkSection.Price);
                Print(root.Left);

                Console.Write("\nId: {0}\nRight= ",root.Id);
                Print(root.Right);
            }
            return;
        }
        private Work Ordenar(Work root)
        {
            if (root == null)
            {
                return root;
            }
            if (root.Left != null)
            {
                Console.WriteLine("Actual={1} Left={0}", root.Left.Id, root.Id);
            }
            else
            {
                Console.WriteLine("Actual={1} Left={0}", "null", root.Id);
            }
            root.Left = Ordenar(root.Left);
            if (root.Right != null)
            {
                Console.WriteLine("Actual={1} Right={0}", root.Right.Id, root.Id);
            }
            else
            {
                Console.WriteLine("Actual={1} Right={0}", "null",root.Id);
            }
            root.Right = Ordenar(root.Right);
            Console.WriteLine("Procesando actual={0}", root.Id);


            /**************************************************************/
            if (root.WorkSection.Price > this.root.WorkSection.Price && root.Left != null)
            {
                Work temp = new Work(root.Id, root.Designers, root.Ubication, root.WorkSection, root.HexId, root.Left, root.Right);
                temp.Left = null;
                root = InsertWork(root.Left, temp, true);
            }
            else if (root.WorkSection.Price<this.root.WorkSection.Price && root.Right!=null)
            {
                Work temp = new Work(root.Id,root.Designers,root.Ubication,root.WorkSection,root.HexId,root.Left,root.Right);
                temp.Right = null;
                root = InsertWork(root.Right, temp, false);
            }

            return root;
        }
        private Work InsertWork(Work root,Work rooToInsert,bool right)
        {
            if (root == null)
            {
                return rooToInsert;
            }
            else if (right)
            {
                root.Right = InsertWork(root.Right,rooToInsert,right);
            }
            else if (!right)
            {
                root.Left = InsertWork(root.Left,rooToInsert,right);
            }
            return root;
        }
        private Work Bound(Work root,Work previousRoot)
        {
            bool counted = false;
            if (root!=null && cont != query && root.WorkSection.Price<=this.root.WorkSection.Price)
            {
                root = Bound(root.Left,root);
                if (!counted && cont != query)
                {
                    counted = true;
                    cont++;
                }
                if (cont == query)
                {
                    return root;
                }
            }
            if(root != null && cont != query && root.WorkSection.Price >= this.root.WorkSection.Price)
            {
                if (!counted && cont != query)
                {
                    counted = true;
                    cont++;
                }
                if (cont == query)
                {
                    root.Right = null;
                    return root;
                }
                root.Right = Bound(root.Right, root.Right);
            }

            return previousRoot;
        }
    }
}
