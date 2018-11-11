using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using TECGames.Diagram_classes;
using TECGames.Branch_and_bound;
using TECGames.Genetic_Algorithm;

namespace TECGames
{
    class Program
    {
        public static List<Work> workList = new List<Work>();
        public static List<Ubication> ubicationList = new List<Ubication>();
        public static List<WorkSection> workSectionList = new List<WorkSection>();
        public static List<Designer> designerList = new List<Designer>();

        public static Dictionary<int, String> schedules = new Dictionary<int, string>() { {0, "No trabaja diurno" },{ 1, "7:00am a 4:00pm" }, { 2, "7:00am a 11:00pm" }, { 3, "7:00pm a 4:00am" }, { 4, "7:00am a 11:00pm" },{5,"No trabaja nocturno" } };

        static void Main(string[] args)
        {
            int numberTest = 70;
            bool testMode = Mode();
            bool keepIn = true;
            int x =0;
            
            while (keepIn)
            {
                if (!testMode)
                {
                    Execution(Menu1());
                }
                else
                {

                }



                Console.Clear();
                Console.Write("Y->Exit      N->Continue\nSelection: ");
                char selectionKeepIn = Console.ReadKey().KeyChar;
                if (selectionKeepIn == 'y' || selectionKeepIn == 'Y')
                {
                    keepIn = false;
                }
                Console.Clear(); ResetList();
            }





 /*           dG.DataCreator();
            Console.ReadKey();


            Console.Clear();

            Console.WriteLine("All data have been linked");

            Console.ReadKey();

            BranchBound bB = new BranchBound(10);
            Console.ReadKey();
            Console.Clear();

            GeneticAlgorithm gA = new GeneticAlgorithm(4000);

            Console.ReadKey();*/
        }

        static void Execution(int x)
        {
            DataManagement dG = new DataManagement(x);
            dG.DataCreator();
            Console.WriteLine("All the data have been created");
            Console.ReadKey();
            Console.Clear();


            dG.Linker();
            Console.WriteLine("All the data have been linked");
            Console.ReadKey();
            Console.Clear();

#if DEBUG
            Console.WriteLine("Memory usage: {0}MB", (System.GC.GetTotalMemory(true) / 1000000).ToString());
            Console.WriteLine("\n_____________________________________\nData couldn't be created \n\nworkList: {0}% \nubicationList: {1}% \ndesignerList: {2}%\n_____________________________________", (int)((((double)x - (double)workList.Count) / (double)x) * ((double)100)), (int)((((double)x - (double)ubicationList.Count) / (double)x) * ((double)100)), (int)((((double)(2 * x) - (double)designerList.Count) / (double)2 * x) * ((double)100)));
            Console.WriteLine("\nTotal created data \n\nworkList={0} \nubicationList={1} \ndesignerList={2} \n_____________________________________", (double)workList.Count, ubicationList.Count, designerList.Count);
            Console.ReadKey();
            Console.Clear();
#endif
            Console.WriteLine("_____________________________________\nBranch and bound\n_____________________________________\n");
            BranchBound bB = new BranchBound(4);
            Console.ReadKey();
            Console.Clear();


            Console.WriteLine("_____________________________________\nGenetic algorithm\n_____________________________________\n");
            GeneticAlgorithm gA = new GeneticAlgorithm(100);
            Console.ReadKey();
        }

        static int Menu1()
        {
            int workAmount = 0;
            Console.Write("Insert number of works you want:");
            if (!int.TryParse(Console.ReadLine(), out workAmount))
            {
                workAmount = 100;
            }

            return workAmount;
        }

        static bool Mode()
        {
            int x = -1;
            bool testMode = false;
            while (!(0 < x && x < 3))
            {
                Console.Write("1->Free mode\n2->Test mode\nMode: ");
                int.TryParse(Console.ReadLine(), out x);
                Console.Clear();
            }

            switch (x)
            {
                case 1:
                    testMode = false;
                    break;
                default:
                    testMode = true;
                    break;
            }

            return testMode;
        }

        static void ResetList()
        {
            workList = new List<Work>();
            ubicationList = new List<Ubication>();
            workSectionList = new List<WorkSection>();
            designerList = new List<Designer>();
        }
    }
}
