using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using System.Net.Mail;
using System.Threading;

namespace BL
{
    public class BL_imp : IBL
    {
        IDAL dal = FactoryDal.Instance;

        public static Layers layer = Layers.LayerBl;

        public object MessageBox { get; private set; }

        //=========================================================================================================

        #region Dates Function   
        //שלוש העמסות לפונקציה של דוריישן
        public int Duration(GuestRequest gr)
        {
            return (gr.ReleaseDate - gr.EntryDate).Days + 1;
        }
        public int Duration(DateTime time1, DateTime time2)
        {
            return (time2 - time1).Days + 1;
        }
        public int Duration(DateTime time)
        {
            return (DateTime.Today - time).Days + 1;
        }
        //=========================================================================================================
        //פונקציות פרייבט
        //שתי העמסות לפונקצייה שבודקת האם התאריך המבוקש פנוי במטריצה
        //העמסה ראשונה
        private bool matchingDates(HostingUnit hu, DateTime time, int duration)
        {
            updateMatrix(hu);
            DateTime temp = time;//צריך לבדוק שמתבצעת פה העתקה עמוקה כדי שלא ישנה פה את השדה בדרישת לקוח
            for (int i = 0; i < duration; i++)
            {
                if (hu[temp] == true) return false;
                temp.AddDays(1);
            }
            return true;
        }
        //העמסה שנייה
        private bool matchingDates(HostingUnit hu, GuestRequest gr)
        {
            updateMatrix(hu);
            DateTime temp = gr.EntryDate;//צריך לבדוק שמתבצעת פה העתקה עמוקה כדי שלא ישנה פה את השדה בדרישת לקוח
            for (int i = 0; i < Duration(gr); i++)
            {
                if (hu[temp] == true) return false;
                temp.AddDays(1);
            }
            return true;
        }
        //---------------------------------------------------------------------------------------------------------
        //שינוי הערכים במטריצה לאמת
        private void changeToTrue(HostingUnit hu, GuestRequest gr)
        {
            DateTime temp = gr.EntryDate;
            for (int i = 0; i < Duration(gr); i++)
            {
                hu[temp] = true;
                temp.AddDays(1);
            }
        }
        //---------------------------------------------------------------------------------------------------------
        //להלן פונקציות שתפקידן שהמטריצה תהיה מעודכנת 11 חודשים קדימה ותמחק חופשות שכבר עבר זמנן
        //שינוי הערכים שבין התאריכים המתקבלים לשקר ביחידת האירוח ספיציפית
        private void changeTofalse(DateTime time1, DateTime time2, HostingUnit hu)
        {
            DateTime date = time1;
            while (date < time2)
            {
                hu[date] = false;
                date =  time1.AddDays(1.0);
                time1 = date;
            }
        }
        //עדכון המטריצה של יחידת אירוח ספיציפית ל11 חודשים קדימה
        private void updateMatrix(HostingUnit hu)
        {
            DateTime now = DateTime.Today;
            DateTime startDate = hu.LastApdate;
            int diffrence = diffrenceOfMonths(startDate, now);
            if (diffrence > 0)
            {
                if (diffrence > 12)
                    diffrence = 12;
                DateTime time1 = new DateTime(startDate.Year, startDate.Month, 1, 0, 0, 0);
                DateTime time2 = time1.AddMonths(diffrence);
                changeTofalse(time1, time2, hu);
                hu.LastApdate = now;
            }
        }
        //הפונקציה מחזירה את מספר החודשים שבין שני התאריכים שמתקבלים
        private int diffrenceOfMonths(DateTime time1, DateTime time2)
        {
            return 12 * (time2.Year - time1.Year) + (time2.Month - time1.Month);
        }
        //---------------------------------------------------------------------------------------------------------
        //פונקצית עזר המקבלת מארח מסוים ומחזירה את מספר היחידות שיש לו
        private int numOfUnitsOfHost(int hostKey)
        {
            int count = 0;
            IEnumerable<HostingUnit> tempUnits = dal.GetAllUnits();
            var UnitsForHost =
                    (from unit in tempUnits
                     where unit.Owner.HostKey == hostKey
                     select ++count).ToList();//שימוש בפונקציה מיידית כדי להריץ להפעיל את התהליך
            return count;
        }

        #endregion

        //=========================================================================================================

        #region Matching Function    

