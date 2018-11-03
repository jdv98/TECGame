using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TECGames.Diagram_classes;

namespace TECGames
{
    class DataGenerator
    {
        public DataGenerator()
        {
        }

        public void BranchBound(int quantity)
        {
            Random random=new Random(DateTime.Now.Millisecond);
            for (int id = 0; id < quantity; id++)
            {
                random = new Random((int)Math.Pow(DateTime.Now.Millisecond,id) + random.Next(10, 5000));
                Work nW = new Work(id);
                nW.Ubication = new Ubication(quantity + id,Name(random),random.Next(0,3),random.Next(0,3));
                nW.WorkSection = new WorkSection((int)Math.Pow(quantity, 2)+id,Name(random),random.Next(500,8000000), WSSchedule(nW.Ubication.GetSchedules(),random.Next(0,2)));
                int designerLimit = random.Next(1, 11);
                for(int x = 0; x < designerLimit; x++)
                {
                    random = new Random((int)Math.Pow(DateTime.Now.Millisecond, x)+random.Next(10,5000));
                    nW.Designers.Add(new Designer((int)Math.Pow(quantity, 3 + id) + (id+x), Name(random), nW.WorkSection.Price, WSSchedule(nW.Ubication.GetSchedules(), random.Next(0, 2))));
                }
                foreach( Designer x in nW.Designers)
                {
                    Console.WriteLine("Id={0}   Name={1}    Price={2}   WS={3}", x.Id, x.Name, x.Price, x.WorkSection);
                }
            }
        }

        private int WSSchedule(int[] schedules,int flag)
        {
            if (schedules[0]!=0 && flag == 1)
            {
                return schedules[0];
            }
            else if (schedules[1] != 0 && flag == 0)
            {
                return schedules[1];
            }
            else if(schedules[0] != 0)
            {
                return schedules[0];
            }
            else
            {
                return schedules[1];
            }
        }

        private string Name(Random random)
        {
            String name = "";
            int limit = random.Next(4, 13);
            name += (char)(random.Next(65,91));

            for(int x = 0; x < limit; x++)
            {
                name+= (char)(random.Next(97, 123));
            }

            return name;
        }
    }
}
