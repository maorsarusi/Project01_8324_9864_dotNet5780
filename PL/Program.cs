using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BE;

namespace PL
{
    class Program
    {
        static void Main(string[] args)
        {
            IBL bl = FactoryBl.Instance;
            //bl.AddGuestRequestList();
            //bl.AddHostingUnitList();
            //bl.AddOrderList();
            //bl.AddHostList();

            int choice, grChoice, huChoice, orderChoice;

            Console.WriteLine(
               @"enter your chioce:
                1-for guest request options,
                2-for units options, 
                3- for order options, 
                0 to exit");
            choice = int.Parse(Console.ReadLine());
            while (choice != 0)
            {
                switch (choice)
                {
                    case 1:
                        Console.WriteLine(
                            @"enter what you want to do:
                             1-for addind guest request,
                             2-for update guest request,
                             3-for getting all guest requests' list
                             4- for gettings group of guest request by areas
                             0 - to leave guest request options");

                        grChoice = int.Parse(Console.ReadLine());
                        while (grChoice != 0)
                        {
                            switch (grChoice)
                            {
                                case 1:
                                    Console.WriteLine(@"enter the values of the guest request in this order:
                                          Private Name 
                                          Family Name
                                          Mail Address
                                          guest Request Status
                                          type 
                                          Adults 
                                          Children ,
                                          for this option: if your answer is 'Necessary' enter 1,'Possible':press 2,'NotInterest':press 3
                                          pool 
                                          jacuzzi 
                                          garden
                                          childrens Attractions");
                                    Random r = new Random();

                                    string Private = Console.ReadLine();
                                    string Family = Console.ReadLine();
                                    string Mail = Console.ReadLine();
                                    DateTime Registration = DateTime.Now;
                                    DateTime Entry = Registration.AddDays(20);
                                    DateTime Release = Registration.AddDays(25);
                                    string sub = Console.ReadLine();
                                    Areas areas = (Areas)r.Next(1, 6);
                                    int adulds = int.Parse(Console.ReadLine());
                                    int children = int.Parse(Console.ReadLine());
                                    int pool = int.Parse(Console.ReadLine());
                                    int Jacuzzi = int.Parse(Console.ReadLine());
                                    int Garden = int.Parse(Console.ReadLine());
                                    int attractions = int.Parse(Console.ReadLine());
                                    Options Pool = (Options)pool;
                                    Options JACUZZi = (Options)Jacuzzi;
                                    Options gardens = (Options)Garden;
                                    Options Attractions = (Options)attractions;


                                    GuestRequest guestRequest = new GuestRequest()
                                    {
                                        // GuestRequestKey = Configuration.GuestRequestKey,
                                        PrivateName = Private,
                                        FamilyName = Family,
                                        MailAddress = Mail,
                                        StatusGuestRequest = GuestRequestStatus.ClosedWebsite,
                                        RegistrationDate = Registration,
                                        EntryDate = Entry,
                                        ReleaseDate = Release,
                                        Area = Areas.GushDan,
                                        SubArea = sub,
                                        Type = ResortType.HotelRoom,
                                        Adults = adulds,
                                        Children = children,
                                        Pool = Pool,
                                        Jacuzzi = JACUZZi,
                                        Garden = gardens,
                                        ChildrensAttractions = Attractions
                                    };
                                    bl.AddGuestRequest(guestRequest);
                                    Console.WriteLine("The guest request number " + guestRequest.GuestRequestKey + " had been added successfully");
                                    List<GuestRequest> guestRequests = bl.GetAllGuestRequests().ToList();
                                    foreach (var item in guestRequests)
                                    {
                                        Console.WriteLine("guest request no: " + item.GuestRequestKey);
                                    }
                                    break;
                                case 2:
                                    List<GuestRequest> guests = bl.GetAllGuestRequests().ToList();
                                    foreach (var item in guests)
                                    {
                                        Console.WriteLine("guest request no: " + item.GuestRequestKey + " status: " + item.StatusGuestRequest);
                                    }
                                    int guestrequestStatus = 0;
                                    Console.WriteLine("enter the key of the guest request you want to change:");
                                    int key = int.Parse(Console.ReadLine());

                                    int j = 0;


                                    for (int i = 0; i < guests.Count(); i++)
                                    {
                                        if (key == guests[i].GuestRequestKey)
                                        {
                                            Console.WriteLine(@"enter the status you want to change to it
                                                            1-for 'Opened'
                                                            2-for ' ClosedWebsite'
                                                            3- for 'Expired'");
                                            guestrequestStatus = int.Parse(Console.ReadLine());
                                            j = i;
                                            break;
                                        }
                                    }
                                    GuestRequestStatus grs = (GuestRequestStatus)guestrequestStatus;
                                    GuestRequest gr = new GuestRequest()
                                    {
                                        //  GuestRequestKey = guests[j].GuestRequestKey,
                                        PrivateName = guests[j].PrivateName,
                                        FamilyName = guests[j].FamilyName,
                                        MailAddress = guests[j].MailAddress,
                                        RegistrationDate = guests[j].RegistrationDate,
                                        EntryDate = guests[j].EntryDate,
                                        ReleaseDate = guests[j].ReleaseDate,
                                        Area = guests[j].Area,
                                        SubArea = guests[j].SubArea,
                                        Type = guests[j].Type,
                                        Adults = guests[j].Adults,
                                        Children = guests[j].Children,
                                        Pool = guests[j].Pool,
                                        Jacuzzi = guests[j].Jacuzzi,
                                        Garden = guests[j].Garden,
                                        ChildrensAttractions = guests[j].ChildrensAttractions
                                    };
                                    bl.UpdateGuestRequest(gr, grs);
                                    List<GuestRequest> e = bl.GetAllGuestRequests().ToList();
                                    foreach (var item in e)
                                    {
                                        Console.WriteLine("guest request no: " + item.GuestRequestKey + " status: " + item.StatusGuestRequest);
                                    }

                                    break;
                                case 3:
                                    List<GuestRequest> gs = bl.GetAllGuestRequests().ToList();
                                    foreach (var item in gs)
                                    {
                                        Console.WriteLine(item);
                                        Console.WriteLine();
                                    }
                                    break;
                                case 4:
                                    IEnumerable<IGrouping<Areas, GuestRequest>> list = bl.GuestRequestsGroupingByArea();
                                    foreach (var group in list)
                                    {
                                        Console.WriteLine(group.Key);
                                        Console.WriteLine(group.Count());
                                        foreach (var item in group)
                                        {
                                            Console.WriteLine(item);
                                        }
                                    }
                                    break;

                                default:
                                    Console.WriteLine("the number has'nt true for guest request");
                                    break;
                            }

                            Console.WriteLine(
                         @"enter what you want to do:
                             1 - for addind guest request,
                             2 - for update guest request,
                             3 - for getting all guest requests' list
                             4- for gettings group of guest request by areas
                             0 - to leave guest request options");
                            grChoice = int.Parse(Console.ReadLine());

                        }

                        break;

                    case 2:
                        Console.WriteLine(
                         @"enter what you want to do:
                             1-for addind unit,
                             2-for update unit,
                             3-for getting all units' list
                             4- for gettings group of units by areas
                             0- for leave unit's options");

                        huChoice = int.Parse(Console.ReadLine());
                        while (huChoice != 0)
                        {
                            switch (huChoice)
                            {
                                case 1:
                                    Console.WriteLine(@"enter the values of the unit you want to add in this order: 
                                                            Hosting Unit Key                                                          
                                                            Hosting Unit Name
                                                            num Of Orders");

                                    int huk = int.Parse(Console.ReadLine());
                                    string hun = Console.ReadLine();
                                    int noford = int.Parse(Console.ReadLine());
                                    HostingUnit hostingUnit = new HostingUnit()
                                    {
                                        HostingUnitKey = huk,
                                        Owner = new Host()
                                        {
                                            HostKey = 4,
                                            PrivateName = "Avraham",
                                            FamilyName = "Levi",
                                            PhoneNumber = "0530989097",
                                            MailAddress = "al@gmail.com",
                                            BankBranchDetails = new BankBranch()
                                            {
                                                BankNumber = 1111,
                                                BankName = "discount",
                                                BranchNumber = 44,
                                                BranchAddress = "Beit Hakerem",
                                                BranchCity = "Jerusalem"
                                            },
                                            BankAccountNumber = 55555,
                                            CollectionClearance = true
                                        },
                                        HostingUnitName = hun,
                                        Diary = new bool[31, 12],
                                        Area = Areas.GushDan,

                                    };
                                    bl.AddUnit(hostingUnit);
                                    Console.WriteLine("Unit Number: " + hostingUnit.HostingUnitKey + " has been succeefully added");
                                    List<HostingUnit> hu1 = bl.GetAllUnits().ToList();

                                    foreach (var item in hu1)
                                    {
                                        Console.WriteLine("Unit Number: " + item.HostingUnitKey + " Name: " + item.HostingUnitName);
                                    }
                                    break;
                                case 2:
                                    List<HostingUnit> hu = bl.GetAllUnits().ToList();
                                    Console.WriteLine("enter the key of the unit you want to change:");
                                    int key = int.Parse(Console.ReadLine());
                                    Console.WriteLine("enter what you want to change");

                                    HostingUnit unit = null;
                                    for (int i = 0; i < hu.Count(); i++)
                                    {
                                        if (key == hu[i].HostingUnitKey)
                                        {
                                            unit = new HostingUnit()
                                            {
                                                HostingUnitKey = hu[i].HostingUnitKey,
                                                Owner = hu[i].Owner,
                                                HostingUnitName = hu[i].HostingUnitName,
                                                Diary = hu[i].Diary,
                                                Area = hu[i].Area,
                                                SubArea = "bla bla",
                                                Type = ResortType.Camp,
                                                Pool = false,
                                                Jacuzzi = false,
                                                Garden = true,
                                                ChildrensAttractions = true,
                                            };
                                            break;
                                        }
                                    }
                                    bl.UpdateUnit(unit);
                                    List<HostingUnit> units = bl.GetAllUnits().ToList();

                                    break;
                                case 3:
                                    List<HostingUnit> hostings = bl.GetAllUnits().ToList();
                                    foreach (var item in hostings)
                                    {
                                        Console.WriteLine(item);
                                        Console.WriteLine();

                                    }
                                    break;
                                default:
                                    Console.WriteLine("the number has'nt true for hosting units");
                                    break;
                            }
                            Console.WriteLine(
                       @"enter what you want to do:
                             1-for addind unit,
                             2-for update unit,
                             3-for getting all units' list
                             4- for gettings group of units by areas
                             0- for leave unit's options");

                            huChoice = int.Parse(Console.ReadLine());
                        }
                        break;
                    case 3:
                        Console.WriteLine(
                        @"enter what you want to do:
                             1-for adding order,
                             2-for update order,
                             3-for getting allorders' list
                             4- for gettings group of units by areas
                             0- for leave order's options");

                        orderChoice = int.Parse(Console.ReadLine());
                        while (orderChoice != 0)
                        {
                            switch (orderChoice)
                            {
                                case 1:
                                    Console.WriteLine(@"enter the values of orders this way:
                                                       Guest Request Key
                                                        Order Key
                                                        orderStatus");
                                    int grk = int.Parse(Console.ReadLine());
                                    int ok = int.Parse(Console.ReadLine());
                                    OrderStatus orderstatus = (OrderStatus)int.Parse(Console.ReadLine());
                                    DateTime current = DateTime.Now;
                                    Order order = new Order()
                                    {
                                        GuestRequestKey = grk,
                                        OrderKey = ok,
                                        StatusOrder = orderstatus,
                                        CreateDate = current,
                                        OrderDate = current.AddDays(5)

                                    };
                                    bl.AddOrder(order);
                                    Console.WriteLine("The order number " + order.OrderKey + " had been added successfully");
                                    List<Order> orders = bl.GetAllOrders().ToList();
                                    foreach (var item in orders)
                                    {
                                        Console.WriteLine(item);
                                    }

                                    break;
                                case 2:
                                    List<Order> or = bl.GetAllOrders().ToList();
                                    foreach (var item in or)
                                    {
                                        Console.WriteLine("order no: " + item.OrderKey + " status: " + item.StatusOrder);
                                    }
                                    int orstatus = 0;
                                    Console.WriteLine("enter the key of the order you want to change:");
                                    int key = int.Parse(Console.ReadLine());

                                    int j = 0;


                                    for (int i = 0; i < or.Count(); i++)
                                    {
                                        if (key == or[i].OrderKey)
                                        {
                                            Console.WriteLine(@"enter the status you want to change to it
                                                            1-for ' NotYetAdressed'
                                                            2-for '  SendEmail'
                                                            3- for 'NoResponse'
                                                            4- for  'Responsiveness' ");
                                            orstatus = int.Parse(Console.ReadLine());
                                            j = i;
                                            break;
                                        }

                                    }
                                    OrderStatus status = (OrderStatus)orstatus;
                                    Order order1 = new Order()
                                    {
                                        HostingUnitKey = or[j].HostingUnitKey,
                                        GuestRequestKey = or[j].GuestRequestKey,
                                        OrderKey = or[j].OrderKey,
                                        StatusOrder = status,
                                        CreateDate = or[j].CreateDate,
                                        OrderDate = or[j].OrderDate
                                    };
                                    List<GuestRequest> g = bl.GetAllGuestRequests().ToList();
                                    int k = 0;
                                    for (int i = 0; i < g.Count(); i++)
                                    {
                                        if (g[i].GuestRequestKey == order1.GuestRequestKey)
                                        {
                                            k = i;
                                            break;
                                        }
                                    }

                                    bl.UpdateOrderStatus(order1, status);
                                    List<Order> e = bl.GetAllOrders().ToList();
                                    foreach (var item in e)
                                    {
                                        Console.WriteLine("order no: " + item.OrderKey + " status: " + item.StatusOrder);
                                    }
                                    break;
                                case 3:
                                    List<Order> ord = bl.GetAllOrders().ToList();
                                    foreach (var item in ord)
                                    {
                                        Console.WriteLine(item);
                                        Console.WriteLine();

                                    }
                                    break;
                                default:
                                    break;
                            }
                            Console.WriteLine(
                                                   @"enter what you want to do:
                             1-for adding order,
                             2-for update order,
                             3-for getting allorders' list
                             4- for gettings group of units by areas
                             0- for leave order's options");

                            orderChoice = int.Parse(Console.ReadLine());
                        }
                        break;
                    default:
                        Console.WriteLine("the number has'nt true for options");
                        break;
                }

                Console.WriteLine(
       @"enter your chioce:
                1-for guest request options,
                2-for units options, 
                3- for order options, 
                0 to exit");
                choice = int.Parse(Console.ReadLine());
            }
            Console.WriteLine("Bye bye");
            Console.ReadKey();
        }
    }
}
