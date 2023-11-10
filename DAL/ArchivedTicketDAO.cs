using Model;
using MongoDB.Driver;

namespace DAL
{
    public class ArchivedTicketDao : BaseDao //Ghonim
    {
        private readonly IMongoCollection<Ticket> _closedTicketCollection;

        public ArchivedTicketDao()
        {
            _closedTicketCollection = GetArchivedTicketCollection();
        }

        public void ArchiveTicket(Ticket ticket)
        {
            // Insert the ticket into the closed ticket collection
            _closedTicketCollection.InsertOne(ticket);
        }
    }
}