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
            random = new Random(random.Next(1, 800000) * (random.Next(1,80000)+DateTime.Now.Millisecond));
        }

        /*Creates data in parallel to fill the global lists*/
        public void DataCreator()
        {
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

        /****************************************************/
        /*Segment of functions that creates Designers, works and ubication separately*/
        private void CreateDesigner()
        {
            try
            {
                for (int counter = 0; counter < 2 * quantity; counter++)
                {
                    Percent();

                    Program.designerList.Add(new Designer(Program.designerList.Count + 1, "Name" + (Program.designerList.Count + 1).ToString(), DesignerPrice(random)));
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
                    Program.ubicationList.Add(new Ubication(Program.ubicationList.Count + 1, "Name" + (Program.ubicationList.Count + 1).ToString(), random.Next(0,3), random.Next(0, 3)));
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
        /****************************************************/

        /*Prints the percent of data that have been created*/
        private void Percent()
        {
            if (total >= cont || total == quantity)
            {
                Console.Clear();
                double temp = (((double)total) / (((double)3) * ((double)2 * (double)quantity))) * ((double)100);
                Console.WriteLine("Data created: {0}%       Memory usage: {1}MB", (int)temp, (System.GC.GetTotalMemory(true) / 1000000));
                cont += ((6 * quantity) / 20);
            }
        }

        /*Insert the payment that the designer wants for a determinate schedule*/
        private Dictionary<int, double> DesignerPrice(Random random)
        {
            Dictionary<int, double> dic = new Dictionary<int, double>();
            int limit = random.Next(1, 5);
            for (int x = 1; x <= limit; x++)
            {
                dic.Add(x, random.Next(500, 8000001));
            }
            return dic;
        }

        /*Creates a random name of 4 to 13 letters*/
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
            int indexLinkDesigner = 0; ;
            bool added;

            foreach (Work work in Program.workList)
            {
                Console.Clear();
                Console.WriteLine("Work: {0}", Program.workList.Count - (work.Id + 1));
                random = new Random(work.Id * random.Next(1, 800000) * (random.Next(1, 80000) + DateTime.Now.Millisecond));
                foreach (Ubication ubication in Program.ubicationList)
                {
                    int cont = Program.designerList.Count;
                    if (!ubication.linked)
                    {

                        for (int designerIndex = 0; designerIndex < 2; designerIndex++)
                        {
                            added = false;
                            while (!added)
                            {
                                indexLinkDesigner = random.Next(0, Program.designerList.Count);
                                foreach (KeyValuePair<int, double> x in Program.designerList[indexLinkDesigner].Price)
                                {
                                    foreach (KeyValuePair<int, string> y in ubication.Schedule)
                                    {
                                        if (x.Key == y.Key && work.Ubication == null)
                                        {
                                            Program.workList.ElementAt((work.Id - 1)).Ubication = Program.ubicationList.ElementAt((ubication.Id - 1));
                                            Program.workList.ElementAt((work.Id - 1)).Designers.Add(Program.designerList.ElementAt((indexLinkDesigner)));
                                            Program.workList.ElementAt(work.Id - 1).WorkSection = new WorkSection(work.Id, Name(random), x.Key);
                                            Program.designerList.ElementAt(indexLinkDesigner).linked = true;
                                            Program.ubicationList.ElementAt((ubication.Id - 1)).linked = true;
                                            Program.workList.ElementAt((work.Id - 1)).linked = true;
                                            added = true;
                                        }
                                        else if (work.WorkSection != null && x.Key == work.WorkSection.Schedule && !Program.designerList[indexLinkDesigner].linked)
                                        {
                                            Program.designerList.ElementAt(indexLinkDesigner).linked = true;
                                            Program.workList.ElementAt((work.Id - 1)).Designers.Add(Program.designerList.ElementAt((indexLinkDesigner)));
                                            added = true;
                                        }
                                    }
                                }
                            }
                            Program.designerList.ElementAt(indexLinkDesigner).linked = false;
                        }
                        if (work.linked)
                        {
                            Program.workList.ElementAt(work.Id - 1).Price();
                            break;
                        }
                    }

                }
            }

            Console.Clear();
        }

    }
}
