using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Data.Entity
{
    public class AvailableBookNotification
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int CustomerId { get; set; }
        public DateTime? NotificationDate { get; set; }
        public bool IsNotificationSent { get; set; }
        public virtual Book? Book { get; set; }
        public virtual Customer? Customer { get; set; }
    }
}
