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
        public DateTime ReportedOn { get; set; }

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

        private Status status;
        [BsonElement("Status")]
        [BsonRepresentation (BsonType.String)]
        public Status Status { get { return status; } set { status = value; } }

        [BsonElement ("Employee")]
        public string Assignedby { get; set; } // should by employee but I am waiting for the class to be created
        public string Email { get; set; }

        [BsonElement ("ReportedBy")]
        public User reportedBy { get; set; }

        public User assignedTo { get; set; }
       
        public void createConceptTicket(string subject, Priority priority, string description, IncidentType incidentType, User user)
        {
            Subject = subject;
            this.priority = priority;
            Description = description;
            Status = Status.Pending;
            ReportedOn = DateTime.Now;
            LastUpdated = DateTime.Now;
            IncidentType = incidentType;
            reportedBy = user;
        }

    }
}
