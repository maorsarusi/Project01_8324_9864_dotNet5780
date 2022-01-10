using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using BE;
using DS;
using System.Net;
using System.Threading;

namespace DAL
{
    class Dal_XML_imp : IDAL
    {
        //=========================================================================================================

        #region start

        public static Layers layer = Layers.LayerDal;

        //private XElement orderRoot;
        private XElement hostRoot;
        private XElement guestRequestRoot;
        private XElement configRoot;
        private XElement bankBranchRoot;

        public static List<BankBranch> BankBranchs;
        public static List<Order> Orders;
        public static List<HostingUnit> HostingUnits;

        private static string configPath = @"Config.xml";
        private static string bankBranchPath = @"Banks.xml";
        private static string guestRequestsPath = @"GuestRequestsXml.xml";
        private static string hostingUnitsPath = @"HostingUnitsXml.xml";
        private static string ordersPath = @"OrdersXml.xml";
        private static string hostsPath = @"HostsXml.xml";

        internal Dal_XML_imp()
        {
            if (!File.Exists(configPath))
            {
                SaveConfigToXml();
            }
            else
            {
                configRoot = XElement.Load(configPath);
                Configuration.GuestRequestKey = int.Parse(configRoot.Element("guestRequestKey").Value);
                Configuration.HostKey = int.Parse(configRoot.Element("hostKey").Value);
                Configuration.HostingUnitKey = int.Parse(configRoot.Element("hostingUnitKey").Value);
                Configuration.OrderKey = int.Parse(configRoot.Element("orderKey").Value);
                Configuration.FeePerDay = int.Parse(configRoot.Element("feePerDay").Value);
            }

            if (!File.Exists(guestRequestsPath))
                CreateFileGuestRequests();
            loadGuestRequest();

            if (!File.Exists(hostsPath))
                CreateFilehosts();
            loadHosts();

            if (!File.Exists(ordersPath))
                SaveToXML(new List<Order>(), ordersPath);
            Orders = LoadFromXML<List<Order>>(ordersPath);

            if (!File.Exists(hostingUnitsPath))
                SaveToXML(new List<HostingUnit>()/*DataSource.HostingUnits*/, hostingUnitsPath);
            HostingUnits = LoadFromXML<List<HostingUnit>>(hostingUnitsPath);

            if (File.Exists(bankBranchPath))
            {
                bankBranchRoot = XElement.Load(bankBranchPath);
                BankBranchs = XmlToBankBranchsList(bankBranchRoot);
                //SaveToXML(BankBranchs, "Banks.xml");
            }
        }

        private void SaveConfigToXml()
        {
            try
            {
                configRoot = new XElement("config");
                configRoot.Add(
                    new XElement("guestRequestKey", Configuration.GuestRequestKey),
                    new XElement("hostKey", Configuration.HostKey),
                    new XElement("hostingUnitKey", Configuration.HostingUnitKey),
                    new XElement("orderKey", Configuration.OrderKey),
                    new XElement("feePerDay", Configuration.FeePerDay));
                configRoot.Save(configPath);
            }
            catch (Exception)
            {

            }
        }

        //~Dal_XML_imp()
        //{
        //    SaveConfigToXml();
        //}

        #endregion

        //=========================================================================================================

        #region Serialize

        public static void SaveToXML<T>(T source, string path)
        {
            FileStream file = new FileStream(path, FileMode.Create);
            XmlSerializer xmlSer = new XmlSerializer(source.GetType());
            xmlSer.Serialize(file, source);
            file.Close();
        }

        public static T LoadFromXML<T>(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            XmlSerializer xmlSer = new XmlSerializer(typeof(T));
            T result = (T)xmlSer.Deserialize(file);
            file.Close();
            return result;
        }
        #endregion

        //=========================================================================================================

        #region BankBranchs 

        public void DownloadBankXml()
        {
            WebClient wc = new WebClient();
            try
            {
                string xmlServerPath =
               @"http://www.boi.org.il/he/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/atm.xml";
                wc.DownloadFile(xmlServerPath, bankBranchPath);
            }
            catch (Exception)
            {
                string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
                wc.DownloadFile(xmlServerPath, bankBranchPath);
            }
            finally
            {
                wc.Dispose();
            }
        }

