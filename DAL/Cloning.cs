﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public static class Cloning
    {
        public static GuestRequest Clone(this GuestRequest original)
        {
            GuestRequest target = new GuestRequest();

            target.GuestRequestKey = original.GuestRequestKey;
            target.PrivateName = original.PrivateName;
            target.FamilyName = original.FamilyName;
            target.MailAddress = original.MailAddress;
            target.StatusGuestRequest = original.StatusGuestRequest;
            target.RegistrationDate = original.RegistrationDate;
            target.EntryDate = original.EntryDate;
            target.ReleaseDate = original.ReleaseDate;
            target.Area = original.Area;
            target.SubArea = original.SubArea;
            target.Type = original.Type;
            target.Adults = original.Adults;
            target.Children = original.Children;
            target.Pool = original.Pool;
            target.Jacuzzi = original.Jacuzzi;
            target.Garden = original.Garden;
            target.ChildrensAttractions = original.ChildrensAttractions;

            return target;
        }

        public static Host Clone(this Host original)
        {
            Host target = new Host();

            target.HostKey = original.HostKey;
            target.PrivateName = original.PrivateName;
            target.FamilyName = original.FamilyName;
            target.WholeName = original.WholeName;
            target.PhoneNumber = original.PhoneNumber;
            target.MailAddress = original.MailAddress;
            target.BankBranchDetails = original.BankBranchDetails;
            target.BankAccountNumber = original.BankAccountNumber;
            target.CollectionClearance = original.CollectionClearance;

            return target;
        }


        public static HostingUnit Clone(this HostingUnit original)
        {
            HostingUnit target = new HostingUnit();

            target.LastApdate = original.LastApdate;
            target.HostingUnitKey = original.HostingUnitKey;
            target.Owner = original.Owner;
            target.HostingUnitName = original.HostingUnitName;
            target.Diary = original.Diary;
            target.Area = original.Area;
            target.SubArea = original.SubArea;
            target.Type = original.Type;
            target.Capacity = original.Capacity;
            target.Pool = original.Pool;
            target.Jacuzzi = original.Jacuzzi;
            target.Garden = original.Garden;
            target.ChildrensAttractions = original.ChildrensAttractions;

            return target;
        }

        public static Order Clone(this Order original)
        {
            Order target = new Order();

            target.HostingUnitKey = original.HostingUnitKey;
            target.GuestRequestKey = original.GuestRequestKey;
            target.OrderKey = original.OrderKey;
            target.StatusOrder = original.StatusOrder;
            target.CreateDate = original.CreateDate;
            target.OrderDate = original.OrderDate;
            target.Fee = original.Fee;

            return target;
        }

        public static BankBranch Clone(this BankBranch original)
        {
            BankBranch target = new BankBranch();

            target.BankNumber = original.BankNumber;
            target.BankName = original.BankName;
            target.BranchNumber = original.BranchNumber;
            target.BranchAddress = original.BranchAddress;
            target.BranchCity = original.BranchCity;

            return target;
        }

    }
}
