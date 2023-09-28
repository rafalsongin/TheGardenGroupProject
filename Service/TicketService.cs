using DAL;
using Model;

namespace Service
{
    public class TicketService
    {
        private  TicketDao ticketDao;

        public void CreateTicket(Ticket ticket)
        {
            ticketDao.CreateTicket(ticket);
        }

        public bool UpdateTicket(Ticket ticket,Dictionary<string,Ticket> updates) 
        {
            return ticketDao.UpdateTicket(ticket,updates);
        }

        public bool DeleteTicket(Ticket ticket)
        { 
        return ticketDao.DeleteTicket(ticket);
        }
        public Ticket ReadTicket(Ticket ticket) 
        {
            return ticketDao.ReadTicket(ticket);
        }
    }
}
