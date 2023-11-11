using DAL;
using Model;

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
            // Check if the reported user exists before creating the ticket
            if (UserExists(ticket.ReportedBy))
            {
                // User exists, proceed with creating the ticket
                ticketDao.CreateTicket(ticket);
            }
            else
            {
                // User does not exist, handle accordingly (show a message, log, etc.)
                throw new Exception($"Error: User '{ticket.ReportedBy}' does not exist.");
            }
        }
       
        private bool UserExists(string username)
        {
            UserDao userDao = new UserDao();

            // Convert both the input username and usernames from the database to lowercase
            string lowercaseUsername = username.ToLower();

            // Check if the user exists in the user collection (case-insensitive)
            User existingUser = userDao.GetAllUsers().FirstOrDefault(user => user.Username.ToLower() == lowercaseUsername);
            return existingUser != null;
        }

        public void UpdateTicket(Ticket updatedTicket)
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