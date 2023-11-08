using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Model;
using System.ComponentModel;

namespace DAL
{
    public class TicketDao
    {
        private MongoClient client;
        private IMongoCollection<Ticket> ticketCollection;
        private IMongoDatabase database;
        public TicketDao()
        {
            client = new MongoClient("mongodb+srv://dbUser:test123@thegardengroupserverles.vovxxor.mongodb.net/");
            database = client.GetDatabase("TheGardenGroup");//geting the database
            ticketCollection = database.GetCollection<Ticket>("IncidentTicket");//the table that we want to work on
        }

        public void CreateTicket(Ticket ticket)
        {

            ticketCollection.InsertOne(ticket);
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
                ticketCollection.ReplaceOne(filter, existingTicket);
            }
        }

        public void DeleteTicket(FilterDefinition<Ticket> filter)
        {
            // FilterDefinition<Ticket> deleteDefinitions = GetTicketByFilter(new );

            var ticketToDelete = GetTicketByFilter(filter);
            //Execute the delete operation
            if(ticketToDelete != null)
            { 
                ticketCollection.DeleteOne(filter);
            }

        }

        public Ticket ReadTicket(Ticket ticket)
        {
            // Find the ticket by its _id (treated as a string)
            var filter = Builders<Ticket>.Filter.Eq("_id", ticket.TicketId);

            // Execute the find operation to retrieve the ticket
            return ticketCollection.Find(filter).FirstOrDefault();
        }

        public List<Ticket> GetAllTickets()
        {
            List<Ticket> tickets = ticketCollection.Find(_ => true).ToList();

            return tickets;
        }


        public Ticket GetTicketByFilter(FilterDefinition<Ticket> filter)
        {
            // Assuming ticketCollection is your MongoDB collection
            var ticket = ticketCollection.Find(filter).FirstOrDefault();
            return ticket;
        }
    }
}
