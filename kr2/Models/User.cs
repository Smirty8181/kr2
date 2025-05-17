using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kr2.DB
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }

        public List<Book> Books { get; set; } = new List<Book>();
    }
}
