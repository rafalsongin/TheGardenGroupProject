using Model;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TicketDao2 : BaseDao
    {
        //kim
        public List<Ticket> GetAllTicketsFromUser(User user)
        {
            IMongoQueryable<Ticket> results = from tickets in GetTicketCollection().AsQueryable()
                                              where tickets.reportedBy.Username == user.Username
                                              select tickets;
            return results.ToList();
        }

        public List<Ticket> GetOpenTicketsFromUser(User user)
        {
            IMongoQueryable<Ticket> results = from tickets in GetTicketCollection().AsQueryable()
                                              where tickets.reportedBy.Username == user.Username
                                              where tickets.Status == Status.Pending || tickets.Status == Status.Opened
                                              select tickets;
            return results.ToList();
        }

        public List<Ticket> GetClosedTicketsFromUser(User user)
        {
            IMongoQueryable<Ticket> results = from tickets in GetTicketCollection().AsQueryable()
                                              where tickets.reportedBy.Username == user.Username
                                              where tickets.Status == Status.Closed || tickets.Status == Status.Resolved
                                              select tickets;
            return results.ToList();
        }

        public List<Ticket> GetAllTickets()
        {
            IMongoQueryable<Ticket> results = from tickets in GetTicketCollection().AsQueryable()
                                              select tickets;
            return results.ToList();
        }

        public List<Ticket> GetAllOpenTickets()
        {
            IMongoQueryable<Ticket> results = from tickets in GetTicketCollection().AsQueryable()
                                              where tickets.Status == Status.Pending || tickets.Status == Status.Opened
                                              select tickets;
            return results.ToList();
        }

        public List<Ticket> GetAllClosedTickets()
        {
            IMongoQueryable<Ticket> results = from tickets in GetTicketCollection().AsQueryable()
                                              where tickets.Status == Status.Closed || tickets.Status == Status.Resolved
                                              select tickets;
            return results.ToList();
        }

    }


}
