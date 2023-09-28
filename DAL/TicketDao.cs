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
            ticketCollection.InsertOne(ticket);
        }



        public bool UpdateTicket(Ticket ticket, Dictionary<string, Ticket> updates)//dictionary containing the fields to update and their new values.
        {
            FilterDefinition<Ticket> filter = CreateIdFilter(ticket);

            var updateDefinitions = new List<UpdateDefinition<Ticket>>();//Dynamic update

            foreach (KeyValuePair<string, Ticket> field in updates)
            {
                var updateField = Builders<Ticket>.Update.Set(field.Key, field.Value);
                updateDefinitions.Add(updateField);
            }
            var combinedUpdate = Builders<Ticket>.Update.Combine(updateDefinitions);
            var result = ticketCollection.UpdateOne(filter, combinedUpdate);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public bool DeleteTicket(Ticket ticket)
        {
            FilterDefinition<Ticket> deleteDefinitions = CreateIdFilter(ticket);

            //Execute the delete operation
            DeleteResult result = ticketCollection.DeleteOne(deleteDefinitions);

            // Check if the delete was successful
            return result.IsAcknowledged && result.DeletedCount > 0;

        }

        public Ticket ReadTicket(Ticket ticket)
        {
            // Find the ticket by its _id (treated as a string)
            var filter = Builders<Ticket>.Filter.Eq("_id", ticket.TicketId);

            // Execute the find operation to retrieve the ticket
            return ticketCollection.Find(filter).FirstOrDefault();
        }

        private FilterDefinition<Ticket> CreateIdFilter(Ticket ticket)
        {
            return Builders<Ticket>.Filter.Eq("_id", ticket.TicketId);
        }
    }
}
