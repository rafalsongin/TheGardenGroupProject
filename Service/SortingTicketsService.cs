using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class SortingTicketsService
    {
        private TicketDao sortingTicketDao;
        public SortingTicketsService()
        {
            sortingTicketDao = new TicketDao();
        }

        //dana

        public List<Ticket> GetAllTicketsSortedByPriorityDescending()
        {
            try
            {
                return sortingTicketDao.GetAllTicketsSortedByPriorityDescending();
            }
            catch (Exception ex)
            {
                // Handle the exception (log it, show a message, etc.)
                throw new Exception($"Error in TicketService: {ex.Message}", ex);
            }
        }


    }
}
