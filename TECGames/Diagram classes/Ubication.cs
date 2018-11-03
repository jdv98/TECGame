using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECGames.Diagram_classes
{
    class Ubication
    {
        private int id;
        private string name;
        private int scheduleNocturnal;
        private int scheduleDiurnal;

        public int Id{ get => id; set => id= value; }
        public string UbicationName{ get => name; set => name= value; }
        public int ScheduleNocturnal { get => scheduleNocturnal; set => scheduleNocturnal = SN(value); }
        public int ScheduleDiurnal { get => scheduleDiurnal; set => scheduleDiurnal = value; }

        public Ubication()
        {
        }

        public Ubication(int id, string ubicationName, int scheduleNocturnal,int scheduleDiurnal)
        {
            Id = id;
            UbicationName = ubicationName;

            Random x = new Random(DateTime.Now.Millisecond);
            if (scheduleNocturnal!=0 || scheduleDiurnal != 0)
            {
                ScheduleDiurnal = scheduleDiurnal;
                ScheduleNocturnal = scheduleNocturnal;
            }
            else if(x.Next(0,2)==0)
            {
                ScheduleDiurnal = x.Next(1, 3);
                ScheduleNocturnal = 0;
            }
            else
            {
                ScheduleDiurnal = 0;
                ScheduleNocturnal = x.Next(1, 3);
            }
            Console.WriteLine("N={0}    D={1}",ScheduleNocturnal,ScheduleDiurnal);
        }

        private int SN(int x)
        {
            if (x == 0)
            {
                return 0;
            }
            else if (x == 1)
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }

        public int[] GetSchedules()
        {
            int[] x = new int[2];
            x[0] = ScheduleDiurnal;
            x[1] = ScheduleNocturnal;
            return x;
        }
    }
}
