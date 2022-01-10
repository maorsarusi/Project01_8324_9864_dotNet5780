using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FactoryDal
    {
        private static Dal_XML_imp instance = null;

        public static IDAL Instance
        {
            get
            {
                if (instance == null)
                    instance = new Dal_XML_imp();
                return instance;
            }
        }
    }
}
