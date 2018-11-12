using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TECGames.Diagram_classes;
namespace TECGames.Branch_and_bound
{
    class BranchBound
    {
        public Work root=null;
        double minimunPrice = 0;
        int nodesAmount = 0;
        int query = 0;
        int cont = 0;

        Stopwatch stopwatch = new Stopwatch();
        Result result = new Result("Branch & bound", Program.workList.Count);

        public BranchBound(int query)
        {
            if(Program.testMode)
                stopwatch.Start();

            this.query = query;
            result.Add(0, 5);

            for(int work=0;work<Program.workList.Count;work++)
            {
                if (root == null)
                {
                    root = Program.workList[work];
                    nodesAmount++;
                    result.Add(1, 2);
                }
                else if(Program.workList[work].WorkSection!=null)
                {
                    Assembler(root, Program.workList[work]);
                    result.Add(2,0);
                }
            }
            //"For" comparations and assignments
            result.Add(Program.workList.Count+1, Program.workList.Count+1);

            root =Organizer(root);          
            cont = 0;
            root = Bound(root,root);
            result.Add(0, 3);

            Console.WriteLine("Original total of nodes: {0}",nodesAmount);
            Console.WriteLine("New total of nodes: {0}", cont);
            Console.Write("\n_________________________________\nOrganized and bounded\n_________________________________\nMain= "); Print(root);
            result.timeMilisecond = stopwatch.ElapsedMilliseconds;
            if (Program.testMode)
                Program.results.Add(result);
        }

        /*Conditionals to make the branching*/
        private void Assembler(Work root,Work nWork)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (minimunPrice == 0 && root.WorkSection.Price > nWork.WorkSection.Price)
            {
                minimunPrice = nWork.WorkSection.Price;
                result.Add(2, 1);
            }
            else if (minimunPrice == 0 && root.WorkSection.Price < nWork.WorkSection.Price)
            {
                minimunPrice = root.WorkSection.Price;
                result.Add(4, 1);
            }
            else if (minimunPrice>nWork.WorkSection.Price)
            {
                minimunPrice = nWork.WorkSection.Price;
                result.Add(5, 1);
            }
            else
            {
                result.Add(5, 0);
            }


            if (root.WorkSection.Price>nWork.WorkSection.Price && root.Left==null)
            {
                root.Left = nWork;
                nodesAmount++;
                result.Add(2, 2);
            }
            else if (root.WorkSection.Price < nWork.WorkSection.Price && root.Right == null)
            {
                root.Right = nWork;
                nodesAmount++;
                result.Add(4, 2);
            }
            else if (root.WorkSection.Price == nWork.WorkSection.Price && root.Left==null)
            {
                root.Left = nWork;
                nodesAmount++;
                result.Add(6, 2);
            }
            else if (root.WorkSection.Price == nWork.WorkSection.Price && root.Left != null)
            {
                Assembler(root.Left, nWork);
                result.Add(8, 0);
            }
            else if (root.WorkSection.Price > nWork.WorkSection.Price && root.Left != null)
            {
                Assembler(root.Left,nWork);
                result.Add(10, 0);
            }
            else if (root.WorkSection.Price < nWork.WorkSection.Price && root.Right != null)
            {
                Assembler(root.Right, nWork);
                result.Add(12, 0);
            }
            else
            {
                result.Add(12, 0);
            }
            return ;
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
                result.Add(1, 0);
                return root;
            }
            root.Left = Organizer(root.Left);
            root.Right = Organizer(root.Right);
            result.Add(1, 2);

            /**************************************************************/
            if (root.WorkSection.Price > this.root.WorkSection.Price && root.Left != null)
            {
                Work temp = root.Copy();
                temp.Left = null;
                root = InsertWork(root.Left, temp, true);
                result.Add(2, 3);
            }
            else if (root.WorkSection.Price<this.root.WorkSection.Price && root.Right!=null)
            {
                Work temp = root.Copy();
                temp.Right = null;
                root = InsertWork(root.Right, temp, false);
                result.Add(4, 3);
            }
            else
            {
                result.Add(4, 0);
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
                result.Add(1, 0);
                return rooToInsert;
            }
            else if (right)
            {
                root.Right = InsertWork(root.Right,rooToInsert,right);
                result.Add(2, 1);
            }
            else if (!right)
            {
                root.Left = InsertWork(root.Left,rooToInsert,right);
                result.Add(3, 1);
            }
            else
            {
                result.Add(3, 0);
            }
            return root;
        }

        /*Returns the branching bounded*/
        private Work Bound(Work root,Work previousRoot)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            bool counted = false;
            result.Add(0,1);
            if (root!=null && cont != query && root.WorkSection.Price<=this.root.WorkSection.Price)
            {
                root = Bound(root.Left,root);
                result.Add(2,1);
                if (!counted && cont != query)
                {
                    counted = true;
                    cont++;
                    result.Add(2,2);
                }
                if (cont == query)
                {
                    result.Add(1,0);
                    return root;
                }
            }
            else
            {
                result.Add(2,0);
            }
            if(root != null && cont != query && root.WorkSection.Price >= this.root.WorkSection.Price)
            {
                result.Add(2,0);
                if (!counted && cont != query)
                {
                    counted = true;
                    cont++;
                    result.Add(2,2);
                }
                if (cont == query)
                {
                    root.Right = null;
                    result.Add(1,1);
                    return root;
                }
                root.Right = Bound(root.Right, root.Right);
            }
            else
            {
                result.Add(2,0);
            }
            return previousRoot;
        }
    }
}