        private IEnumerable<Func<HostingUnit, GuestRequest, bool>> conditionsForMatchingBetweenUnitAndRequirement()
        {
            yield return matchingDates;

            //yield return matchingCapacity;
            yield return (hu, gr) => hu.Capacity >= gr.Adults + gr.Children;

            //yield return matchingArea;
            yield return (hu, gr) => gr.Area == Areas.All ||
            hu.Area == gr.Area && (hu.SubArea == gr.SubArea || hu.SubArea == null ||
            gr.SubArea == null || hu.SubArea == "" || gr.SubArea == "");

            //yield return matchingType;
            yield return (hu, gr) => hu.Type == gr.Type;

            //yield return matchingPool;
            yield return (hu, gr) => gr.Pool == Options.Possible ||
                gr.Pool == Options.Necessary && hu.Pool == true ||
                gr.Pool == Options.NotInterest && hu.Pool == false;

            //yield return matchingJacuzzi;
            yield return (hu, gr) => gr.Jacuzzi == Options.Possible ||
                gr.Jacuzzi == Options.Necessary && hu.Jacuzzi == true ||
                gr.Jacuzzi == Options.NotInterest && hu.Jacuzzi == false;

            //yield return matchingGarden;
            yield return (hu, gr) => gr.Garden == Options.Possible ||
                gr.Garden == Options.Necessary && hu.Garden == true ||
                gr.Garden == Options.NotInterest && hu.Garden == false;

            //yield return matchingChildrensAttractions;
            yield return (hu, gr) => gr.ChildrensAttractions == Options.Possible ||
                gr.ChildrensAttractions == Options.Necessary && hu.ChildrensAttractions == true ||
                gr.ChildrensAttractions == Options.NotInterest && hu.ChildrensAttractions == false;
        }

        private bool matchingBetweenUnitAndRequirement(HostingUnit hu, GuestRequest gr)
        {
            bool matching = true;
            foreach (var func in conditionsForMatchingBetweenUnitAndRequirement())
                if (func(hu, gr) == false)
                {
                    matching = false;
                    break;
                }
            return matching;
        }
       
        #endregion

        //=========================================================================================================

        #region Host Function    

        public Host GetHostByKey(int key)
        {
            try
            {
                return dal.GetHostByKey(key);
            }
            catch (MissingKeyException ex)
            {
                throw new MissingKeyException(ex.Layer, ex.Message, ex.Key);
            }
        }

        public void AddHost(Host owner)
        {
            try
            {
                dal.AddHost(owner);
            }
            catch (MissingKeyException ex)
            {
                throw new MissingKeyException(ex.Layer, ex.Message, ex.Key);
            }
        }

        //החזרת רשימת המארחים
        public IEnumerable<Host> GetAllHosts()
        {
            return dal.GetAllHosts();
        }

        #endregion

        //=========================================================================================================

        #region HostingUnit Function    

        //קבלת יחידת אירוח על פי מפתח
        public HostingUnit GetHostingUnit(int key)
        {
            try
            {
                return dal.GetHostingUnit(key);
            }
            catch (MissingKeyException ex)
            {
                throw new MissingKeyException(ex.Layer, ex.Message, ex.Key);
            }
        }

        //פונקציות ליחידה - הוספה מחיקה ועדכון
        //הוספה
        public int AddUnit(HostingUnit unit)
        {
            try
            {
                int key = dal.AddUnit(unit);
                return key;
            }
            catch (DuplicateKeyException ex)
            {
                throw new DuplicateKeyException(ex.Layer, ex.Message, ex.Key);
            }
        }

