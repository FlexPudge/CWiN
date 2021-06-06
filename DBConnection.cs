using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceCenter.Models;

namespace ServiceCenter
{
    class DBConnection
    {
        private static Service_centerEntities1 _context;

        public static Service_centerEntities1 GetContext()
        {
            if (_context == null)
                _context = new Service_centerEntities1();
            return _context;
        }

    }
}
