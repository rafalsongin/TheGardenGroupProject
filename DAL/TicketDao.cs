using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Model;

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
            string stringTicket = ticket.Priority.ToString();

            ticketCollection.InsertOne(ticket);
        }



        /* public bool UpdateTicket(Ticket ticket, Dictionary<string, Ticket> updates)//dictionary containing the fields to update and their new values.
         {
             FilterDefinition<Ticket> filter = FilterByID(ticket);

             var updateDefinitions = new List<UpdateDefinition<Ticket>>();//Dynamic update

             foreach (KeyValuePair<string, Ticket> field in updates)
             {
                 var updateField = Builders<Ticket>.Update.Set(field.Key, field.Value);
                 updateDefinitions.Add(updateField);
             }
             var combinedUpdate = Builders<Ticket>.Update.Combine(updateDefinitions);
             var result = ticketCollection.UpdateOne(filter, combinedUpdate);

             return result.IsAcknowledged && result.ModifiedCount > 0;
         }*/
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

        /*public FilterDefinition<Ticket> FilterByID(Ticket ticket)
        {
            return Builders<Ticket>.Filter.Eq("_id", ticket.TicketId);
        }*/

        public Ticket GetTicketByFilter(FilterDefinition<Ticket> filter)
        {
            // Assuming ticketCollection is your MongoDB collection
            var ticket = ticketCollection.Find(filter).FirstOrDefault();
            return ticket;
        }
    }
}
