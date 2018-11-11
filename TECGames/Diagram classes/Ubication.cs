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
        private List<KeyValuePair<int, string>> schedule = new List<KeyValuePair<int, string>>();
        private String hexId = "";      //posible a eliminar
        public bool linked = false;

        public int Id { get => id; set => id= value; }
        public string UbicationName{ get => name; set => name= value; }
        public List<KeyValuePair<int, string>> Schedule { get => schedule; set => schedule = value; }
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
                case 0:
                    flagN = true;
                    break;
                default:
                    Schedule.Add(new KeyValuePair<int, string>(SN(nocturnal), Program.schedules[nocturnal]));
                    break;
            }
            switch (diurnal)
            {
                case 0:
                    flagD = true;
                    break;
                default:
                    Schedule.Add(new KeyValuePair<int, string>(diurnal, Program.schedules[diurnal]));
                    break;
            }

            if (flagD && !flagN)
            {
                Schedule.Add(new KeyValuePair<int, string>(diurnal, Program.schedules[diurnal]));
            }
            else if (flagN && !flagD)
            {
                Schedule.Add(new KeyValuePair<int, string>(SN(nocturnal), Program.schedules[SN(nocturnal)]));
            }
            else if (flagN && flagD)
            {
                Schedule.Add(new KeyValuePair<int, string>(x.Next(1, 3), Program.schedules[x.Next(1, 3)]));
                Schedule.Add(new KeyValuePair<int, string>(SN(x.Next(1, 3)), Program.schedules[SN(x.Next(1, 3))]));
            }
        }

        private int SN(int x)
        {
            switch (x)
            {
                case 1:
                    return 3;
                case 2:
                    return 4;
                default:
                    return 0;
            }
        }
    }
}
