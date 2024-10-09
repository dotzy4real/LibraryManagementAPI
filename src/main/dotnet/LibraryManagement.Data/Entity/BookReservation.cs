using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Entity
{
    public class BookReservation
    {
        public int Id { get; set; } 
        public int BookId { get; set; }
        public int CustomerId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime ReservedDate { get; set; } = DateTime.Now;
        public bool ReservationStatus { get; set; } = true;
        public bool RequestAvailabilityNotification { get; set; }
        public virtual Book? Book { get; set; }
        public virtual Customer? Customer { get; set; }

    }
}
