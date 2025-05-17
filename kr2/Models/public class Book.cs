using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kr2.DB
{
    public class Book
    {
        public int Id { get; set; }
        public string ArticleNumber { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public BookStatus Status { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
