using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
namespace Model
{
    public class Ticket
    {
        [BsonId]
        public ObjectId TicketId { get; set; }

        [BsonElement("Description")]
        [BsonIgnoreIfNull]
        public string Description { get; set; }


        [BsonElement("Subject")]
        public string Subject { get; set; }

        [BsonElement("DataReported")]
        public DateTime DateReported { get; set; }

        [BsonElement("DeadLine")]
        public DateTime Deadline { get; set; }

        [BsonElement("LastUpdated")]
        public DateTime LastUpdated { get; set; }

        [BsonElement("Priority")]
        public Priority Priority {get;set;}
        // try to compare here and create a backup field 

        [BsonElement("Status")]
        [BsonIgnoreIfDefault]
        public Status Status { get; set; }

        [BsonElement ("IncidentType")]
        public IncidentType IncidentType { get; set; }

        [BsonElement ("Employee")]
        public string Assignedby { get; set; } // should by employee but I am waiting for the class to be created
       


    }
}
