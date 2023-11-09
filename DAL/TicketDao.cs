using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Model;
using System.Net.Sockets;
using MongoDB.Driver.Linq;

namespace DAL
{
    public class TicketDao : BaseDao
    {
/*        private MongoClient client;
        private IMongoCollection<Ticket> ticketCollection;
        private IMongoDatabase database;*/
/*        public TicketDao()
        {
            client = new MongoClient("mongodb+srv://dbUser:test123@thegardengroupserverles.vovxxor.mongodb.net/");
            database = client.GetDatabase("TheGardenGroup");//geting the database
            ticketCollection = database.GetCollection<Ticket>("IncidentTicket");//the table that we want to work on
        }
*/
        public void CreateTicket(Ticket ticket)
        {
            GetTicketCollection().InsertOne(ticket);
            //ticketCollection.InsertOne(ticket);
        }

        public void UpdateTicket(FilterDefinition<Ticket> filter, Ticket updatedTicket)
        {
            Ticket existingTicket = GetTicketByFilter(filter);

            if (existingTicket != null)
            {
                // Update the properties of the existing ticket with the values from the updated ticket
                existingTicket.TicketId = updatedTicket.TicketId;
                existingTicket.Subject = updatedTicket.Subject;
                existingTicket.IncidentType = updatedTicket.IncidentType;
                existingTicket.Assignedby = updatedTicket.Assignedby;
                existingTicket.Priority = updatedTicket.Priority;
                existingTicket.Deadline = updatedTicket.Deadline;

                // Replace the existing ticket in the collection with the updated ticket
                GetTicketCollection().ReplaceOne(filter, existingTicket);
            }
        }

        public void DeleteTicket(FilterDefinition<Ticket> filter)
        {
            // FilterDefinition<Ticket> deleteDefinitions = GetTicketByFilter(new );

            var ticketToDelete = GetTicketByFilter(filter);
            //Execute the delete operation
            if(ticketToDelete != null)
            { 
                GetTicketCollection().DeleteOne(filter);
            }

        }

        public Ticket ReadTicket(Ticket ticket)
        {
            // Find the ticket by its _id (treated as a string)
            var filter = Builders<Ticket>.Filter.Eq("_id", ticket.TicketId);

            // Execute the find operation to retrieve the ticket
            return GetTicketCollection().Find(filter).FirstOrDefault();
        }


        public Ticket GetTicketByFilter(FilterDefinition<Ticket> filter)
        {
            // Assuming ticketCollection is your MongoDB collection
            var ticket = GetTicketCollection().Find(filter).FirstOrDefault();
            return ticket;
        }

        //kim
        public List<Ticket> GetAllTicketsFromUser(User user)
        {
            IMongoQueryable<Ticket> results = from tickets in GetTicketCollection().AsQueryable()
                                              where tickets.reportedBy.Username == user.Username
                                              select tickets;
            return results.ToList();
        }

        public List<Ticket> GetOpenTicketsFromUser(User user)
        {
            IMongoQueryable<Ticket> results = from tickets in GetTicketCollection().AsQueryable()
                                              where tickets.reportedBy.Username == user.Username
                                              where tickets.Status == Status.Pending || tickets.Status == Status.Opened
                                              select tickets;
            return results.ToList();
        }

        public List<Ticket> GetClosedTicketsFromUser(User user)
        {
            IMongoQueryable<Ticket> results = from tickets in GetTicketCollection().AsQueryable()
                                              where tickets.reportedBy.Username == user.Username
                                              where tickets.Status == Status.Closed || tickets.Status == Status.Resolved
                                              select tickets;
            return results.ToList();
        }

        public List<Ticket> GetAllTickets()
        {
            IMongoQueryable<Ticket> results = from tickets in GetTicketCollection().AsQueryable()
                                              select tickets;
            return results.ToList();
        }

        public List<Ticket> GetAllOpenTickets()
        {
            IMongoQueryable<Ticket> results = from tickets in GetTicketCollection().AsQueryable()
                                              where tickets.Status == Status.Pending || tickets.Status == Status.Opened
                                              select tickets;
            return results.ToList();
        }

        public List<Ticket> GetAllClosedTickets()
        {
            IMongoQueryable<Ticket> results = from tickets in GetTicketCollection().AsQueryable()
                                              where tickets.Status == Status.Closed || tickets.Status == Status.Resolved
                                              select tickets;
            return results.ToList();
        }

    }
}
