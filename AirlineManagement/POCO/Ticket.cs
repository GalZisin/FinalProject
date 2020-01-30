using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement
{
    public class Ticket : IPoco
    {
        public Ticket()
        {
        }

        public Ticket(long iD, long fLIGHT_ID, long cUSTOMER_ID)
        {
            ID = iD;
            FLIGHT_ID = fLIGHT_ID;
            CUSTOMER_ID = cUSTOMER_ID;
        }

        public long ID { get; set; }
        public long FLIGHT_ID { get; set; }
        public long CUSTOMER_ID { get; set; }
        public static bool operator ==(Ticket thisTicket, Ticket otherTicket)
        {
            if (ReferenceEquals(thisTicket, null) && ReferenceEquals(otherTicket, null))
                return true;
            if (ReferenceEquals(thisTicket, null) || ReferenceEquals(otherTicket, null))
                return false;
            return thisTicket.ID == otherTicket.ID;
        }
        public static bool operator !=(Ticket thisTicket, Ticket otherTicket)
        {
            if (thisTicket == null && otherTicket == null)
                return false;
            if (thisTicket == null || otherTicket == null)
                return true;
            return !(thisTicket.ID == otherTicket.ID);
        }

        public override bool Equals(object obj)
        {
            var ticket = obj as Ticket;
            if (ReferenceEquals(ticket, null))
                return false;

            return this.ID == ticket.ID;
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }

    }
}
