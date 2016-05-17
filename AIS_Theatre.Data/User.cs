using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.Data
{
    public class User : BaseEntity<Guid>
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public User(string login, string password)
        {
            this.Id = Guid.NewGuid();
            this.Login = login;
            this.Password = password;
        }

        public User(Guid id, string login, string password)
        {
            this.Id = id;
            this.Login = login;
            this.Password = password;
        }

        public User Clone()
        {
            return (User)base.MemberwiseClone();
        }

        public override string ToString()
        {
            return Login;
        }
    }
}