        public List<BankBranch> XmlToBankBranchsList(XElement bankBranchsRoot)
        {

            try
            {
                List<BankBranch> list = new List<BankBranch>();
                list = (from bankBranch in bankBranchsRoot.Elements()
                        select new BankBranch()
                        {
                            BankNumber = Int32.Parse(bankBranch.Element("BankNumber").Value),
                            BankName = bankBranch.Element("BankName").Value,
                            BranchNumber = Int32.Parse(bankBranch.Element("BranchNumber").Value),
                            BranchAddress = bankBranch.Element("BranchAddress").Value,
                            BranchCity = bankBranch.Element("BranchCity").Value
                        }
                        ).Distinct().ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<BankBranch> GetAllBankBranchs()
        {
            var copyBankBranchs = (from branch in BankBranchs
                                   select branch.Clone()).ToList();
            return copyBankBranchs;
        }

        #endregion

        //=========================================================================================================

        #region GuestRequest          

        //public void GuestRequestsManagement()
        //{
        //    if (!File.Exists(guestRequestsPath))
        //        CreateFileGuestRequestRoot();
        //    loadGuestRequest();
        //}

        private void loadGuestRequest()
        {
            guestRequestRoot = XElement.Load(guestRequestsPath);
        }

        private void CreateFileGuestRequests()
        {
            guestRequestRoot = new XElement("guestRequestRoot");
            guestRequestRoot.Save(guestRequestsPath);
        }

        private bool isExistGuestRequest(int key)
        {
            XElement element = (from gr in guestRequestRoot.Elements()
                                where int.Parse(gr.Element("guestRequestKey").Value) == key
                                select gr).FirstOrDefault();
            return element != null;
        }

        public GuestRequest GetGuestRequest(int key)
        {
            XElement tempElement = (from gr in guestRequestRoot.Elements()
                                    where int.Parse(gr.Element("guestRequestKey").Value) == key
                                    select gr).FirstOrDefault();
            if (tempElement == null)
                throw new MissingKeyException(layer, "The guestRequest not exists", key);
            GuestRequest request = new GuestRequest()
            {

                GuestRequestKey = Int32.Parse(tempElement.Element("guestRequestKey").Value),
                PrivateName = tempElement.Element("privateName").Value,
                FamilyName = tempElement.Element("familyName").Value,
                MailAddress = tempElement.Element("mailAddress").Value,
                StatusGuestRequest = (GuestRequestStatus)Enum.Parse(typeof(GuestRequestStatus), tempElement.Element("statusGuestRequest").Value),
                RegistrationDate = DateTime.Parse(tempElement.Element("registrationDate").Value),
                EntryDate = DateTime.Parse(tempElement.Element("entryDate").Value),
                ReleaseDate = DateTime.Parse(tempElement.Element("releaseDate").Value),
                Area = (Areas)Enum.Parse(typeof(Areas), tempElement.Element("area").Value),
                SubArea = tempElement.Element("subArea").Value,
                Type = (ResortType)Enum.Parse(typeof(ResortType), tempElement.Element("type").Value),
                Adults = Int32.Parse(tempElement.Element("adults").Value),
                Children = Int32.Parse(tempElement.Element("children").Value),
                Pool = (Options)Enum.Parse(typeof(Options), tempElement.Element("pool").Value),
                Jacuzzi = (Options)Enum.Parse(typeof(Options), tempElement.Element("jacuzzi").Value),
                Garden = (Options)Enum.Parse(typeof(Options), tempElement.Element("garden").Value),
                ChildrensAttractions = (Options)Enum.Parse(typeof(Options), tempElement.Element("childrensAttractions").Value),
            };
            return request;
        }

        public void AddGuestRequest(GuestRequest guestRequest)
        {
            guestRequest.GuestRequestKey = Configuration.GuestRequestKey++;
            SaveConfigToXml();
            if (isExistGuestRequest(guestRequest.GuestRequestKey))
                throw new DuplicateKeyException(layer, "The requirement already exists", guestRequest.GuestRequestKey);
            guestRequestRoot.Add(new XElement("guestRequest",
                                      new XElement("guestRequestKey", guestRequest.GuestRequestKey),
                                      new XElement("privateName", guestRequest.PrivateName),
                                      new XElement("familyName", guestRequest.FamilyName),
                                      new XElement("mailAddress", guestRequest.MailAddress),
                                      new XElement("statusGuestRequest", guestRequest.StatusGuestRequest),
                                      new XElement("registrationDate", guestRequest.RegistrationDate),
                                      new XElement("entryDate", guestRequest.EntryDate),
                                      new XElement("releaseDate", guestRequest.ReleaseDate),
                                      new XElement("area", guestRequest.Area),
                                      new XElement("subArea", guestRequest.SubArea),
                                      new XElement("type", guestRequest.Type),
                                      new XElement("adults", guestRequest.Adults),
                                      new XElement("children", guestRequest.Children),
                                      new XElement("pool", guestRequest.Pool),
                                      new XElement("jacuzzi", guestRequest.Jacuzzi),
                                      new XElement("garden", guestRequest.Garden),
                                      new XElement("childrensAttractions", guestRequest.ChildrensAttractions)
                                     )
                        );
            guestRequestRoot.Save(guestRequestsPath);
        }

        public void UpdateGuestRequest(GuestRequest guestRequest)
        {
            XElement tempElement = (from gr in guestRequestRoot.Elements()
                                    where int.Parse(gr.Element("guestRequestKey").Value) == guestRequest.GuestRequestKey
                                    select gr).FirstOrDefault();
            if (tempElement == null)
                throw new MissingKeyException(layer, "The requirement not exists", guestRequest.GuestRequestKey);
            tempElement.Element("guestRequestKey").Value = guestRequest.GuestRequestKey.ToString();
            tempElement.Element("privateName").Value = guestRequest.PrivateName;
            tempElement.Element("familyName").Value = guestRequest.FamilyName;
            tempElement.Element("mailAddress").Value = guestRequest.MailAddress;
            tempElement.Element("statusGuestRequest").Value = guestRequest.StatusGuestRequest.ToString();
            tempElement.Element("registrationDate").Value = guestRequest.RegistrationDate.ToString();
            tempElement.Element("entryDate").Value = guestRequest.EntryDate.ToString();
            tempElement.Element("releaseDate").Value = guestRequest.ReleaseDate.ToString();
            tempElement.Element("area").Value = guestRequest.Area.ToString();
            tempElement.Element("subArea").Value = guestRequest.SubArea.ToString();
            tempElement.Element("type").Value = guestRequest.Type.ToString();
            tempElement.Element("adults").Value = guestRequest.Adults.ToString();
            tempElement.Element("children").Value = guestRequest.Children.ToString();
            tempElement.Element("pool").Value = guestRequest.Pool.ToString();
            tempElement.Element("jacuzzi").Value = guestRequest.Jacuzzi.ToString();
            tempElement.Element("garden").Value = guestRequest.Garden.ToString();
            tempElement.Element("childrensAttractions").Value = guestRequest.ChildrensAttractions.ToString();

            guestRequestRoot.Save(guestRequestsPath);
        }
        //public void DelGuestRequest(GuestRequest guestRequest)
        //{
        //    XElement tempElement = (from gr in guestRequests.Elements()
        //                            where int.Parse(gr.Element("guestRequestKey").Value) == guestRequest.GuestRequestKey
        //                            select gr).FirstOrDefault();
        //    if (tempElement == null)
        //        throw new MissingKeyException(layer, "The requirement not exists", guestRequest.GuestRequestKey);
        //    tempElement.Remove();
        //    guestRequests.Save(guestRequestsPath);
        //}

        public IEnumerable<GuestRequest> GetAllGuestRequests()
        {
            loadGuestRequest();
            IEnumerable<GuestRequest> list = new List<GuestRequest>();
            list = (from gr in guestRequestRoot.Elements()
                    select new GuestRequest()
                    {
                        GuestRequestKey = Int32.Parse(gr.Element("guestRequestKey").Value),
                        PrivateName = gr.Element("privateName").Value,
                        FamilyName = gr.Element("familyName").Value,
                        MailAddress = gr.Element("mailAddress").Value,
                        StatusGuestRequest = (GuestRequestStatus)Enum.Parse(typeof(GuestRequestStatus), gr.Element("statusGuestRequest").Value),
                        RegistrationDate = DateTime.Parse(gr.Element("registrationDate").Value),
                        EntryDate = DateTime.Parse(gr.Element("entryDate").Value),
                        ReleaseDate = DateTime.Parse(gr.Element("releaseDate").Value),
                        Area = (Areas)Enum.Parse(typeof(Areas), gr.Element("area").Value),
                        SubArea = gr.Element("subArea").Value,
                        Type = (ResortType)Enum.Parse(typeof(ResortType), gr.Element("type").Value),
                        Adults = Int32.Parse(gr.Element("adults").Value),
                        Children = Int32.Parse(gr.Element("children").Value),
                        Pool = (Options)Enum.Parse(typeof(Options), gr.Element("pool").Value),
                        Jacuzzi = (Options)Enum.Parse(typeof(Options), gr.Element("jacuzzi").Value),
                        Garden = (Options)Enum.Parse(typeof(Options), gr.Element("garden").Value),
                        ChildrensAttractions = (Options)Enum.Parse(typeof(Options), gr.Element("childrensAttractions").Value),
                    }
                   ).ToList();
            return list;
        }

        public void AddGuestRequestList()
        {
            foreach (GuestRequest gr in DataSource.GuestRequests)
            {
                AddGuestRequest(gr);
            }
        }
        #endregion

        //=========================================================================================================

        #region Host

        //public void HostsManagement()
        //{
        //    if (!File.Exists(hostsPath))
        //        CreateFilehosts();
        //    loadHosts();
        //}

        private void loadHosts()
        {
            hostRoot = XElement.Load(hostsPath);
        }

        private void CreateFilehosts()
        {
            hostRoot = new XElement("hosts");
            hostRoot.Save(hostsPath);
        }
        private bool isExistHost(int key)
        {
            XElement element = (from h in hostRoot.Elements()
                                where int.Parse(h.Element("hostKey").Value) == key
                                select h).FirstOrDefault();

            return element != null;
        }

        public Host GetHostByKey(int Key)
        {
            XElement tempElement = (from h in hostRoot.Elements()
                                    where int.Parse(h.Element("hostKey").Value) == Key
                                    select h).FirstOrDefault();
            if (tempElement == null)
                throw new MissingKeyException(layer, "The order not exists", Key);
            Host owner = new Host()
            {
                HostKey = Int32.Parse(tempElement.Element("hostKey").Value),
                PrivateName = tempElement.Element("privateName").Value,
                FamilyName = tempElement.Element("familyName").Value,
                WholeName = tempElement.Element("wholeName").Value,
                PhoneNumber = tempElement.Element("phoneNumber").Value,
                MailAddress = tempElement.Element("mailAddress").Value,
                BankBranchDetails = new BankBranch()
                {
                    BankNumber = Int32.Parse(tempElement.Element("bankBranchDetails").Element("bankNumber").Value),
                    BankName = tempElement.Element("bankBranchDetails").Element("bankName").Value,
                    BranchNumber = Int32.Parse(tempElement.Element("bankBranchDetails").Element("branchNumber").Value),
                    BranchAddress = tempElement.Element("bankBranchDetails").Element("branchAddress").Value,
                    BranchCity = tempElement.Element("bankBranchDetails").Element("branchCity").Value
                },
                BankAccountNumber = Int32.Parse(tempElement.Element("bankAccountNumber").Value),
                CollectionClearance = bool.Parse(tempElement.Element("collectionClearance").Value),
            };
            return owner;
        }

        public void AddHost(Host host)
        {
            host.HostKey = Configuration.HostKey++;
            SaveConfigToXml();
            if (isExistHost(host.HostKey))
                throw new DuplicateKeyException(layer, "The Host already exists", host.HostKey);
            hostRoot.Add(new XElement("host",
                                      new XElement("hostKey", host.HostKey),
                                      new XElement("privateName", host.PrivateName),
                                      new XElement("familyName", host.FamilyName),
                                      new XElement("wholeName" , host.WholeName),
                                      new XElement("phoneNumber", host.PhoneNumber),
                                      new XElement("mailAddress", host.MailAddress),
                                      new XElement("bankBranchDetails",
                                                   new XElement("bankNumber", host.BankBranchDetails.BankNumber),
                                                   new XElement("bankName", host.BankBranchDetails.BankName),
                                                   new XElement("branchNumber", host.BankBranchDetails.BranchNumber),
                                                   new XElement("branchAddress", host.BankBranchDetails.BranchAddress),
                                                   new XElement("branchCity", host.BankBranchDetails.BranchCity)
                                                   ),
                                      new XElement("bankAccountNumber", host.BankAccountNumber),
                                      new XElement("collectionClearance", host.CollectionClearance)

                                      )
                        );
            hostRoot.Save(hostsPath);
        }

        //public void UpdateHost(Host host)
        //{
        //    XElement tempElement = (from h in hosts.Elements()
        //                            where int.Parse(h.Element("hostKey").Value) == host.HostKey
        //                            select h).FirstOrDefault();
        //    if (tempElement == null)
        //        throw new MissingKeyException(layer, "The host not exists", host.HostKey);
        //    tempElement.Element("hostKey").Value = host.HostKey.ToString();
        //    tempElement.Element("privateName").Value = host.PrivateName;
        //    tempElement.Element("familyName").Value = host.FamilyName;
        //    tempElement.Element("phoneNumber").Value = host.PhoneNumber;
        //    tempElement.Element("mailAddress").Value = host.MailAddress;
        //    tempElement.Element("bankBranchDetails").Element("bankNumber").Value = host.BankBranchDetails.BankNumber.ToString();
        //    tempElement.Element("bankBranchDetails").Element("bankName").Value = host.BankBranchDetails.BankName;
        //    tempElement.Element("bankBranchDetails").Element("branchNumber").Value = host.BankBranchDetails.BranchNumber.ToString();
        //    tempElement.Element("bankBranchDetails").Element("branchAddress").Value = host.BankBranchDetails.BranchAddress;
        //    tempElement.Element("bankBranchDetails").Element("branchCity").Value = host.BankBranchDetails.BranchCity;
        //    tempElement.Element("bankAccountNumber").Value = host.BankAccountNumber.ToString();
        //    tempElement.Element("collectionClearance").Value = host.CollectionClearance.ToString();
        //
        //    hosts.Save(hostsPath);
        //}
        //public void DelHost (Host host)
        //{
        //    XElement tempElement = (from h in hosts.Elements()
        //                            where int.Parse(h.Element("hostKey").Value) == host.HostKey
        //                            select h).FirstOrDefault();
        //    if (tempElement == null)
        //        throw new MissingKeyException(layer, "The host not exists", host.HostKey);
        //    tempElement.Remove();
        //    hosts.Save(hostsPath);
        //}

        public IEnumerable<Host> GetAllHosts()
        {
            loadHosts();
            IEnumerable<Host> list = new List<Host>();
            list = (from h in hostRoot.Elements()
                    select new Host()
                    {
                        HostKey = Int32.Parse(h.Element("hostKey").Value),
                        PrivateName = h.Element("privateName").Value,
                        FamilyName = h.Element("familyName").Value,
                        WholeName = h.Element("wholeName").Value,
                        PhoneNumber = h.Element("phoneNumber").Value,
                        MailAddress = h.Element("mailAddress").Value,
                        BankBranchDetails = new BankBranch()
                        {
                            BankNumber = Int32.Parse(h.Element("bankBranchDetails").Element("bankNumber").Value),
                            BankName = h.Element("bankBranchDetails").Element("bankName").Value,
                            BranchNumber = Int32.Parse(h.Element("bankBranchDetails").Element("branchNumber").Value),
                            BranchAddress = h.Element("bankBranchDetails").Element("branchAddress").Value,
                            BranchCity = h.Element("bankBranchDetails").Element("branchCity").Value
                        },
                        BankAccountNumber = Int32.Parse(h.Element("bankAccountNumber").Value),
                        CollectionClearance = bool.Parse(h.Element("collectionClearance").Value),
                    }
                   ).ToList();
            return list;
        }

        public void AddHostList()
        {
            int i = 0;
            List<BankBranch> AllBranchs = GetAllBankBranchs().ToList();
            foreach (Host h in DataSource.hosts)
            {
                h.BankBranchDetails = AllBranchs[i];
                AddHost(h);
                i = i + 20;//חלוקה אקראית של סניפי בנק אמיתיים לבעלי יחידות האירוח
            }
        }

        #endregion

        //=========================================================================================================

        #region HostingUnit 

        //פונקציות ליחידה לפי מפתח
        private bool existHostingUnit(int Key)
        {
            return HostingUnits.Any(hu => hu.HostingUnitKey == Key);
        }
        public HostingUnit GetHostingUnit(int Key)
        {
            HostingUnit unit = HostingUnits.FirstOrDefault(hu => hu.HostingUnitKey == Key);
            if (unit == null)
                throw new MissingKeyException(layer, "The unit not exists", Key);
            return unit.Clone();
        }
        //---------------------------------------------------------------------------------------------------------
        //פונקציות ליחידה - הוספה מחיקה ועדכון
        //הוספה
        public int AddUnit(HostingUnit unit)
        {
            unit.HostingUnitKey = Configuration.HostingUnitKey++;
            SaveConfigToXml();
            if (existHostingUnit(unit.HostingUnitKey))
                throw new DuplicateKeyException(layer, "The unit already exists", unit.HostingUnitKey);
            HostingUnits.Add(unit);
            SaveToXML<List<HostingUnit>>(HostingUnits, hostingUnitsPath);
            return unit.HostingUnitKey;

        }
        //עדכון
        public void UpdateUnit(HostingUnit unit)
        {
            int count = HostingUnits.RemoveAll(hu => hu.HostingUnitKey == unit.HostingUnitKey);
            if (count == 0)
                throw new MissingKeyException(layer, "The unit not exists", unit.HostingUnitKey);
            HostingUnits.Add(unit.Clone());
            SaveToXML<List<HostingUnit>>(HostingUnits, hostingUnitsPath);
        }
        //מחיקה
        public void DelUnit(HostingUnit unit)
        {
            int count = HostingUnits.RemoveAll(hu => hu.HostingUnitKey == unit.HostingUnitKey);
            if (count == 0)
                throw new MissingKeyException(layer, "The unit not exists", unit.HostingUnitKey);
            SaveToXML<List<HostingUnit>>(HostingUnits, hostingUnitsPath);
        }
        //החזרת רשימת היחידות
        public IEnumerable<HostingUnit> GetAllUnits()
        {
            IEnumerable<HostingUnit> copyUnits = new List<HostingUnit>();
            IEnumerable<HostingUnit> units = HostingUnits;
            copyUnits =
                (from unit in HostingUnits
                 orderby unit.HostingUnitKey
                 select unit.Clone()).ToList();
            return copyUnits;
            //HostingUnits = LoadFromXML<List<HostingUnit>>(hostingUnitsPath);
            //return HostingUnits;
        }

        public void AddHostingUnitList()
        {
            int i = 0;
            List<Host> AllHosts = GetAllHosts().ToList();
            foreach (HostingUnit hu in DataSource.HostingUnits)
            {
                hu.Owner = AllHosts[i];
                AddUnit(hu);
                i++;
            }
        }
        #endregion

        //=========================================================================================================

        #region Orders

        //פונקציות להזמנה לפי מפתח
        private bool existOrder(int Key)
        {
            return Orders.Any(or => or.OrderKey == Key);
        }
        public Order GetOrder(int Key)
        {
            Order order = Orders.FirstOrDefault(or => or.OrderKey == Key);
            if (order == null)
                throw new MissingKeyException(layer, "The order not exists", Key);
            return order.Clone();
        }
        //---------------------------------------------------------------------------------------------------------
        //פונקציות להזמנה - הוספה מחיקה ועדכון
        //הוספה
        public int AddOrder(Order order)
        {
            order.OrderKey = Configuration.OrderKey++;
            SaveConfigToXml();
            if (existOrder(order.OrderKey))
                throw new MissingKeyException(layer, "The order already exists", order.OrderKey);
            Orders.Add(order.Clone());
            SaveToXML<List<Order>>(Orders, ordersPath);
            return order.OrderKey;
        }
        //עדכון סטטוס
        public void UpdateOrderStatus(Order order)
        {
            int count = Orders.RemoveAll(or => or.OrderKey == order.OrderKey);
            if (count == 0)
                throw new MissingKeyException(layer, "The order not exists", order.OrderKey);
            Orders.Add(order.Clone());
            SaveToXML<List<Order>>(Orders, ordersPath);
        }

        //החזרת רשימת ההזמנות
        public IEnumerable<Order> GetAllOrders()
        {
            IEnumerable<Order> copyOrders = new List<Order>();
            IEnumerable<Order> orders = Orders;
            copyOrders =
                (from order in orders
                 orderby order.OrderKey
                 select order.Clone()).ToList();
            return copyOrders;
        }

        public void AddOrderList()
        {
            foreach (Order or in DataSource.Orders)
            {
                AddOrder(or);
            }
        }
        #endregion

        //=========================================================================================================        
    }
}