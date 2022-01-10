using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE
{
    [Serializable]
    public class HostingUnit
    {

        private DateTime m_lastApdate = DateTime.Today;
        public DateTime LastApdate { get { return m_lastApdate; } set { m_lastApdate = value; } }

        public int HostingUnitKey { get; set; }

        public Host Owner { get; set; }

        public string HostingUnitName { get; set; }

        private bool[,] m_diary = new bool[12, 31];

        [XmlIgnore]
        public bool[,] Diary
        {
            get { return m_diary; }
            set { m_diary = value; }
        }
        [XmlArray("Diary")]
        public bool[] DiaryDto
        {
            get { return m_diary.Flatten(); }
            set { m_diary = value.Expand(12); }
        }

        public Areas Area { get; set; }

        public string SubArea { get; set; }

        public ResortType Type { get; set; }

        private int m_capacity = 1;
        public int Capacity { get { return m_capacity; } set { m_capacity = value; } }

        public bool Pool { get; set; }

        public bool Jacuzzi { get; set; }

        public bool Garden { get; set; }

        public bool ChildrensAttractions { get; set; }

        public override string ToString()
        {
            return string.Format("Unit key: {0}\nOwner: {1}\nName of unit: {2}\nArea: {3}\nSubArea: {4}\nType: {5}\nCapacity: {6}\nPool: {7}\nJacuzzi: {8}\nGarden: {9}\nChildrensAttractions: {10}\n", HostingUnitKey, Owner, HostingUnitName, Area, SubArea, Type, Capacity, Pool, Jacuzzi, Garden, ChildrensAttractions);
        }

        //מדובר באינדקסר של הוסטינג יוניט ולא של דיירי - חשוב לא להתבלבל
        //מדובר באינדקסר של היחידה ולא של לוח השנה
        public bool this[DateTime date]
        {
            get { return Diary[date.Month - 1, date.Day - 1]; }
            set { Diary[date.Month - 1, date.Day - 1] = value; }
        }
    }
}
