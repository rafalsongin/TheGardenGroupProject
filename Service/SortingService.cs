using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class SortingService
    {
         private TicketDao sorting =  new TicketDao();

        //dana
        public List<Ticket> SortTicketsByPriority(List<Ticket> tickets)
        {
            //sort the tickets by priority
            return sorting.GetTicketsSortingByPriorityDescending();
        }


    }
}
