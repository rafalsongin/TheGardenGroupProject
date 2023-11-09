using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Model;

namespace DAL
{
    public class TicketDao : BaseDao
    {
        private readonly IMongoCollection<Ticket> _ticketCollection;
        
        public TicketDao() 
        {
            _ticketCollection = GetTicketCollection();
        }

        public void CreateTicket(Ticket ticket)
        {
            _ticketCollection.InsertOne(ticket);
        }

        public void UpdateTicket(Ticket updatedTicket)
        {
            var filter = Builders<Ticket>.Filter.Eq(t => t.TicketId, updatedTicket.TicketId);
            var existingTicket = GetTicketByFilter(filter);

            if (existingTicket != null)
            {
                // Update the properties of the existing ticket with the values from the updated ticket
                existingTicket.TicketId = updatedTicket.TicketId;
                existingTicket.Subject = updatedTicket.Subject;
                existingTicket.IncidentType = updatedTicket.IncidentType;
                existingTicket.ReportedBy = updatedTicket.ReportedBy;
                existingTicket.Priority = updatedTicket.Priority;
                existingTicket.Deadline = updatedTicket.Deadline;
                existingTicket.Status = updatedTicket.Status;

                // Replace the existing ticket in the collection with the updated ticket
               _ticketCollection.ReplaceOne(filter, existingTicket);
            }
        }

        public void DeleteTicket(Ticket ticket)
        {
            var filter = Builders<Ticket>.Filter.Eq(t => t.TicketId, ticket.TicketId);
            _ticketCollection.DeleteOne(filter);
        }

        public Ticket ReadTicket(Ticket ticket)
        {
            // Find the ticket by its _id (treated as a string)
            var filter = Builders<Ticket>.Filter.Eq("_id", ticket.TicketId);

            // Execute the find operation to retrieve the ticket
            return _ticketCollection.Find(filter).FirstOrDefault();
        }


        public Ticket GetTicketByFilter(FilterDefinition<Ticket> filter)
        {
            // Assuming ticketCollection is your MongoDB collection
            var ticket = _ticketCollection.Find(filter).FirstOrDefault();
            return ticket;
        }

        public List<Ticket> GetAllTickets()
        {
            // Find all tickets
            var tickets = _ticketCollection.Find(new BsonDocument()).ToList();
            return tickets;
        }

        public List<Ticket> GetOpenedTickets()
        {
            // Find all tickets
            var tickets = _ticketCollection.Find(new BsonDocument()).ToList();
            List<Ticket> openedTickets = new List<Ticket>();
            foreach (var ticket in tickets)
            {
                if (ticket.Status == Status.Opened)
                {
                    openedTickets.Add(ticket);
                }
            }
            return openedTickets;   
        }

        public List<Ticket> GetResolvedTickets()
        {
            // Find all tickets
            var tickets = _ticketCollection.Find(new BsonDocument()).ToList();
            List<Ticket> resolvedTickets = new List<Ticket>();
            foreach (var ticket in tickets)
            {
                if (ticket.Status == Status.Resolved)
                {
                    resolvedTickets.Add(ticket);
                }
            }
            return resolvedTickets;
        }

        public List<Ticket> GetClosedTickets()
        {
            // Find all tickets
            var tickets = _ticketCollection.Find(new BsonDocument()).ToList();
            List<Ticket> closedTickets = new List<Ticket>();
            foreach (var ticket in tickets)
            {
                if (ticket.Status == Status.Closed)
                {
                    closedTickets.Add(ticket);
                }
            }
            return closedTickets;
        }
    }
}
