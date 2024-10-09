using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Entity
{
    public class Customer
    { 
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public ICollection<BookReservation> BookReservations { get; set; } = new List<BookReservation>();
        public ICollection<AvailableBookNotification> AvailableBookNotifications { get; set; } = new List<AvailableBookNotification>();


    }
}
