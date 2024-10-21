using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensInfo1
{
    public class Ticket
    {
        public int TicketID { get; set; }
        public int MovieID { get; set; }
        public string MovieName { get; set; }
        public int CustomerID { get; set; }
        public string Username { get; set; }
        public string LastName { get; set; }
        public float Price { get; set; }
        public DateTime PurchaseDate { get; set; } // If you want to store the purchase date
    }
    public class TicketData
    {
        private static TicketData _instance;

        public static TicketData Instance => _instance ??= new TicketData();

        public ObservableCollection<Ticket> Tickets { get; set; }

        private TicketData() // Constructor is private for singleton
        {
            Tickets = new ObservableCollection<Ticket>();
        }
    }
}
