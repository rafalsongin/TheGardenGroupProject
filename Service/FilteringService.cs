using Model;

namespace Service
{
    public class FilteringService
    {
        // Made by: Kim
        // Individual functionality
        public List<Ticket>
            FilterDate(List<Ticket> tickets,
                String selectedItem) // returning a list of all tickets with the correct dates as given
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

        public List<Ticket>
            FilterType(List<Ticket> tickets,
                IncidentType type) // returning a list of all tickets with the same type as given
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

        public List<Ticket>
            FilterPriority(List<Ticket> tickets,
                Priority priority) // returning a list of all tickets with the same prioriy as given
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

        public List<Ticket>
            FilterStatus(List<Ticket> tickets,
                Status status) // returning a list of all tickets with the same status as given
        {
            List<Ticket> newTickets = new List<Ticket>();
            foreach (Ticket ticket in tickets)
            {
                if (ticket.Status == status)
                {
                    newTickets.Add(ticket);
                }
            }

            return newTickets;
        }

        public List<Ticket>
            FilterSearch(List<Ticket> tickets,
                string search) // returning a list of all tickets with the same search as given
        {
            List<Ticket> newTickets = new List<Ticket>();
            foreach (Ticket ticket in tickets)
            {
                if (ticket.Subject.Contains(search) || ticket.Description.Contains(search))
                {
                    newTickets.Add(ticket);
                }
            }

            return newTickets;
        }
    }
}