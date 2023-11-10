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
        //Ghonim
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
        public List<Ticket> GetAllTickets()
        {
            return ticketDao.GetAllTickets();
        }
        //Ghonim end

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
    

        //kim
        public List<Ticket> GetAllTicketsFromUser(User user)
        {
            return ticketDao.GetAllTicketsFromUser(user);
        }

        public List<Ticket> GetOpenTicketsFromUser(User user)
        {
            return ticketDao.GetOpenTicketsFromUser(user);
        }

        public List<Ticket> GetClosedTicketsFromUser(User user)
        {
            return ticketDao.GetClosedTicketsFromUser(user);
        }


        public List<Ticket> GetAllOpenTickets()
        {
            return ticketDao.GetAllOpenTickets();
        }

        public List<Ticket> GetAllClosedTickets()
        {
            return ticketDao.GetAllClosedTickets();
        }

        //dana
        public long GetTicketCountForUser(string userEmail)
        {
            return ticketDao.GetTicketCountForUser(userEmail);
        }
    }
}
