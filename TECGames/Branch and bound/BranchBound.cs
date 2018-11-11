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
        double minimunPrice = 0;
        int nodesAmount = 0;
        int query = 0;int cont = 0;

        public BranchBound(int query)
        {
            this.query = query;
            foreach (Work work in Program.workList)
            {
                if (root == null)
                {
                    root = work;
                    nodesAmount++;
                }
                else if(work.WorkSection!=null)
                {
                    Assembler(root, work);
                }
            }
            Lnv(root);

            root =Organizer(root);

            GC.WaitForPendingFinalizers();
            cont = 0;
            root = Bound(root,root); GC.WaitForPendingFinalizers();
            Console.WriteLine("Original total of nodes: {0}",nodesAmount);
            Console.WriteLine("New total of nodes: {0}", cont);
            Console.Write("\n_________________________________\nOrganized and bounded\n_________________________________\nMain= "); Print(root);

        }

        /*Conditionals to make the branching*/
        private void Assembler(Work root,Work nWork)
        {
            if (minimunPrice == 0 && root.WorkSection.Price > nWork.WorkSection.Price)
            {
                minimunPrice = nWork.WorkSection.Price;
            }
            else if (minimunPrice == 0 && root.WorkSection.Price < nWork.WorkSection.Price)
            {
                minimunPrice = root.WorkSection.Price;
            }
            else if (minimunPrice>nWork.WorkSection.Price)
            {
                minimunPrice = nWork.WorkSection.Price;
            }


            if (root.WorkSection.Price>nWork.WorkSection.Price && root.Left==null)
            {
                root.Left = nWork;
                nodesAmount++;
            }
            else if (root.WorkSection.Price < nWork.WorkSection.Price && root.Right == null)
            {
                root.Right = nWork;
                nodesAmount++;
            }
            else if (root.WorkSection.Price == nWork.WorkSection.Price && root.Left==null)
            {
                root.Left = nWork;
                nodesAmount++;
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

        /*Makes the list of the alives nodes*/  ///revisar
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

        /*It just prints the nodes*/
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
                Console.SetCursorPosition(17, Console.CursorTop);
                Console.Write("Price:{0}\nLeft= ",root.WorkSection.Price);
                Print(root.Left);

                Console.Write("\nId: {0}\nRight= ",root.Id);
                Print(root.Right);
            }
            return;
        }

        /*Reconstructs the order of the nodes to make easier the bounding*/
        private Work Organizer(Work root)
        {
            if (root == null)
            {
                return root;
            }
            root.Left = Organizer(root.Left);
            root.Right = Organizer(root.Right);

            /**************************************************************/
            if (root.WorkSection.Price > this.root.WorkSection.Price && root.Left != null)
            {
                Work temp = root.Copy();
                temp.Left = null;
                root = InsertWork(root.Left, temp, true);
            }
            else if (root.WorkSection.Price<this.root.WorkSection.Price && root.Right!=null)
            {
                Work temp = root.Copy();
                temp.Right = null;
                root = InsertWork(root.Right, temp, false);
            }

            return root;
        }

        /*
         *Complement in the Organizer function :
         *It just inserts a specific node at the end of another node's children
         */
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

        /*Returns the branching bounded*/
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
