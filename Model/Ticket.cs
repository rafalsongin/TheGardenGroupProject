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

        [BsonElement("ReportedOn")]
        public DateTime DateReported { get; set; }

        [BsonElement("DeadLine")]
        public DateTime Deadline { get; set; }

        [BsonElement("LastUpdated")]
        public DateTime LastUpdated { get; set; }

        private Priority priority;

        [BsonElement("Priority")]
        [BsonRepresentation(BsonType.String)]
        public Priority Priority {get
            {
                return priority;
                
            }
            set
            { 
                priority = value;
            }
        }

        private IncidentType selectedIncidentType;
        [BsonElement ("IncidentType")]
        [BsonRepresentation(BsonType.String)]// Store enum as string
        public IncidentType IncidentType { get
            {
                return selectedIncidentType;
            } set
            {
                selectedIncidentType = value;
            } 
        }

        [BsonElement("Status")]
        [BsonIgnoreIfDefault]
        public Status Status { get; set; }
        public string Email { get; set; }
       
        [BsonElement ("ReportedBy")]
        [BsonIgnoreIfDefault]
        public string Assignedby { get; set; } // should by employee but I am waiting for the class to be created

    }
}