        //מחיקה
        public void DelUnit(HostingUnit hu)
        {
            try
            {
                IEnumerable<Order> tempOrder = dal.GetAllOrders();
                var v =
                    from order in tempOrder
                    where order.HostingUnitKey == hu.HostingUnitKey
                    where order.StatusOrder == OrderStatus.SendEmail || order.StatusOrder == OrderStatus.NotYetAdressed
                    select order;
                if (v.Count() > 0)
                {
                    throw new CannotChangeItemInOpenedOrderException
                        (layer, "A unit cannot be deleted when there is an open invitation associated with it");
                }
                dal.DelUnit(hu);
            }
            catch (MissingKeyException ex)
            {
                throw new MissingKeyException(ex.Layer, ex.Message, ex.Key);
            }
        }
        //עדכון
        public void UpdateUnit(HostingUnit hu)
        {
            int count = 0;
            if (hu.Owner.CollectionClearance == false && dal.GetHostingUnit(hu.HostingUnitKey).Owner.CollectionClearance == true)
            {
                IEnumerable<Order> tempOrders = dal.GetAllOrders();
                var v =
                    (from order in tempOrders
                     where order.HostingUnitKey == hu.HostingUnitKey
                     where order.StatusOrder == OrderStatus.SendEmail || order.StatusOrder == OrderStatus.NotYetAdressed
                     select ++count).ToList();
            }
            try
            {
                if (count > 0)
                    throw new CannotChangeItemInOpenedOrderException
                        (layer, "A unit cannot be deleted when there is an open invitation associated with it");
                dal.UpdateUnit(hu);
            }
            catch (MissingKeyException ex)
            {
                throw new MissingKeyException(ex.Layer, ex.Message, ex.Key);
            }
        }

        //החזרת רשימת יחידות האירוח
        public IEnumerable<HostingUnit> GetAllUnits()
        {
            return dal.GetAllUnits();
        }

        #endregion

        //=========================================================================================================

        #region GuestRequest Function    

        //קבלת דרישת לקוח על פי מפתח
        public GuestRequest GetGuestRequest(int key)
        {
            try
            {
                return dal.GetGuestRequest(key);
            }
            catch (MissingKeyException ex)
            {
                throw new MissingKeyException(ex.Layer, ex.Message, ex.Key);
            }
        }

        //פונקציות לדרישת לקוח - הוספה מחיקה ועדכון
        //הוספה
        public void AddGuestRequest(GuestRequest gr)
        {
            try
            {
                if (Duration(gr) < 1)
                    throw new InvalidDurationException
                        (layer, "Duration is invalid", Duration(gr));
                else if ((gr.EntryDate - DateTime.Today).Days < 1)
                    throw new InvalidDurationException
                        (layer, "EntryDate is invalid", (gr.EntryDate - DateTime.Today).Days);
                else
                    dal.AddGuestRequest(gr);
            }
            catch (MissingKeyException ex)
            {
                throw new MissingKeyException(ex.Layer, ex.Message, ex.Key);
            }
        }
        
        //עדכון סטטוס
        public void UpdateGuestRequest(GuestRequest gr, GuestRequestStatus update)
        {
            try
            {
                gr.StatusGuestRequest = update;
                dal.UpdateGuestRequest(gr);
            }
            catch (MissingKeyException ex)
            {
                throw new MissingKeyException(ex.Layer, ex.Message, ex.Key);
            }
        }

        //החזרת רשימת הדרישות
        public IEnumerable<GuestRequest> GetAllGuestRequests()
        {
            return dal.GetAllGuestRequests();
        }

        #endregion

        //=========================================================================================================

        #region Order Function    

        //פונקציות להזמנה - הוספה מחיקה ועדכון

        //קבלת הזמנה על פי מפתח
        public Order GetOrder(int key)
        {
            try
            {
                return dal.GetOrder(key);
            }
            catch (MissingKeyException ex)
            {
                throw new MissingKeyException(ex.Layer, ex.Message, ex.Key);
            }
        }

