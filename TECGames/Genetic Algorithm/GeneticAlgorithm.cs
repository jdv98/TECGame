using System;
using System.Collections.Generic;
using System.Linq;
using TECGames.Diagram_classes;

namespace TECGames.Genetic_Algorithm
{
    class GeneticAlgorithm
    {
        //List to contein valid works to start genetic algorithm. :D
        public List<Work> works = new List<Work>();

        //Contructor receives n quantity of generations that user want. xD
        public GeneticAlgorithm(int n) 
        {
            Console.Clear();
            //Load data (works) ->
            foreach (Work w in Program.workList)
            {
                if (w.WorkSection != null)
                {
                    works.Add(w);
                }
            }

            PrintData();

            int flag = n;
            //Begin of genetic algorithm process. Start a process of n generations. :,)
            while (n > 0)
            {
                Courtship(works);
                n--;
            }
            Console.WriteLine("\n" + flag + " GENERATIONS LATER...\n");
            PrintData();
        }

        //From works data proposes pairs to get croos or else the pairs get mutated. <3
        public void Courtship(List<Work> works) {

            var rnd = new Random(DateTime.Now.Millisecond);
            int i = rnd.Next(0, works.Count);
            int j = rnd.Next(0, works.Count);

            if (works[i].Id != works[j].Id)
            {
                if(Evaluate(works[i], works[j]) != -1 && Evaluate(works[j], works[i]) != -1)
                {
                    Crossing(works[i], Evaluate(works[i], works[j]), works[j], Evaluate(works[j], works[i]));
                }
                else
                {
                    Mutation(works[i], works[j]);
                }
            }
        }

        //Take each pair of parents to cross and evaluate the two posible children. 
        //It's important to know that if offspring is better than parents the offsprig substitute the parent(s). :o  
        public void Crossing(Work work1, int work1Ubi, Work work2, int work2Ubi)
        {
            Work offspring1 = new Work(-1);
            offspring1.Ubication = work1.Ubication;
            offspring1.Designers = new List<Designer>();
            foreach (Designer d in work2.Designers)
            {
                if (d.Price.ContainsKey(work1Ubi))
                {
                    offspring1.Designers.Add(d);
                }
            }
            string name;
            Program.schedules.TryGetValue(work1Ubi, out name);
            offspring1.WorkSection = new WorkSection(-1, name, work1Ubi);
            offspring1.Price();

            Work offspring2 = new Work(-1);
            offspring2.Ubication = work2.Ubication;
            offspring2.Designers = new List<Designer>();
            foreach (Designer d in work1.Designers)
            {
                if (d.Price.ContainsKey(work2Ubi))
                {
                    offspring2.Designers.Add(d);
                }
            }
            string name2;
            Program.schedules.TryGetValue(work2Ubi, out name2);
            offspring2.WorkSection = new WorkSection(-1, name2, work2Ubi);
            offspring2.Price();

            //temporal pointers 
            Work osHigher, osLower;
            if(offspring1.WorkSection.Price >= offspring2.WorkSection.Price)
            {
                osHigher = offspring1;
                osLower = offspring2;
            }
            else
            {
                osHigher = offspring2;
                osLower = offspring1;
            }

            if(work1.WorkSection.Price >= work2.WorkSection.Price)
            {
                if(work1.WorkSection.Price >= osLower.WorkSection.Price)
                {
                    osLower.Id = work1.Id;
                    osLower.WorkSection.Id = osLower.Id;
                    UpdateWorks(osLower);
                }
                else if(work2.WorkSection.Price >= osHigher.WorkSection.Price)
                {
                    osHigher.Id = work2.Id;
                    osHigher.WorkSection.Id = osLower.Id;
                    UpdateWorks(osHigher);
                }
            }
            else
            {
                if (work2.WorkSection.Price >= osLower.WorkSection.Price)
                {
                    osLower.Id = work2.Id;
                    osLower.WorkSection.Id = osLower.Id;
                    UpdateWorks(osLower);
                }
                else if (work1.WorkSection.Price >= osHigher.WorkSection.Price)
                {
                    osHigher.Id = work1.Id;
                    osHigher.WorkSection.Id = osLower.Id;
                    UpdateWorks(osHigher);
                }
            }
        }

