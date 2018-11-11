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
        private Dictionary<int, string> schedule = new Dictionary<int, string>();
        private String hexId = "";      //posible a eliminar
        public bool linked = false;

        public int Id { get => id; set => id= value; }
        public string UbicationName{ get => name; set => name= value; }
        public Dictionary<int,string> Schedule { get => schedule; set => schedule = value; }
        public string HexId { get => hexId; set => hexId = value; }

        public Ubication(int id, string ubicationName, int scheduleNocturnal,int scheduleDiurnal)
        {
            Id = id;
            HexId = Convert.ToString(id, 16);
            UbicationName = ubicationName;
            SetSchedule(scheduleNocturnal, scheduleDiurnal);
        }

        /*Inserts information in the schedule list and confirms that it's not empty*/
        private void SetSchedule(int nocturnal, int diurnal)
        {
            Random x = new Random(DateTime.Now.Millisecond);
            bool flagN = false;
            bool flagD = false;

            switch (nocturnal)
            {
                case 5:
                    flagN = true;
                    break;
                default:
                    Schedule.Add(nocturnal, Program.schedules[nocturnal]);
                    break;
            }
            switch (diurnal)
            {
                case 0:
                    flagD = true;
                    break;
                default:
                    Schedule.Add(diurnal, Program.schedules[diurnal]);
                    break;
            }

            if (flagD && !flagN)
            {
                Schedule.Add(diurnal, Program.schedules[diurnal]);
            }
            else if (flagN && !flagD)
            {
                Schedule.Add(nocturnal, Program.schedules[nocturnal]);
            }
            else if (flagN && flagD)
            {
                Schedule.Add(x.Next(1, 3), Program.schedules[x.Next(1, 3)]);
                Schedule.Add(x.Next(3, 5), Program.schedules[x.Next(3, 5)]);
            }
        }
    }
}
