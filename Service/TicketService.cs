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

        public void UpdateTicket(Ticket updatedTicket)
        {
            ticketDao.UpdateTicket(updatedTicket);
        }

        public void DeleteTicket(FilterDefinition<Ticket> filter)
        {
            ticketDao.DeleteTicket(filter);
        }
        public Ticket ReadTicket(Ticket ticket) 
        {
            return ticketDao.ReadTicket(ticket);
        }

        public Ticket GetTicketByFilter(FilterDefinition<Ticket> filter)
        { 
            return ticketDao.GetTicketByFilter(filter);
        }

        public List<Ticket> GetAllTickets()
        {
            return ticketDao.GetAllTickets();
        }
    }
}
