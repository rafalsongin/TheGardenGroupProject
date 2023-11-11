using DAL;
using Model;

namespace Service
{
    public class SortingTicketsService
    {
        private readonly TicketDao _sortingTicketDao;

        public SortingTicketsService()
        {
            _sortingTicketDao = new TicketDao();
        }

        // dana
        public List<Ticket> GetAllTicketsSortedByPriorityDescending()
        {
            try
            {
                return _sortingTicketDao.GetAllTicketsSortedByPriorityDescending();
            }
            catch (Exception ex)
            {
                // Handle the exception (log it, show a message, etc.)
                throw new Exception($"Error in TicketService: {ex.Message}", ex);
            }
        }
    }
}