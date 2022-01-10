using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Host
    {
        public int HostKey { get; set; }
        public string PrivateName { get; set; }
        public string FamilyName { get; set; }
        private string m_wholeName;
        public string WholeName
        {
            get
            {
                return m_wholeName;
            }
            set
            {
                m_wholeName = PrivateName + " " + FamilyName;
            }
        }
        public string PhoneNumber { get; set; }
        public string MailAddress { get; set; }
        public BankBranch BankBranchDetails { get; set; }
        public int BankAccountNumber { get; set; }
        public bool CollectionClearance { get; set; }

        public override string ToString()
        {
            string str = "";
            if (CollectionClearance == true)
            {
                str = "Yes";
            }
            else
            {
                str = "No";
            }
            return string.Format("Host key:{0}\nHost name: {1}\nHost's phone number: {2}\nEmail address:{3}\nBank branch detailes: {4}\nBank account number: {5}\nThe money was collected?{6}\n", HostKey, WholeName, PhoneNumber, MailAddress, BankBranchDetails, BankAccountNumber, str);
        }
    }
}