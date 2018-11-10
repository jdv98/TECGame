using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TECGames.Diagram_classes;

namespace TECGames.Genetic_Algorithm
{

    class GeneticAlgorithm
    {
        //List to contein valid works to start genetic algorithm.
        public List<Work> works = new List<Work>();

        //List to contein the actual generation.
        public List<Tuple<Work, Work>> actualGeneration = new List<Tuple<Work, Work>>();


        //Contructor of n quantity generations that is recommended to get a real minimization on price.
        public GeneticAlgorithm()
        {
            Console.Clear();
            //Load data (works)
            foreach (Work w in Program.workList)
            {
                if (w.WorkSection != null)
                {
                    works.Add(w);
                }
            }

            int flag = -1;
            do
            {
                Courtship(works);
                 
                if (flag == -1)
                {
                    flag = actualGeneration.Count;
                    Console.WriteLine("PARENTS: parent 1 <-> parent 2 \n");
                    foreach (Tuple<Work, Work> tuple in actualGeneration)
                    {
                        Console.WriteLine("ID: " + tuple.Item1.Id + " , PRICE: " + tuple.Item1.WorkSection.Price + " <-> ID: " + tuple.Item2.Id + " , PRICE: " + tuple.Item2.WorkSection.Price);
                    }
                }
                Crossing();
                UpadateWorks();
                flag--;
            } while (flag > 0);
            Console.WriteLine("\n" + actualGeneration.Count + " GENERATIONS LATER...\n");
            foreach (Tuple<Work, Work> tuple in actualGeneration)
            {
                Console.WriteLine("ID: " + tuple.Item1.Id + " , PRICE: " + tuple.Item1.WorkSection.Price + " <-> ID: " + tuple.Item2.Id + " , PRICE: " + tuple.Item2.WorkSection.Price);
            }
            Console.WriteLine("\nRESULT: ");
            //Sort works data lower price to higher price.
            works.Sort((a, b) => a.WorkSection.Price.CompareTo(b.WorkSection.Price));
            Work resultWork = works[0];
            Console.WriteLine("ID: " + resultWork.Id);
            Console.Write("DESIGNER(S): ");
            foreach (Designer d in resultWork.Designers)
            {
                Console.Write(d.Name + ". ");
            }
            Console.WriteLine("\nUBICATION: " + resultWork.Ubication.UbicationName);
            Console.WriteLine("WORK SECTION: " + resultWork.WorkSection.Name);
            Console.WriteLine("PRICE: " + resultWork.WorkSection.Price);
        }


        //Contructor receives n quantity of generations that user want.
        public GeneticAlgorithm(int n) 
        {
            Console.Clear();
            //Load data (works)
            foreach (Work w in Program.workList)
            {
                if (w.WorkSection != null)
                {
                    works.Add(w);
                }
            }

            int flag = n;
            //Begin of genetic algorithm process. Start a process of n generations.
            while (n > 0)
            {
                Courtship(works);
                if (flag == n)
                {
                    Console.WriteLine("PARENTS: parent 1 <-> parent 2 \n");
                    foreach (Tuple<Work, Work> tuple in actualGeneration)
                    {
                        Console.WriteLine("ID: "+tuple.Item1.Id+ " , PRICE: " + tuple.Item1.WorkSection.Price+ " <-> ID: " + tuple.Item2.Id+ " , PRICE: " + tuple.Item2.WorkSection.Price);
                    }
                }
                Crossing();
                UpadateWorks();
                n--;
            }
            Console.WriteLine("\n"+flag+ " GENERATIONS LATER...\n");
            foreach (Tuple<Work, Work> tuple in actualGeneration)
            {
                Console.WriteLine("ID: " + tuple.Item1.Id + " , PRICE: " + tuple.Item1.WorkSection.Price + " <-> ID: " + tuple.Item2.Id + " , PRICE: " + tuple.Item2.WorkSection.Price);
            }
            Console.WriteLine("\nRESULT: ");
            //Sort works data lower price to higher price.
            works.Sort((a, b) => a.WorkSection.Price.CompareTo(b.WorkSection.Price));
            Work resultWork = works[0];
            Console.WriteLine("ID: "+resultWork.Id);
            Console.Write("DESIGNER(S): ");
            foreach(Designer d in resultWork.Designers)
            {
                Console.Write(d.Name + ". ");
            }
            Console.WriteLine("\nUBICATION: " + resultWork.Ubication.UbicationName);
            Console.WriteLine("WORK SECTION: "+resultWork.WorkSection.Name);
            Console.WriteLine("PRICE: " + resultWork.WorkSection.Price);
        }


