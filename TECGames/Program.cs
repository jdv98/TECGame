using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using TECGames.Diagram_classes;

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
            int x = 100000;
                                             //7343282
            DataGenerator dG=new DataGenerator(x);
            dG.BranchBound();
#if DEBUG
            Console.WriteLine("\n_____________________________________\nData couldn't be created \n\nworkList: {0}% \nubicationList: {1}% \ndesignerList: {2}%\n_____________________________________", (int)((( (double)x -(double)workList.Count) / (double)x) * ((double)100)), (int)((((double)x - (double)ubicationList.Count) / (double)x) * ((double)100)), (int)((((double)x - (double)designerList.Count) / (double)x) * ((double)100)));
            Console.WriteLine("\nTotal created data \n\nworkList={0} \nubicationList={1} \ndesignerList={2} \n_____________________________________", (double)workList.Count,ubicationList.Count, designerList.Count);
            Console.ReadKey();
#endif
            Console.ReadKey();
        }
    }
}
