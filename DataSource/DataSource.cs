using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DS
{
    public class DataSource
    {
        static DateTime dateTime = DateTime.Now;
        public static List<Host> hosts = new List<Host>()
        {

            new Host()
            {
                HostKey = Configuration.HostKey,
                PrivateName = "David",
                FamilyName = "Cohen",
                PhoneNumber = "0522222222",
                MailAddress = "David@gmail.com",
                //BankBranchDetails = new BankBranch()
                //{
                //    BankNumber =12,
                //    BankName = "discount",
                //    BranchNumber = 41,
                //    BranchAddress = "Kanfey Nesharim",
                //    BranchCity = "Jerusalem"
                //},
                BankAccountNumber = 222222,
                CollectionClearance = true
            },
            new Host()
            {
                HostKey =Configuration.HostKey,
                PrivateName = "Maor",
                FamilyName = "Sarusi",
                PhoneNumber = "0530090097",
                MailAddress = "ms@gmail.com",
                //BankBranchDetails = new BankBranch()
                //{
                //    BankNumber =12,
                //    BankName = "discount",
                //    BranchNumber = 42,
                //    BranchAddress = "Malchey Israel",
                //    BranchCity = "Jerusalem"
                //},
                BankAccountNumber = 333333,
                CollectionClearance = false
            },
            new Host()
            {
                HostKey = Configuration.HostKey,
                PrivateName = "Ishay",
                FamilyName = "Lutwok",
                PhoneNumber = "0530089097",
                MailAddress = "ilu@gmail.com",
                //BankBranchDetails = new BankBranch()
                //{
                //   BankNumber = 12,
                //   BankName = "discount",
                //   BranchNumber = 43,
                //   BranchAddress = "Kiryat Moshe",
                //   BranchCity = "Jerusalem"
                // },
                 BankAccountNumber = 44444,
                 CollectionClearance = true
            },
            new Host()
            {
                HostKey = Configuration.HostKey,
                PrivateName = "Avraham",
                FamilyName = "Levi",
                PhoneNumber = "0530989097",
                MailAddress = "al@gmail.com",
                //BankBranchDetails = new BankBranch()
                //{
                //    BankNumber=12,
                //    BankName = "discount",
                //    BranchNumber = 44,
                //    BranchAddress = "Beit Hakerem",
                //    BranchCity = "Jerusalem"
                //},
                BankAccountNumber = 55555,
                CollectionClearance = true
            },
            new Host()
            {
                HostKey = Configuration.HostKey,
                PrivateName = "Yaakov",
                FamilyName = "Ythaki",
                PhoneNumber = "0530923097",
                MailAddress = "yayi@gmail.com",
                //BankBranchDetails = new BankBranch()
                //{
                //    BankNumber =12,
                //    BankName = "discount",
                //    BranchNumber = 45,
                //    BranchAddress = "Ramot Eshkol",
                //    BranchCity = "Jerusalem"
                //},
                BankAccountNumber = 66666,
                CollectionClearance = false
            }
        };

        public static List<HostingUnit> HostingUnits = new List<HostingUnit>()
        {
            new HostingUnit()
            {
                HostingUnitKey = Configuration.HostingUnitKey,
                //Owner = hosts[0],
                HostingUnitName = "Halav Udevash",
                Area=Areas.GushDan,
                Capacity = 4,
                Type=ResortType.HostingApartment,
                Pool   =true,
                Jacuzzi=true,
                Garden =false,
                ChildrensAttractions=false
            },

            new HostingUnit()
            {
                HostingUnitKey =Configuration.HostingUnitKey,
                //Owner = hosts[1],
                HostingUnitName = "Heaven's door",
                Area = Areas.Jerusalem,
                SubArea ="Mevaseret",
                Type=ResortType.HostingApartment,
                Pool   =true,
                Jacuzzi=true,
                Garden =false,
                ChildrensAttractions=false
            },

            new HostingUnit()
            {
                HostingUnitKey =Configuration.HostingUnitKey,
                //Owner = hosts[2],                
                HostingUnitName = "Olam Umelo'o",
                Area = Areas.North,
                Type=ResortType.HostingApartment,
                Pool   =true,
                Jacuzzi=true,
                Garden =false,
                ChildrensAttractions=false
            },

            new HostingUnit()
            {
                HostingUnitKey =Configuration.HostingUnitKey,
                //Owner = hosts[3],
                HostingUnitName = "the ZIMMER",
                Area = Areas.South,
                Type=ResortType.HostingApartment,
                Pool   =true,
                Jacuzzi=true,
                Garden =false,
                ChildrensAttractions=false
            },

            new HostingUnit()
            {
                HostingUnitKey =Configuration.HostingUnitKey,
                //Owner = hosts[4],
                HostingUnitName = "Zimerman",
                Area = Areas.Arava,
                Type=ResortType.HostingApartment,
                Pool   =true,
                Jacuzzi=true,
                Garden =false,
                ChildrensAttractions=false
            }
        };

        public static List<GuestRequest> GuestRequests = new List<GuestRequest>()
        {
            new GuestRequest()
            {
                GuestRequestKey =Configuration.GuestRequestKey,
                PrivateName="Moshe",
                FamilyName ="Melicson",
                MailAddress="MM@gmail.com",
                RegistrationDate=dateTime,
                EntryDate=dateTime.AddDays(15),
                ReleaseDate=dateTime.AddDays(21),
                Area=Areas.GushDan,
                Type=ResortType.HotelRoom,
                Adults=2,
                Children=7,
                Pool=Options.Necessary,
                Jacuzzi=Options.NotInterest,
                Garden=Options.Possible,
                ChildrensAttractions=Options.Necessary,
                StatusGuestRequest=GuestRequestStatus.Expired
            },

            new GuestRequest()
            {
                GuestRequestKey =Configuration.GuestRequestKey,
                PrivateName = "Menahem",
                FamilyName = "Buzaglo",
                MailAddress = "MB@gmail.com",
                RegistrationDate =dateTime.AddDays(55),
                EntryDate = dateTime.AddDays(60),
                ReleaseDate =dateTime.AddDays(65),
                Area = Areas.GushDan,
                SubArea = "Tel Aviv",
                Type = ResortType.Zimmer,
                Adults = 5,
                Children = 20,
                Pool = Options.Necessary,
                Jacuzzi = Options.Possible,
                Garden = Options.NotInterest,
                ChildrensAttractions = Options.Necessary,
                StatusGuestRequest = GuestRequestStatus.Opened
            },

            new GuestRequest()
            {
                GuestRequestKey = Configuration.GuestRequestKey,
                PrivateName = "Eran",
                FamilyName = "Davidov",
                MailAddress = "ED@gmail.com",
                RegistrationDate = dateTime.AddDays(12),
                EntryDate = dateTime.AddDays(16),
                ReleaseDate = dateTime.AddDays(30),
                Area = Areas.Jerusalem,
                SubArea = "Mevaseret",
                Type = ResortType.HostingApartment,
                Adults = 2,
                Children = 3,
                Pool = Options.NotInterest,
                Jacuzzi = Options.Necessary,
                Garden = Options.Possible,
                ChildrensAttractions = Options.Possible,
                StatusGuestRequest = GuestRequestStatus.ClosedWebsite
            },

            new GuestRequest()
            {
                GuestRequestKey =Configuration.GuestRequestKey,
                PrivateName = "Israel",
                FamilyName = "Ben Yossef",
                MailAddress = "IBY@gmail.com",
                RegistrationDate = dateTime.AddDays(-40),
                EntryDate = dateTime.AddDays(-30),
                ReleaseDate = dateTime.AddDays(-22),
                Area = Areas.North,
                SubArea = "Ha-Golan",
                Type = ResortType.Camp,
                Adults = 2,
                Children = 12,
                Pool = Options.NotInterest,
                Jacuzzi = Options.NotInterest,
                Garden = Options.NotInterest,
                ChildrensAttractions = Options.Necessary,
                StatusGuestRequest = GuestRequestStatus.Opened
            },

            new GuestRequest()
            {
                GuestRequestKey =Configuration.GuestRequestKey,
                PrivateName = "Ovadya",
                FamilyName = "Yechezkel",
                MailAddress = "OY@gmail.com",
                RegistrationDate = dateTime.AddDays(-15),
                EntryDate = dateTime.AddDays(-9),
                ReleaseDate = dateTime.AddDays(-11),
                Area = Areas.South,
                SubArea = "Beer Sheva",
                Type = ResortType.HotelRoom,
                Adults = 4,
                Children = 15,
                Pool = Options.Necessary,
                Jacuzzi = Options.Necessary,
                Garden = Options.Possible,
                ChildrensAttractions = Options.Necessary,
                StatusGuestRequest = GuestRequestStatus.Opened
            },
        };

        public static List<Order> Orders = new List<Order>()
        {
            new Order()
            {
                HostingUnitKey=HostingUnits[0].HostingUnitKey,
                GuestRequestKey=GuestRequests[1].GuestRequestKey,
                OrderKey=Configuration.OrderKey,
                StatusOrder=OrderStatus.NoResponse,
                CreateDate = dateTime.AddDays(-58),
                OrderDate =dateTime.AddDays(-56),
            },

            new Order()
            {
                HostingUnitKey = HostingUnits[2].HostingUnitKey,
                GuestRequestKey = GuestRequests[3].GuestRequestKey,
                OrderKey =Configuration.OrderKey,
                StatusOrder = OrderStatus.Responsiveness,
                CreateDate = dateTime.AddDays(-15),
                OrderDate = dateTime.AddDays(-13),
            },

            new Order()
            {
                HostingUnitKey =HostingUnits[1].HostingUnitKey,
                GuestRequestKey =GuestRequests[2].GuestRequestKey,
                OrderKey =Configuration.OrderKey,
                StatusOrder = OrderStatus.SendEmail,
                CreateDate = dateTime.AddDays(-24),
                OrderDate = dateTime.AddDays(-23),
            },

            new Order()
            {
                HostingUnitKey= HostingUnits[3].HostingUnitKey,
                GuestRequestKey =GuestRequests[4].GuestRequestKey,
                OrderKey =Configuration.OrderKey,
                StatusOrder = OrderStatus.NoResponse,
                CreateDate =dateTime.AddDays(-12),
                OrderDate = dateTime.AddDays(-10),
            },

            new Order()
            {
                HostingUnitKey = HostingUnits[4].HostingUnitKey,
                GuestRequestKey =GuestRequests[0].GuestRequestKey,
                OrderKey =Configuration.OrderKey,
                StatusOrder = OrderStatus.NotYetAdressed,
                CreateDate =dateTime.AddDays(-13),
                OrderDate = dateTime.AddDays(-10),
            }
        };
    }
}

