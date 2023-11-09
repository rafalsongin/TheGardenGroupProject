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
        public Ticket(DateTime reportedOn, string subject, string description, IncidentType incidentType, string reportedBy, Priority priority, DateTime deadLine)
        {
            this.ReportedOn = reportedOn;
            this.Subject = subject;
            this.Description = description;
            this.IncidentType = incidentType;
            this.ReportedBy = reportedBy;
            this.Priority = priority;
            this.Deadline = deadLine;
            Status = Status.Opened;
        }
    }
}
