using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ArchivingService
    {
        private ArchivedTicketDAO closedTicketArchiveDAO;
        public ArchivingService()
        {
            closedTicketArchiveDAO = new ArchivedTicketDAO();
        }
        public void ArchiveTicket(Ticket ticket)
        {
            // Archive the ticket in the secondary database
            closedTicketArchiveDAO.ArchiveTicket(ticket);

        }
    }
}
