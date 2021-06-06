using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace ServiceCenter.Check
{
    [Serializable]
    class Users
    {
        public List<string> Logins = new List<string>(); // Логин.
        public List<string> Passwords = new List<string>(); // Пароль.
    }
}