        //Compares work1 ubication schedules to work2 designers schedules. 
        //It returns int number schedule. ;)
        public int Evaluate(Work work1, Work work2)
        {
            //Two posible schedules for ubications.
            KeyValuePair<int, string> ubiSch1 = work1.Ubication.Schedule.ElementAt(0);
            KeyValuePair<int, string> ubiSch2 = work1.Ubication.Schedule.ElementAt(1);

            foreach (Designer d in work2.Designers)
            {
                if (d.Price.ContainsKey(ubiSch1.Key) )
                {
                    return ubiSch1.Key;
                }
                else if (d.Price.ContainsKey(ubiSch2.Key)){
                    return ubiSch2.Key;
                }
            }
            return -1;
        }

        //Subtitute the parent. :)
        public void UpdateWorks(Work w)
        {
            for (int i = 0; i < works.Count; i++)
            {
                if (works[i].Id == w.Id)
                {
                    works[i] = w;
                }
            }
        }
       
        //Foreach and console.writeLine. :v
        public void PrintData()
        {
            Console.WriteLine("Total works: " + works.Count);
            works.Sort((a, b) => a.WorkSection.Price.CompareTo(b.WorkSection.Price));
            foreach (Work w in works)
            {
                Console.WriteLine("ID: " + w.Id + " - PRICE: " + w.WorkSection.Price);
            }
        }

        //Make random changes to improve the data. (Y)
        public void Mutation(Work work1, Work work2)
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            int c = rnd.Next(0, 2);
            switch (c)
            {
                case 0:
                    MutationCase0(work1);
                    MutationCase0(work2);
                    break;
                case 1:
                    MutationCase1(work1);
                    MutationCase1(work2);
                    break;
            }
            
        }

        //This mutation apply a random discount (minimun 0% and maximun 10%) for all designers. :$
        public void MutationCase0(Work work)
        {
            for (int i = 0; i < work.Designers.Count; i++)
            {
                var rnd = new Random(DateTime.Now.Millisecond+i);
                int percent = rnd.Next(0, 10);

                for (int dP = 0; dP < work.Designers[i].Price.Count; dP++)
                {
                    work.Designers[i].Price[work.Designers[i].Price.Keys.ElementAt(dP)] -= (work.WorkSection.Price * percent) / 100;
                }
            }
        }

        //This mutation changes designers and ubication. :P
        public void MutationCase1(Work work)
        {
            for (int i = 0; i < work.Designers.Count; i++)
            {
                for (int dP = 0; dP < work.Designers[i].Price.Count; dP++)
                {
                    for (int key = 0; key < work.Designers[i].Price.Keys.Count; i++)
                    {
                        int k = work.Designers[i].Price.Keys.ElementAt(key);
                        if (work.Designers[i].Price[work.Designers[i].Price.Keys.ElementAt(dP)] < work.WorkSection.Price && k != work.WorkSection.Schedule)
                        {
                            Designer temp = work.Designers[i];
                            work.Designers.Clear();
                            work.Designers.Add(temp);
                            for (int d = 0; d < Program.designerList.Count; d++)
                            {
                                if (Program.designerList[d].Price.Keys.Contains(k))
                                {
                                    work.Designers.Add(Program.designerList[d]);
                                }
                            }
                            string name;
                            Program.schedules.TryGetValue(k, out name);
                            work.WorkSection = new WorkSection(work.Id, name, k);
                            if (k == 1 || k == 2)
                            {
                                work.Ubication = new Ubication(work.Id, "Location4MutationId" + work.Id, 0, k);
                            }
                            else if (k == 3 || k == 4)
                            {
                                work.Ubication = new Ubication(work.Id, "Location4MutationId" + work.Id, k, 0);
                            }
                            else { work.Ubication = new Ubication(work.Id, "Location4MutationId" + work.Id, 0, 0); }
                            break;
                        }
                    }
                    break;
                }
                break;
            }
        }
    }
}
