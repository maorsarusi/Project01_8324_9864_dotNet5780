using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace DAL
{
    public class Dal_imp : IDAL
    {
        public static Layers layer = Layers.LayerDal;

        //=========================================================================================================

        #region Host Function 

        //פונקציות למארח לפי מפתח
        private bool existHost(int Key)
        {
            return DataSource.hosts.Any(host => host.HostKey == Key);
        }
        public Host GetHostByKey(int Key)
        {
            Host owner = DataSource.hosts.FirstOrDefault(host => host.HostKey == Key);
            if (owner == null)
                throw new MissingKeyException(layer, "The owner not exists", Key);
            return owner.Clone();
        }
        //---------------------------------------------------------------------------------------------------------
        //פונקציות לדרישת לקוח - הוספה 
        public void AddHost(Host owner)
        {
            if (existHost(owner.HostKey))
                throw new DuplicateKeyException(layer, "The owner already exists", owner.HostKey);
            owner.HostKey = Configuration.HostKey;
            DataSource.hosts.Add(owner.Clone());
        }

        #endregion
        //=========================================================================================================

        #region Guest Request Function 

        //פונקציות לדרישת לקוח לפי מפתח
        private bool existGuestRequest(int Key)
        {
            return DataSource.GuestRequests.Any(gr => gr.GuestRequestKey == Key);
        }
        public GuestRequest GetGuestRequest(int Key)
        {
            GuestRequest request = DataSource.GuestRequests.FirstOrDefault(gr => gr.GuestRequestKey == Key);
            if (request == null)
                throw new MissingKeyException(layer, "The requirement not exists", Key);
            return request.Clone();
        }
        //---------------------------------------------------------------------------------------------------------
        //פונקציות לדרישת לקוח - הוספה מחיקה ועדכון
        //הוספה
        public void AddGuestRequest(GuestRequest request)
        {
            if (existGuestRequest(request.GuestRequestKey))
                throw new DuplicateKeyException(layer, "The requirement already exists", request.GuestRequestKey);
            request.GuestRequestKey = Configuration.GuestRequestKey;
            DataSource.GuestRequests.Add(request.Clone());
        }
        //עדכון
        public void UpdateGuestRequest(GuestRequest request)
        {
            int count = DataSource.GuestRequests.RemoveAll(gr => gr.GuestRequestKey == request.GuestRequestKey);
            if (count == 0)
                throw new MissingKeyException(layer, "The requirement not exists", request.GuestRequestKey);
            DataSource.GuestRequests.Add(request.Clone());
        }
        //מחיקה
        //public void DelGuestRequest(GuestRequest request)
        //{
        //    int count = DataSource.GuestRequests.RemoveAll(gr => gr.GuestRequestKey == request.GuestRequestKey);
        //    if (count == 0)
        //        throw new MissingKeyException(layer, "The requirement not exists", request.GuestRequestKey);
        //}

        #endregion        
        //=========================================================================================================

        #region Order Function 

        //פונקציות להזמנה לפי מפתח
        private bool existOrder(int Key)
        {
            return DataSource.Orders.Any(or => or.OrderKey == Key);
        }
        public Order GetOrder(int Key)
        {
            Order order = DataSource.Orders.FirstOrDefault(or => or.OrderKey == Key);
            if (order == null)
                throw new MissingKeyException(layer, "The order not exists", Key);
            return order.Clone();
        }
        //---------------------------------------------------------------------------------------------------------
        //פונקציות להזמנה - הוספה מחיקה ועדכון
        //הוספה
        public int AddOrder(Order order)
        {
            if (existOrder(order.OrderKey))
                throw new MissingKeyException(layer, "The order already exists", order.OrderKey);
            order.OrderKey = Configuration.OrderKey;
            DataSource.Orders.Add(order.Clone());
            return order.OrderKey;
        }
        //עדכון סטטוס
        public void UpdateOrderStatus(Order order)
        {
            int count = DataSource.Orders.RemoveAll(or => or.OrderKey == order.OrderKey);
            if (count == 0)
                throw new MissingKeyException(layer, "The order not exists", order.OrderKey);
            DataSource.Orders.Add(order.Clone());
        }
        #endregion
        //=========================================================================================================

        #region Hosting Unit Function
        //פונקציות ליחידה לפי מפתח
        private bool existHostingUnit(int Key)
        {
            return DataSource.HostingUnits.Any(hu => hu.HostingUnitKey == Key);
        }
        public HostingUnit GetHostingUnit(int Key)
        {
            HostingUnit unit = DataSource.HostingUnits.FirstOrDefault(hu => hu.HostingUnitKey == Key);
            if (unit == null)
                throw new MissingKeyException(layer, "The unit not exists", Key);
            return unit.Clone();
        }
        //---------------------------------------------------------------------------------------------------------
        //פונקציות ליחידה - הוספה מחיקה ועדכון
        //הוספה
        public int AddUnit(HostingUnit unit)
        {
            if (existHostingUnit(unit.HostingUnitKey))
                throw new DuplicateKeyException(layer, "The unit already exists", unit.HostingUnitKey);
            unit.HostingUnitKey = Configuration.HostingUnitKey;
            DataSource.HostingUnits.Add(unit.Clone());
            return unit.HostingUnitKey;

        }
        //עדכון
        public void UpdateUnit(HostingUnit unit)
        {
            int count = DataSource.HostingUnits.RemoveAll(hu => hu.HostingUnitKey == unit.HostingUnitKey);
            if (count == 0)
                throw new MissingKeyException(layer, "The unit not exists", unit.HostingUnitKey);
            DataSource.HostingUnits.Add(unit.Clone());
        }
        //מחיקה
        public void DelUnit(HostingUnit unit)
        {
            int count = DataSource.HostingUnits.RemoveAll(hu => hu.HostingUnitKey == unit.HostingUnitKey);
            if (count == 0)
                throw new MissingKeyException(layer, "The unit not exists", unit.HostingUnitKey);
        }
        #endregion
        //=========================================================================================================

        #region Return the lists Function
        //פונקציות להחזרת עותקים של הרשימות
        //החזרת כל המארחים
        public IEnumerable<Host> GetAllHosts()
        {
            IEnumerable<Host> copyHosts = new List<Host>();
            IEnumerable<Host> hosts = DataSource.hosts;
            copyHosts =
                (from host in hosts
                 orderby host.HostKey
                 select host.Clone()).ToList();
            return copyHosts;
        }
        //החזרת רשימת הדרישות
        public IEnumerable<GuestRequest> GetAllGuestRequests()
        {
            IEnumerable<GuestRequest> copyGuestRequests = new List<GuestRequest>();
            IEnumerable<GuestRequest> guestRequests = DataSource.GuestRequests;
            copyGuestRequests =
                (from gr in guestRequests
                 orderby gr.GuestRequestKey
                 select gr.Clone()).ToList();
            return copyGuestRequests;
        }
        //החזרת רשימת ההזמנות
        public IEnumerable<Order> GetAllOrders()
        {
            IEnumerable<Order> copyOrders = new List<Order>();
            IEnumerable<Order> orders = DataSource.Orders;
            copyOrders =
                (from order in orders
                 orderby order.OrderKey
                 select order.Clone()).ToList();
            return copyOrders;
        }
        //החזרת רשימת היחידות 
        public IEnumerable<HostingUnit> GetAllUnits()
        {
            IEnumerable<HostingUnit> copyUnits = new List<HostingUnit>();
            IEnumerable<HostingUnit> units = DataSource.HostingUnits;
            copyUnits =
                (from unit in units
                 orderby unit.HostingUnitKey
                 select unit.Clone()).ToList();
            return copyUnits;
        }
        //החזרת רשימת הבנקים
        public IEnumerable<BankBranch> GetAllBankBranchs()
        {
            List<BankBranch> bankBranches = new List<BankBranch>();
            BankBranch bank1 = new BankBranch()
            {
                BankNumber = 1111,
                BankName = "discount",
                BranchNumber = 41,
                BranchAddress = "Kanfey Nesharim",
                BranchCity = "Jerusalem"
            };
            BankBranch bank2 = new BankBranch()
            {
                BankNumber = 1111,
                BankName = "discount",
                BranchNumber = 42,
                BranchAddress = "Malchey Israel",
                BranchCity = "Jerusalem"
            };
            BankBranch bank3 = new BankBranch()
            {
                BankNumber = 1111,
                BankName = "discount",
                BranchNumber = 43,
                BranchAddress = "Kiryat Moshe",
                BranchCity = "Jerusalem"
            };
            BankBranch bank4 = new BankBranch()
            {
                BankNumber = 1111,
                BankName = "discount",
                BranchNumber = 44,
                BranchAddress = "Beit Hakerem",
                BranchCity = "Jerusalem"
            };
            BankBranch bank5 = new BankBranch()
            {
                BankNumber = 1111,
                BankName = "discount",
                BranchNumber = 45,
                BranchAddress = "Ramot Eshkol",
                BranchCity = "Jerusalem"
            };
            bankBranches.Add(bank1);
            bankBranches.Add(bank2);
            bankBranches.Add(bank3);
            bankBranches.Add(bank4);
            bankBranches.Add(bank5);
            return bankBranches;
        }

        public void AddOrderList()
        {
            throw new NotImplementedException();
        }

        public void AddHostingUnitList()
        {
            throw new NotImplementedException();
        }

        public void AddHostList()
        {
            throw new NotImplementedException();
        }

        public void AddGuestRequestList()
        {
            throw new NotImplementedException();
        }

        public void DownloadBankXml()
        {
            throw new NotImplementedException();
        }
        #endregion
        //=========================================================================================================

    }
}
