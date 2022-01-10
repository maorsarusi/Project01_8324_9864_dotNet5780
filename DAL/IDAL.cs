using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface IDAL
    {
        Host GetHostByKey(int Key);
        void AddHost(Host owner);

        GuestRequest GetGuestRequest(int Key);
        void AddGuestRequest(GuestRequest gs);
        void UpdateGuestRequest(GuestRequest gr);

        Order GetOrder(int Key);
        int AddUnit(HostingUnit hu);
        void DelUnit(HostingUnit hu);
        void UpdateUnit(HostingUnit hu);

        HostingUnit GetHostingUnit(int Key);
        int AddOrder(Order or);
        void UpdateOrderStatus(Order or);

        IEnumerable<Host> GetAllHosts();
        IEnumerable<HostingUnit> GetAllUnits();
        IEnumerable<GuestRequest> GetAllGuestRequests();
        IEnumerable<Order> GetAllOrders();
        IEnumerable<BankBranch> GetAllBankBranchs();

        void AddOrderList();
        void AddHostingUnitList();
        void AddHostList();
        void AddGuestRequestList();
        void DownloadBankXml();
    }
}
