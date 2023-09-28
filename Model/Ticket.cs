using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
namespace Model
{
    public class Ticket
    {
        [BsonElement("_id")]
        public string TicketId { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }


        [BsonElement("Subject")]
        public string subject { get; set; }

        [BsonElement("DataAdded")]
        public DateTime DateAdded { get; set; }

        [BsonElement("DeadLine")]
        public DateTime Deadline { get; set; }

        [BsonElement("LastUpdated")]
        public DateTime LastUpdated { get; set; }

        [BsonElement("Priority")]
        public Priority Priority { get; set; }

        [BsonElement("Status")]
        public Status Status { get; set; }


    }
}
