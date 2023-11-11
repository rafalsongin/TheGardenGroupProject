using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class FilteringService
    {
        // Made by: Kim
        // Individual functionality
        public List<Ticket> FilterDate(List<Ticket> tickets, String selectedItem)
        {
            List<Ticket> newTickets = new List<Ticket>();
            foreach (Ticket ticket in tickets)
            {
                switch (selectedItem)
                {
                    case "Today":
                        if (ticket.ReportedOn.Day == DateTime.Today.Day)
                        {
                            newTickets.Add(ticket);
                        }
                        break;
                    case "This Month":
                        if (ticket.ReportedOn.Month == DateTime.Today.Month)
                        {
                            newTickets.Add(ticket);
                        }
                        break;
                    case "This Year":
                        if (ticket.ReportedOn.Year == DateTime.Today.Year)
                        {
                            newTickets.Add(ticket);
                        }
                        break;
                    default: break;
                }
            }
            return newTickets;
        }

        public List<Ticket> FilterType(List<Ticket> tickets, IncidentType type)
        {
            List<Ticket> newTickets = new List<Ticket>();
            foreach (Ticket ticket in tickets)
            {
                if (ticket.IncidentType == type)
                {
                    newTickets.Add(ticket);
                }
            }
            return newTickets;
        }

        public List<Ticket> FilterPriority(List<Ticket> tickets, Priority priority)
        {
            List<Ticket> newTickets = new List<Ticket>();
            foreach (Ticket ticket in tickets)
            {
                if (ticket.Priority == priority)
                {
                    newTickets.Add(ticket);
                }
            }
            return newTickets;
        }
    }
}
