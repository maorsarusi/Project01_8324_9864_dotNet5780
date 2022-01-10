using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BE
{
    public class GuestRequest
    {
        
        public int GuestRequestKey { get; set; }

        public string PrivateName { get; set; }

        public string FamilyName { get; set; }

        public string MailAddress { get; set; }

        private GuestRequestStatus m_statusGuestRequest = GuestRequestStatus.Opened;
        public GuestRequestStatus StatusGuestRequest { get { return m_statusGuestRequest; } set { m_statusGuestRequest = value; } }

        private DateTime m_registrationDate = DateTime.Today;
        public DateTime RegistrationDate { get { return m_registrationDate; } set { m_registrationDate = value; } }

        private DateTime m_entryDate = DateTime.Today;
        public DateTime EntryDate { get { return m_entryDate; } set { m_entryDate = value; } }

        private DateTime m_releaseDate = DateTime.Today;
        public DateTime ReleaseDate { get { return m_releaseDate; } set { m_releaseDate = value; } }
      
        public Areas Area { get; set; }

        public string SubArea { get; set; }

        private ResortType m_type;
        public ResortType Type { get { return m_type; } set { m_type =value ; } }

        private int m_adults = 1;
        public int Adults { get { return m_adults; } set { m_adults = value; } }

        public int Children { get; set; }

        private Options m_pool = Options.Possible;
        public Options Pool { get { return m_pool; } set { m_pool = value; } }

        private Options m_jacuzzi = Options.Possible;
        public Options Jacuzzi { get { return m_jacuzzi; } set { m_jacuzzi = value; } }

        private Options m_garden = Options.Possible;
        public Options Garden { get { return m_garden; } set { m_garden = value; } }

        private Options m_childrensAttractions = Options.Possible;
        public Options ChildrensAttractions { get { return m_childrensAttractions; } set { m_childrensAttractions = value; } }

        public override string ToString()
        {
            //return String.Format("Guest request key: {0}\nGuest: {1} {2}\nEmail adress: {3}\nRegistration for the system day: {4}\nEntry date: {5}\nRealease date: {6}\nArea: {7}\nSub area: {8}\nResort type: {9}\nNum of aduults: {10}\nNum of childrens: {11}\nPool status: {12}\nJacuzzi status: {13}\nGarden status: {14}\nChildren's attractions status: {15}\nGuest request status: {16}\n", GuestRequestKey, PrivateName, FamilyName, MailAddress, RegistrationDate, EntryDate, ReleaseDate, Area, SubArea, Type, Adults, Children, Pool, Jacuzzi, Garden, ChildrensAttractions, StatusGuestRequest);
            return String.Format("Guest request key: {0}\nRegistration for the system day: {4}\nEntry date: {5}\nRealease date: {6}\nArea: {7}\nSub area: {8}\nResort type: {9}\nNum of aduults: {10}\nNum of childrens: {11}\nPool status: {12}\nJacuzzi status: {13}\nGarden status: {14}\nChildren's attractions status: {15}\nGuest request status: {16}\n", GuestRequestKey, PrivateName, FamilyName, MailAddress, RegistrationDate, EntryDate, ReleaseDate, Area, SubArea, Type, Adults, Children, Pool, Jacuzzi, Garden, ChildrensAttractions, StatusGuestRequest);
        }
    }
}