        //From works data get the suitable pairs and save that pairs in actualGeneration list. 
        public void Courtship(List<Work> works) {

            //Sort works data lower price to higher price.
            works.Sort((a, b) => a.WorkSection.Price.CompareTo(b.WorkSection.Price));

            foreach (Work w in works)
            {
                for (int i = works.Count-1; i >= 0; i--) {

                    if (w.Id != works[i].Id && w.WorkSection.Price > works[i].WorkSection.Price && w.WorkSection.Schedule == works[i].WorkSection.Schedule) {
                        //Create a tuple that represent the relation (the parents).
                        if (!actualGeneration.Contains(Tuple.Create(w, works[i])))
                        {
                            actualGeneration.Add(Tuple.Create(w, works[i]));
                        }
                        
                    }
                }
            }
        }

        //Take each pair or parents to cross and evaluate the two posible children. 
        //It's important to know that if offspring is better than parents the offsprig substitute the parent(s).  
        public void Crossing()
        {
            try {
                foreach (Tuple<Work, Work> tuple in actualGeneration)
                {

                    // Offspring (two new instances of work combining attributes of their parents):
                    Work offspring1 = new Work(0, tuple.Item1.Designers, tuple.Item2.Ubication, new WorkSection(0, "", tuple.Item1.WorkSection.Schedule));
                    offspring1.WorkSection.Price = 0;
                    offspring1.Price();

                    Work offspring2 = new Work(0, tuple.Item2.Designers, tuple.Item1.Ubication, new WorkSection(0, "", tuple.Item2.WorkSection.Schedule));
                    offspring2.WorkSection.Price = 0;
                    offspring2.Price();

                    //Creating temp pointers 
                    Work oshigher, oslower = null;
                    if (offspring1.WorkSection.Price >= offspring2.WorkSection.Price)
                    {
                        oshigher = offspring1;
                        oslower = offspring2;
                    }
                    else
                    {
                        oshigher = offspring2;
                        oslower = offspring1;
                    }

                    //Ask if offpring is better (have cheap work section) than parents.
                    if (tuple.Item1.WorkSection.Price > oslower.WorkSection.Price)
                    {
                        oslower.Id = tuple.Item1.Id;
                        oslower.WorkSection.Id = tuple.Item1.WorkSection.Id;
                        oslower.WorkSection.Name = tuple.Item1.WorkSection.Name;
                        oslower.WorkSection.Schedule = tuple.Item1.WorkSection.Schedule;

                        if (tuple.Item2.WorkSection.Price > oshigher.WorkSection.Price)
                        {
                            oshigher.Id = tuple.Item2.Id;
                            oshigher.WorkSection.Id = tuple.Item2.WorkSection.Id;
                            oshigher.WorkSection.Name = tuple.Item2.WorkSection.Name;
                            oshigher.WorkSection.Schedule = tuple.Item2.WorkSection.Schedule;
                            UpdateParents(oshigher);
                        }
                        UpdateParents(oslower);
                    }
                    else if (tuple.Item2.WorkSection.Price > oslower.WorkSection.Price)
                    {
                        oslower.Id = tuple.Item2.Id;
                        oslower.WorkSection.Id = tuple.Item2.WorkSection.Id;
                        oslower.WorkSection.Name = tuple.Item2.WorkSection.Name;
                        oslower.WorkSection.Schedule = tuple.Item2.WorkSection.Schedule;
                        UpdateParents(oslower);
                    }
                }
            }
            catch { }
            
            
        }

        //Update parents (actualGeneration) data.
        public void UpdateParents(Work work)
        {
            for(int i = 0; i< actualGeneration.Count; i++)
            {
                if(actualGeneration[i].Item1.Id == work.Id)
                {
                    actualGeneration[i] = Tuple.Create(work, actualGeneration[i].Item2);
                }
                else if(actualGeneration[i].Item2.Id == work.Id)
                {
                    actualGeneration[i] = Tuple.Create(actualGeneration[i].Item1, work);
                }
            }
        }
        
        //Update works list referent to actualGeneration.
        public void UpadateWorks()
        {
            works.Clear();
            foreach(Tuple<Work, Work> tuple in actualGeneration)
            {
                if(!works.Contains(tuple.Item1))
                {
                    works.Add(tuple.Item1);
                }
                else if (!works.Contains(tuple.Item2))
                {
                    works.Add(tuple.Item2);
                }

            }
        }
    }
}
