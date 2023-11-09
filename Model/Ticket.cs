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

        [BsonElement("Subject")]
        public string Subject { get; set; }

        [BsonElement("Description")]
        [BsonIgnoreIfNull]
        public string Description { get; set; }        

        [BsonElement("ReportedOn")]
        public DateTime ReportedOn { get; set; }

        [BsonElement("DeadLine")]
        public DateTime Deadline { get; set; }

        [BsonElement("LastUpdated")]
        public DateTime LastUpdated { get; set; }

        private Priority priority;

        [BsonElement("Priority")]
        [BsonRepresentation(BsonType.String)]
        public Priority Priority {get;set;}

        private IncidentType selectedIncidentType;
        [BsonElement ("IncidentType")]
        [BsonRepresentation(BsonType.String)]// Store enum as string
        public IncidentType IncidentType { get;set;}

        [BsonElement("Status")]
        [BsonRepresentation(BsonType.String)]// Store enum as string
        public Status Status { get; set; }
        public bool IsClosed => Status == Status.Closed;

        [BsonElement("ReportedBy")]
        public string ReportedBy { get; set; }

        public Ticket()
        {

        }

        public Ticket(DateTime reportedOn, string subject, string description, IncidentType incidentType, string reportedBy, Priority priority, DateTime deadLine)
        {
            ReportedOn = reportedOn;
            Subject = subject;
            Description = description;
            IncidentType = incidentType;
            ReportedBy = reportedBy;
            Priority = priority;
            Deadline = deadLine;
            Status = Status.Opened;
            LastUpdated = DateTime.Now;
        }

        public void createConceptTicket(string subject, Priority priority, string description, IncidentType incidentType, User user)
        {
            Subject = subject;
            this.priority = priority;
            Description = description;
            Status = Status.Pending;
            ReportedOn = DateTime.Now;
            LastUpdated = DateTime.Now;
            IncidentType = incidentType;
            ReportedBy = user.Username;
        }
    }
}