        //הוספה
        public int AddOrder(Order order)
        {
            try
            {
                HostingUnit tempUnit = GetHostingUnit(order.HostingUnitKey);
                GuestRequest tempGuestRequest = GetGuestRequest(order.GuestRequestKey);
                if (matchingBetweenUnitAndRequirement(tempUnit, tempGuestRequest) == false)
                    throw new DiscrepancyException(layer, "There is a discrepancy between the customer requirement and the unit");
                int key = dal.AddOrder(order);
                return key;
            }
            catch (DuplicateKeyException ex)
            {
                throw new DuplicateKeyException(ex.Layer, ex.Message, ex.Key);
            }
        }
        //עדכון סטטוס
        public void UpdateOrderStatus(Order or, OrderStatus update)
        {
            try
            {
                //בדיקה אם הסטטוס לא נמצא במצב של נסגרה עסקה
                if (or.StatusOrder == OrderStatus.Responsiveness)
                {
                    throw new CannotChangeStatusException(layer, "Status cannot be changed after a transaction is closed");
                }
                //בדיקה אם יש הרשאה לחשבון
                if (update == OrderStatus.SendEmail &&
                    dal.GetHostingUnit(or.HostingUnitKey).Owner.CollectionClearance == false)
                {
                    throw new CannotChangeStatusException(layer, "Unable to send email without account authorization");
                }
                if (update == OrderStatus.Responsiveness)
                {
                    //משתני עזר
                    HostingUnit tempHU = dal.GetHostingUnit(or.HostingUnitKey);
                    GuestRequest tempGR = dal.GetGuestRequest(or.GuestRequestKey);
                    //חישוב עמלה
                    or.Fee = Configuration.FeePerDay * Duration(tempGR);
                    //סימון במטריצה
                    changeToTrue(tempHU, tempGR);
                    //שינוי הסטטוס בדרישת הלקוח
                    UpdateGuestRequest(tempGR, GuestRequestStatus.ClosedWebsite);
                    //שינוי הסטטוסים של כל שאר ההזמנות הקשורות לאותו לקוח 
                    IEnumerable<Order> TempOrders = dal.GetAllOrders();
                    var orders =
                        from order in TempOrders
                        where order.GuestRequestKey == or.GuestRequestKey
                        select order;
                    foreach (var item in orders)
                    {
                        item.StatusOrder = OrderStatus.NoResponse;
                        dal.UpdateOrderStatus(item);
                    }
                }
                //עדכון סטטוס לנשלח מייל מתבצע במקום אחר כיון שהוא קשור לתהליכון של שליחת מייל
                if (update != OrderStatus.SendEmail)
                {
                    or.StatusOrder = update;
                    dal.UpdateOrderStatus(or);
                }
            }
            catch (MissingKeyException ex)
            {
                throw new MissingKeyException(ex.Layer, ex.Message, ex.Key);
            }
        }


        public void SendMail(Order or)
        {
            //יצירת אובייקט מסוג מייל
            MailMessage mail = new MailMessage();
            //כתובת נמען
            mail.To.Add((dal.GetGuestRequest(or.GuestRequestKey)).MailAddress);
            //מייל של השולח
            mail.From = new MailAddress("ylutvak@gmail.com");
            //נושא הודעה
            mail.Subject = "הסטטוס עבר ל- נשלח מייל";
            //תוכן הודעה
            mail.Body = "הודעה זו נשלחה אליך עקב קבלת הצעה";
            //הגדרה שתוכן ההודעה בפורמט HTML 
            mail.IsBodyHtml = true;
            // smt יצירת עצם מסוג 
            SmtpClient smtp = new SmtpClient();

            smtp.Host = "smtp.gmail.com";

            smtp.Credentials = new System.Net.NetworkCredential("ylutvak@gmail.com", "52300325");

            smtp.EnableSsl = true;
            try
            {
                smtp.Send(mail);
                or.StatusOrder = OrderStatus.SendEmail;
                or.OrderDate = DateTime.Today;
                dal.UpdateOrderStatus(or);
            }
            catch (Exception ex)
            {
                throw new MailException(layer, ex.Message);
            }
        }

        public void UpdateExpiredOrderStatusThread()
        {
            TimeSpan span = new TimeSpan(0, 1, 0);
            Thread thread = new Thread(UpdateExpiredOrderStatus);
            thread.Start();
            Thread.Sleep(span);
        }

        private void UpdateExpiredOrderStatus()
        {
            IEnumerable<Order> orders = dal.GetAllOrders();
            foreach (var or in orders)
            {
                if ((or.StatusOrder == OrderStatus.SendEmail) && (Duration(or.OrderDate) > 30))
                {
                    or.StatusOrder = OrderStatus.NoResponse;
                    dal.UpdateOrderStatus(or);
                }
            }
        }

        //החזרת רשימת ההזמנת
        public IEnumerable<Order> GetAllOrders()
        {
            return dal.GetAllOrders();
        }

        #endregion

        //=========================================================================================================

        #region BankBranch

        //החזרת רשימת הבנקים
        public IEnumerable<BankBranch> GetAllBankBranchs()
        {
            return dal.GetAllBankBranchs();
        }

        #endregion

        //=========================================================================================================

        #region Link    

