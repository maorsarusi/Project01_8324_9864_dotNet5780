using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public enum ResortType
    {
        Zimmer = 1,
        HostingApartment = 2,
        HotelRoom = 3,
        Camp = 4,
    }
    public enum Areas
    {
        All = 0,
        North = 1,
        South = 2,
        GushDan = 3,
        Jerusalem = 4,
        Arava = 5
    }
    public enum OrderStatus
    {
        NotYetAdressed = 1,
        SendEmail = 2,
        NoResponse = 3,
        Responsiveness = 4
    }
    public enum GuestRequestStatus
    {
        Opened = 1,
        ClosedWebsite = 2,
        Expired = 3
    }
    public enum Options
    {
        Necessary = 1,
        Possible = 2,
        NotInterest = 3
    }
    public enum Layers
    {
        LayerBE = 0,
        LayerDal = 1,
        LayerBl = 2,
        LayerPl = 3,
    }
}
