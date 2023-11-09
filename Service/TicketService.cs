using DAL;
using Model;
using MongoDB.Driver;

namespace Service
{
    public class TicketService
    {
        private readonly TicketDao ticketDao;
        public TicketService()
        {
            ticketDao = new TicketDao();
        }
        public void CreateTicket(Ticket ticket)
        {
            ticketDao.CreateTicket(ticket);
        }

        public void UpdateTicket( Ticket updatedTicket) 
        {
            ticketDao.UpdateTicket(updatedTicket);
        }

        public void DeleteTicket(Ticket ticket)
        {
            ticketDao.DeleteTicket(ticket);
        }
        public Ticket ReadTicket(Ticket ticket) 
        {
            return ticketDao.ReadTicket(ticket);
        }

        public Ticket GetTicketByFilter(FilterDefinition<Ticket> filter)
        { 
            return ticketDao.GetTicketByFilter(filter);
        }
        
        public List<Ticket> GetOpenedTickets()
        {
            return ticketDao.GetOpenedTickets();
        }
        
        public List<Ticket> GetResolvedTickets()
        {
            return ticketDao.GetResolvedTickets();
        }
        
        public List<Ticket> GetClosedTickets()
        {
            return ticketDao.GetClosedTickets();
        }
        public List<Ticket> GetAllTickets()
        {
            return ticketDao.GetAllTickets();
        }
    }
}
