using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using TECGames.Diagram_classes;

namespace TECGames
{
    class DataManagement
    {
        int cont = 0;
        int total = 0;
        int quantity;
        Random random = new Random(DateTime.Now.Millisecond);

        public DataManagement(int quantity)
        {
            this.quantity = quantity;
        }

        /*      Create data to fill the lists      */
        public void DataCreator()
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
                for (int counter = 0; counter < 2*quantity; counter++)
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
                for (int counter = 0; counter < quantity; counter++)
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
                for (int counter = 0; counter < quantity; counter++)
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
        private void Percent()
        {
            if (total >= cont || total==quantity)
            {
                Console.Clear();
                double temp = (((double)total)/(((double)3) * ((double)2*(double)quantity)))*((double)100);
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

        
        
        /*      it associates data      */
        public void Linker()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            random = new Random(random.Next(10,1001)*random.Next(50,5001));
            int designers = 0;

            foreach (Work work in Program.workList)
            {
                Console.Clear();
                Console.WriteLine("Work: {0}", Program.workList.Count - (work.Id+1));
                designers = random.Next(1, 2);

                foreach(Ubication ubication in Program.ubicationList)
                {
                    int cont = Program.designerList.Count;
                    if (!ubication.linked)
                    {
                        foreach (Designer designer in Program.designerList)
                        {
                            /*__________________________________________________*/
                            if (!designer.linked && designers>0)
                            {
                                foreach (KeyValuePair<int, double> x in designer.Price)
                                {
                                    foreach (KeyValuePair<int, string> y in ubication.Schedule)
                                    {
                                        if (x.Key == y.Key && work.Ubication == null)
                                        {
                                            Program.workList.ElementAt((work.Id-1)).Ubication = Program.ubicationList.ElementAt((ubication.Id - 1));
                                            Program.workList.ElementAt((work.Id-1)).Designers.Add(Program.designerList.ElementAt((designer.Id-1)));
                                            Program.workList.ElementAt(work.Id - 1).WorkSection = new WorkSection(work.Id,Name(random),x.Key);
                                            
                                            /*temp*/
                                            Program.designerList.ElementAt(designer.Id - 1).linked = true;
                                            /*temp*/

                                            Program.ubicationList.ElementAt((ubication.Id-1)).linked = true;
                                            Program.workList.ElementAt((work.Id - 1)).linked = true;
                                            designers--;
                                        }
                                        else if (work.WorkSection!=null && x.Key == work.WorkSection.Schedule)
                                        {
                                            /*temp*/
                                            Program.designerList.ElementAt(designer.Id - 1).linked = true;
                                            /*temp*/

                                            Program.workList.ElementAt((work.Id - 1)).Designers.Add(Program.designerList.ElementAt((designer.Id - 1)));
                                            designers--;
                                        }
                                    }
                                }
                            }
                            else if (designers == 0)
                            {
                                break;
                            }
                        }
                        if (work.linked)
                        {
                            Program.workList.ElementAt(work.Id - 1).Price();
                            break;
                        }
                    }
                    
                }
            }
        }

        public void Unlinker()
        {
            for (int wIndex = 0; wIndex < Program.workList.Count; wIndex++)
            {
                Program.workList.ElementAt(wIndex).linked = false;
            }
            for (int uIndex = 0; uIndex < Program.ubicationList.Count; uIndex++)
            {
                Program.ubicationList.ElementAt(uIndex).linked = false;
            }
            for (int dIndex = 0; dIndex < Program.designerList.Count; dIndex++)
            {
                Program.designerList.ElementAt(dIndex).linked = false;
            }
        }

        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
        }
    }
}
