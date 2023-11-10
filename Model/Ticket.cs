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

        [BsonElement("Priority")]
        [BsonRepresentation(BsonType.String)]
        public Priority Priority {get;set;}

        [BsonElement ("IncidentType")]
        [BsonRepresentation(BsonType.String)] 
        public IncidentType IncidentType { get;set;}

        [BsonElement("Status")]
        [BsonRepresentation(BsonType.String)]
        public Status Status { get; set; }
        public bool IsClosed => Status == Status.Closed;//for UI purpose 

        [BsonElement("ReportedBy")]
        public string ReportedBy { get; set; }//based on userName

        public Ticket(DateTime reportedOn, string subject, string description, IncidentType incidentType,string reportedBy, Priority priority, DateTime deadLine)//Ghonim
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

        /*
            Made by Kim, is used to create a ticket by company employees and managers. 
        */
        public Ticket(string subject, Priority priority, string description, IncidentType incidentType, User user)
        {
            Subject = subject;
            Priority = priority;
            Description = description;
            Status = Status.Pending;
            ReportedOn = DateTime.Now;
            LastUpdated = DateTime.Now;
            IncidentType = incidentType;
            ReportedBy = user.Username;
        }
    }
}
