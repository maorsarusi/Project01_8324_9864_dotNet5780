using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{

    public interface IBL
    {
        Host GetHostByKey(int key);
        void AddHost(Host owner);

        int Duration(GuestRequest gr);
        int Duration(DateTime time1, DateTime time2);
        int Duration(DateTime time);

        GuestRequest GetGuestRequest(int Key);
        void AddGuestRequest(GuestRequest gs);
        void UpdateGuestRequest(GuestRequest gr, GuestRequestStatus update);
        //void DelGuestRequest(GuestRequest gr);

        Order GetOrder(int Key);
        int AddUnit(HostingUnit hu);
        void DelUnit(HostingUnit hu);
        void UpdateUnit(HostingUnit hu);

        HostingUnit GetHostingUnit(int Key);
        int AddOrder(Order or);
        void UpdateOrderStatus(Order or, OrderStatus update);
        void SendMail(Order or);

        IEnumerable<Host> GetAllHosts();
        IEnumerable<GuestRequest> GetAllGuestRequests();
        IEnumerable<HostingUnit> GetAllUnits();
        IEnumerable<Order> GetAllOrders();
        IEnumerable<BankBranch> GetAllBankBranchs();

        IEnumerable<HostingUnit> GetAllAvailableUnits(DateTime date, int duration);
        IEnumerable<Order> GetOrdersByMinimumDistanceFromCreationTime(int numOfDays);
        IEnumerable<Order> GetOrdersByMinimumDistanceFromSendingMailTime(int numOfDays);
        int numOfOrders(GuestRequest guestRequest);
        int NumOfOrdersForUnit(HostingUnit hostingUnit);
        IEnumerable<Order> OrdersForUnit(HostingUnit hu);
        IEnumerable<GuestRequest> MatchingConditions(Func<GuestRequest, bool> predicate = null);
        IEnumerable<GuestRequest> GetAllRequirementsMatchingToTheUnit(HostingUnit hu);
        int NumberOfUnitsThatMatchingTheRequirement(GuestRequest gr);

        IEnumerable<IGrouping<Areas, GuestRequest>> GuestRequestsGroupingByArea();
        IEnumerable<IGrouping<int, GuestRequest>> GuestRequestsGroupingByNumOfVacationers();
        IEnumerable<IGrouping<int, Host>> hostsGroupingByNumberOfUnits();
        IEnumerable<IGrouping<Areas, HostingUnit>> hostingUnitsGroupingByArea();

        void AddOrderList();
        void AddHostingUnitList();
        void AddHostList();
        void AddGuestRequestList();
        void DownloadBankXml();
    }
}