        //פונקציה שמקבלת תאריך ומספר ימי נופש ומחזירה את רשימת כל יחידות האירוח הפנויות בתאריך זה 
        public IEnumerable<HostingUnit> GetAllAvailableUnits(DateTime date, int duration)
        {
            IEnumerable<HostingUnit> hostingUnits = dal.GetAllUnits();
            var allAvailableUnits =
                from unit in hostingUnits
                where matchingDates(unit, date, duration) == true
                orderby unit.Area
                select unit;
            return allAvailableUnits;
        }

        //פונקציה שמקבלת מספר ימים, ומחזירה את כל ההזמנות שמשך הזמן שעבר מאז שנוצרו 
        //גדול או שווה למספר הימים שהפונקציה קיבלה 
        public IEnumerable<Order> GetOrdersByMinimumDistanceFromCreationTime(int numOfDays)
        {
            IEnumerable<Order> orders = dal.GetAllOrders();
            var ordersByMinimumDistanceFromCreationTime =
                from order in orders
                let differenceCreate = Duration(order.CreateDate)
                where differenceCreate >= numOfDays
                orderby differenceCreate
                select order;
            return ordersByMinimumDistanceFromCreationTime;
        }

        //פונקציה שמקבלת מספר ימים, ומחזירה את כל ההזמנות שמשך הזמן שעבר מאז מאז שנשלח מייל ללקוח 
        //גדול או שווה למספר הימים שהפונקציה קיבלה
        public IEnumerable<Order> GetOrdersByMinimumDistanceFromSendingMailTime(int numOfDays)
        {
            IEnumerable<Order> orders = dal.GetAllOrders();
            var ordersByMinimumDistanceFromSendingMailTime =
                from order in orders
                where order.StatusOrder != OrderStatus.NotYetAdressed
                let differenceOrder = Duration(order.OrderDate)
                where differenceOrder >= numOfDays
                orderby differenceOrder
                select order;
            return ordersByMinimumDistanceFromSendingMailTime;
        }

        //פונקציה שמקבלת דרישת לקוח, ומחזירה את מספר ההזמנות שנשלחו ללקוח עבור דרישה זו 
        public int numOfOrders(GuestRequest guestRequest)
        {
            IEnumerable<Order> orders = dal.GetAllOrders();
            int count = 0;
            var Orders =
                (from order in orders
                 where order.GuestRequestKey == guestRequest.GuestRequestKey && order.StatusOrder != OrderStatus.NotYetAdressed
                 select ++count).ToList();
            return count;
        }

        //פונקציה שמקבלת יחידת אירוח ומחזירה את מספר ההזמנות שנסגרו בהצלחה עבור יחידה זו דרך האתר 
        public int NumOfOrdersForUnit(HostingUnit hu)
        {
            IEnumerable<Order> orders = dal.GetAllOrders();
            int count = 0;
            var succesOrders =
                (from order in orders
                 where order.HostingUnitKey == hu.HostingUnitKey && order.StatusOrder == OrderStatus.Responsiveness
                 select ++count).ToList();
            return count;
        }

        //פונקציה שמקבלת יחידת אירוח ומחזירה את רשימת ההזמנות שלה 
        public IEnumerable<Order> OrdersForUnit(HostingUnit hu)
        {
            IEnumerable<Order> orders = dal.GetAllOrders();
            var ordersForUnit =
                 from order in orders
                 where order.HostingUnitKey == hu.HostingUnitKey
                 select order;
            return ordersForUnit;
        }

        //פונקציה שיכולה להחזיר את כל דרישות הלקוח המתאימות לתנאי מסוים
        public IEnumerable<GuestRequest> MatchingConditions(Func<GuestRequest, bool> predicate = null)
        {
            IEnumerable<GuestRequest> guestRequests = dal.GetAllGuestRequests();
            var MatchingGR =
                from gr in guestRequests
                where predicate(gr) == true
                orderby gr.GuestRequestKey
                select gr;
            return MatchingGR;
        }

        //פונקציה המקבלת יחידת אירוח ומחזירה את כל הדרישות שמתאימות אליה
        public IEnumerable<GuestRequest> GetAllRequirementsMatchingToTheUnit(HostingUnit hu)
        {
            IEnumerable<GuestRequest> GuestRequests = dal.GetAllGuestRequests();
            var allRequirementsMatchingToTheUnit =
                from gr in GuestRequests
                where matchingBetweenUnitAndRequirement(hu, gr)
                orderby gr.EntryDate, gr.ReleaseDate
                select gr;
            return allRequirementsMatchingToTheUnit;
        }

