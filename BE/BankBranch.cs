using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BankBranch
    {
        public int BankNumber { get; set; }
        public string BankName { get; set; }
        public int BranchNumber { get; set; }
        public string BranchAddress { get; set; }
        public string BranchCity { get; set; }

        public override string ToString()
        {
            return string.Format("Bank number: {0}\nBank name: {1}\nBranch number: {2}\nBrunch address: {3}\nBranch city: {4}\n", BankNumber, BankName, BranchNumber, BranchAddress, BranchCity);
        }
    }
}
