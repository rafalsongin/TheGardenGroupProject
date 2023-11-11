using DAL;
using Model;

namespace Service
{
    public class ArchivingService
    {
        private ArchivedTicketDao closedTicketArchiveDAO;

        public ArchivingService()
        {
            closedTicketArchiveDAO = new ArchivedTicketDao();
        }

        public void ArchiveTicket(Ticket ticket)
        {
            // Archive the ticket in the secondary database
            closedTicketArchiveDAO.ArchiveTicket(ticket);
        }
    }
}