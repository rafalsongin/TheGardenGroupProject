using DAL;
using Model;

namespace Service
{
    public class ArchivingService
    {
        private readonly ArchivedTicketDao _closedTicketArchiveDao;

        public ArchivingService()
        {
            _closedTicketArchiveDao = new ArchivedTicketDao();
        }

        public void ArchiveTicket(Ticket ticket)
        {
            // Archive the ticket in the secondary database
            _closedTicketArchiveDao.ArchiveTicket(ticket);
        }
    }
}