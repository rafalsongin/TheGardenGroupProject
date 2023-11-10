using Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ArchivedTicketDAO:BaseDao //Ghonim
    {
        private IMongoCollection<Ticket> closedTicketCollection;
        private IMongoDatabase secondaryDatabase;
        public ArchivedTicketDAO() 
        {
            secondaryDatabase = _client.GetDatabase("Database_Archive");
            closedTicketCollection = secondaryDatabase.GetCollection<Ticket>("ClosedTickets_Archive");
        }
        public void ArchiveTicket(Ticket ticket)
        {
            // Insert the ticket into the closed ticket collection
            closedTicketCollection.InsertOne(ticket);
        }

    }
}
