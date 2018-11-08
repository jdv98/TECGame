using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using TECGames.Diagram_classes;
using TECGames.Branch_and_bound;

namespace TECGames
{
    class Program
    {
        public static List<Work> workList = new List<Work>();
        public static List<Ubication> ubicationList = new List<Ubication>();
        public static List<WorkSection> workSectionList = new List<WorkSection>();
        public static List<Designer> designerList = new List<Designer>();

        public static Dictionary<int, String> schedules = new Dictionary<int, string>() { {1, "7:00am a 4:00pm" },{ 2, "7:00am a 11:00pm" },{ 3, "7:00pm a 4:00am" },{ 4, "7:00am a 11:00pm" } };

        static void Main(string[] args)
        {
            int x = 10;
            DataManagement dG=new DataManagement(x);
            dG.DataCreator();
            Console.ReadKey();

            Console.Clear();
            Console.WriteLine("Linking data");
            dG.Linker();
            Console.Clear();
            Console.WriteLine("Memory usage: {0}MB", (System.GC.GetTotalMemory(true) / 1000000).ToString());
            GC.Collect();
            dG.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Console.WriteLine("Memory usage: {0}MB", (System.GC.GetTotalMemory(true) / 1000000).ToString());

            Console.WriteLine("All data have been linked");
#if DEBUG
            Console.WriteLine("\n_____________________________________\nData couldn't be created \n\nworkList: {0}% \nubicationList: {1}% \ndesignerList: {2}%\n_____________________________________", (int)((( (double)x -(double)workList.Count) / (double)x) * ((double)100)), (int)((((double)x - (double)ubicationList.Count) / (double)x) * ((double)100)), (int)((((double)(2*x) - (double)designerList.Count) / (double)2*x) * ((double)100)));
            Console.WriteLine("\nTotal created data \n\nworkList={0} \nubicationList={1} \ndesignerList={2} \n_____________________________________", (double)workList.Count,ubicationList.Count, designerList.Count);
#endif
            Console.ReadKey();

            BranchBound bB = new BranchBound(10);

            Console.ReadKey();
        }
    }
}