        //הפונקציה מקבלת דרישת לקוח ומחזירה את מספר היחידות המתאימות לדרישה
        public int NumberOfUnitsThatMatchingTheRequirement(GuestRequest gr)
        {
            int count = 0;
            IEnumerable<HostingUnit> hostingUnits = dal.GetAllUnits();
            var numUnitsThatMatchingTheRequirement =
                (from hu in hostingUnits
                 where matchingBetweenUnitAndRequirement(hu, gr)
                 select ++count).ToList();
            return count;
        }

        #endregion

        //=========================================================================================================

        #region Grouping    

        //פונקציות גרופינג
        //הפונקציה מחזירה אוסף של קבוצות של דרישות לקוח מקובצות לפי איזור הנופש הנדרש
        public IEnumerable<IGrouping<Areas, GuestRequest>> GuestRequestsGroupingByArea()
        {
            IEnumerable<GuestRequest> guestRequests = dal.GetAllGuestRequests();
            IEnumerable<IGrouping<Areas, GuestRequest>> GroupsByArea =
                from gr in guestRequests
                orderby gr.EntryDate, gr.ReleaseDate//מיון פנימי כפול, קודם לפי תאריך כניסה ואח"כ לפי תאריך יציאה
                group gr by gr.Area into AreaGroup
                orderby AreaGroup.Count()//מיון של הקבוצות על פי מספר האיברים שיש בכל קבוצה
                select AreaGroup;
            return GroupsByArea;
        }

        //הפונקציה מחזירה אוסף קבוצות של של דרישות לקוח מקובצות לפי מספר הנופשים
        public IEnumerable<IGrouping<int, GuestRequest>> GuestRequestsGroupingByNumOfVacationers()
        {
            IEnumerable<GuestRequest> guestRequests = dal.GetAllGuestRequests();
            var GroupsByNumOfVacationers =
                from gr in guestRequests
                orderby gr.StatusGuestRequest//מיון פנימי לפי הסטטוס
                let sumVacationers = gr.Adults + gr.Children
                group gr by sumVacationers into sumVacationersGroup
                orderby sumVacationersGroup.Key//מיון של הקבוצות לפי המפתח - מספר הנופשים
                select sumVacationersGroup;
            return GroupsByNumOfVacationers;
        }

        //הפונקציה מחזירה אוסף קבוצות של מארחים מקובצות לפי מספר יחידות האירוח שהם מחזיקים
        public IEnumerable<IGrouping<int, Host>> hostsGroupingByNumberOfUnits()
        {
            IEnumerable<Host> hosts = dal.GetAllHosts();
            var GroupsByNumberOfUnits =
                from host in hosts
                orderby host.PrivateName[0]//מיון פנימי לפי שם המארח
                let numberOfUnits = numOfUnitsOfHost(host.HostKey)
                group host by numberOfUnits into numberOfUnitsGroup
                orderby numberOfUnitsGroup.Key//מיון של הקבוצות לפי המפתח - מספר יחידות האירוח
                select numberOfUnitsGroup;
            return GroupsByNumberOfUnits;
        }

        //הפונקציה מחזירה אוסף קבוצות של יחידות אירוח מקובצות לפי איזור הנופש
        public IEnumerable<IGrouping<Areas, HostingUnit>> hostingUnitsGroupingByArea()
        {
            IEnumerable<HostingUnit> hostingUnits = dal.GetAllUnits();
            var GroupsByArea =
                from hu in hostingUnits
                orderby hu.Pool//מיון פנימי לפי בריכה
                group hu by hu.Area into areaGroup
                orderby areaGroup.Count()//מיון של הקבוצות לפי מספר האיברים שיש בכל קבוצה
                select areaGroup;
            return GroupsByArea;
        }

        #endregion

        //=========================================================================================================

        #region Add XML 

        public void AddOrderList()
        {
            dal.AddOrderList();
        }
        public void AddHostingUnitList()
        {
            dal.AddHostingUnitList();
        }
        public void AddHostList()
        {
            dal.AddHostList();
        }
        public void AddGuestRequestList()
        {
            dal.AddGuestRequestList();
        }
        public void DownloadBankXml()
        {
            dal.DownloadBankXml();
        }

        #endregion

        //=========================================================================================================

    }
}