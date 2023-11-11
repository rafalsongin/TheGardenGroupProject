using Model;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections;
using System.Collections.ObjectModel;

namespace DAL
{
    public class TicketDao : BaseDao
    {
        private readonly IMongoCollection<Ticket> _ticketCollection;

        public TicketDao()
        {
            _ticketCollection = GetTicketCollection();
        }

        //Ghonim
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
                existingTicket.LastUpdated = DateTime.Now;

                // Replace the existing ticket in the collection with the updated ticket
                _ticketCollection.ReplaceOne(filter, existingTicket);
            }
        }

        public void DeleteTicket(Ticket ticket)
        {
            var filter = Builders<Ticket>.Filter.Eq(t => t.TicketId, ticket.TicketId);
            _ticketCollection.DeleteOne(filter);
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
        //Ghonim end

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
        
        //dana
        public long GetTicketCountForUser(string userEmail)
        {
            var filter = Builders<Ticket>.Filter.Eq("Email", userEmail);
            long ticketCount = _ticketCollection.CountDocuments(filter);
            return ticketCount;
        }

        public List<Ticket> GetAllTicketsSortedByPriorityDescending()
        {
            try
            {
                var tickets = _ticketCollection
                    .Find(new BsonDocument())
                    .ToList();

                var sortedTickets = tickets.OrderByDescending(ticket => ticket.Priority).ToList();
                return sortedTickets;
            }
            catch (Exception ex)
            {
                // Handle the exception (log it, show a message, etc.)
                throw new Exception($"Error getting and sorting tickets: {ex.Message}", ex);
            }
        }



        // dana end 



        //kim
        public List<Ticket> GetAllTicketsFromUser(User user) //getting all tickets based on username
        {
            IMongoQueryable<Ticket> results = from tickets in GetTicketCollection().AsQueryable()
                where tickets.ReportedBy == user.Username
                select tickets;
            return results.ToList();
        }

        public List<Ticket> GetOpenTicketsFromUser(User user) //getting pending and open tickets based on username
        {
            IMongoQueryable<Ticket> results = from tickets in GetTicketCollection().AsQueryable()
                where tickets.ReportedBy == user.Username
                where tickets.Status == Status.Pending || tickets.Status == Status.Opened
                select tickets;
            return results.ToList();
        }

        public List<Ticket> GetClosedTicketsFromUser(User user) // getting closed and resolved tickets based on username
        {
            IMongoQueryable<Ticket> results = from tickets in GetTicketCollection().AsQueryable()
                where tickets.ReportedBy == user.Username
                where tickets.Status == Status.Closed || tickets.Status == Status.Resolved
                select tickets;
            return results.ToList();
        }

        public List<Ticket> GetAllOpenTickets() // getting all open and pending tickets
        {
            IMongoQueryable<Ticket> results = from tickets in GetTicketCollection().AsQueryable()
                where tickets.Status == Status.Pending || tickets.Status == Status.Opened
                select tickets;
            return results.ToList();
        }

        public List<Ticket> GetAllClosedTickets() // getting all closed and resolved tickets
        {
            IMongoQueryable<Ticket> results = from tickets in GetTicketCollection().AsQueryable()
                where tickets.Status == Status.Closed || tickets.Status == Status.Resolved
                select tickets;
            return results.ToList();
        }
    }
}