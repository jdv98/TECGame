using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using TECGames.Diagram_classes;

namespace TECGames
{
    class DataGenerator
    {
        long cont = 0;
        long total = 0;
        long quantity;
        Random random = new Random(DateTime.Now.Millisecond);

        public DataGenerator(long quantity)
        {
            this.quantity = quantity;
        }

        public void BranchBound()
        {
            random = new Random((int)Math.Pow(DateTime.Now.Millisecond, total) + random.Next(10, 5000) * random.Next(1, 10));
            try
            {
                Parallel.Invoke(CreateDesigner, CreateUbication, CreateWork);
            }
            catch
            {
                #if DEBUG
                Console.WriteLine("BTotal: {0}   Memory usage: {1}MB", total, (System.GC.GetTotalMemory(true) / 1000000).ToString());
                #endif
            }
            Console.Clear();
            Console.WriteLine("Memory usage: {0}MB", (System.GC.GetTotalMemory(true) / 1000000).ToString());
        }

        private void CreateDesigner()
        {
            try { 
                for (long counter = 0; counter < quantity; counter++)
                {
                    Percent();

                    Program.designerList.Add(new Designer(Program.designerList.Count + 1, Name(random), DesignerPrice(random)));
                    total++; 
                }
            }
            catch
            {
#if DEBUG
                Console.WriteLine("DTotal: {0}   Memory: {1}MB", total, (System.GC.GetTotalMemory(true) / 1000000).ToString());
#endif
            }
        }
        private void CreateWork()
        {
            try
            {
                for (long counter = 0; counter < quantity; counter++)
                {
                    Program.workList.Add(new Work(Program.workList.Count + 1));
                    total++;
                }
            }
            catch
            {
#if DEBUG
                Console.WriteLine("WTotal: {0}   Memory: {1}MB", total, (System.GC.GetTotalMemory(true) / 1000000).ToString());
#endif
            }
        }
        private void CreateUbication()
        {
            try
            {
                for (long counter = 0; counter < quantity; counter++)
                {
                    Program.ubicationList.Add(new Ubication(Program.ubicationList.Count + 1, Name(random), random.Next(0, 3), random.Next(0, 3)));
                    total++;
                }
            }
            catch
            {
#if DEBUG
                Console.WriteLine("UTotal: {0}   Memory: {1}MB", total, (System.GC.GetTotalMemory(true) / 1000000).ToString());
#endif
            }
        }

        public void Percent()
        {
            if (total >= cont || total==quantity)
            {
                Console.Clear();
                double temp = (((double)total)/(((double)3) * ((double)quantity)))*((double)100);
                Console.WriteLine("Data created: {0}%       Memory usage: {1}MB", (int)temp,(System.GC.GetTotalMemory(true)/ 1000000));
                cont += ((6 * quantity) / 20);
            }
        }


        private Dictionary<int,double> DesignerPrice(Random random)
        {
            Dictionary<int, double> dic = new Dictionary<int, double>();
            int limit = random.Next(1, 5);
            for(int x = 1; x <= limit; x++)
            {
                dic.Add(x,random.Next(500, 8000001));
            }
            return dic;
        }

        private string Name(Random random)
        {
            String name = "";
            try
            {
                int limit = random.Next(3, 13);
                name += (char)(random.Next(65, 91));

                for (int x = 0; x < limit; x++)
                {
                    name += (char)(random.Next(97, 123));
                }
            }
            catch { }

            return name;
        }
    }
}
