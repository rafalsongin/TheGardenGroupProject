﻿using DAL;
using Model;
using MongoDB.Driver;

namespace Service
{
    public class TicketService
    {
        private readonly TicketDao ticketDao;
        //private TicketDao2 ticketDao2;
        public TicketService()
        {
            ticketDao = new TicketDao();
            //ticketDao2 = new TicketDao2();
        }
        public void CreateTicket(Ticket ticket)
        {
            ticketDao.CreateTicket(ticket);
        }

        public void UpdateTicket(FilterDefinition<Ticket> filter, Ticket updatedTicket) 
        {
             ticketDao.UpdateTicket(filter, updatedTicket);
        }

        public void DeleteTicket(FilterDefinition<Ticket> filter)
        {
            ticketDao.DeleteTicket(filter);
        }
        public Ticket ReadTicket(Ticket ticket) 
        {
            return ticketDao.ReadTicket(ticket);
        }

        public Ticket GetTicketByFilter(FilterDefinition<Ticket> filter)
        { 
            return ticketDao.GetTicketByFilter(filter);
        }

        //kim
        public List<Ticket> GetAllTicketsFromUser(User user)
        {
            return ticketDao.GetAllTicketsFromUser(user);
        }

        public List<Ticket> GetOpenTicketsFromUser(User user)
        {
            return ticketDao.GetOpenTicketsFromUser(user);
        }

        public List<Ticket> GetClosedTicketsFromUser(User user)
        {
            return ticketDao.GetClosedTicketsFromUser(user);
        }

        public List<Ticket> GetAllTickets()
        {
            return ticketDao.GetAllTickets();
        }

        public List<Ticket> GetAllOpenTickets()
        {
            return ticketDao.GetAllOpenTickets();
        }

        public List<Ticket> GetAllClosedTickets()
        {
            return ticketDao.GetAllClosedTickets();
        }
    }
}
