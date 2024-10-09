using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Entity
{
    public class Book
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string Description { get; set; }
        public bool IsAvailable { get; set; } = true;
        public ICollection<BookReservation> BookReservations { get; set; } = new List<BookReservation>();
        public ICollection<AvailableBookNotification> AvailableBookNotifications { get; set; } = new List<AvailableBookNotification>();

    }
}
