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

        [BsonElement ("Employee")]
        public string Assignedby { get; set; } // should by employee but I am waiting for the class to be created
        public string Email { get; set; }

        public User CreatedBy { get; set; }
       
        public void createConceptTicket(string title, Priority priority, string description, IncidentType incidentType, User user)
        {
            Subject = title;
            this.priority = priority;
            Description = description;
            Status = Status.Pending;
            DateReported = DateTime.Now;
            LastUpdated = DateTime.Now;
            IncidentType = incidentType;
            CreatedBy = user;
        }

    }
}
