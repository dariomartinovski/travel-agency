﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyApplication.Domain.Model;
using TravelAgencyApplication.Repository.Interface;
using TravelAgencyApplication.Web.Data;

namespace TravelAgencyApplication.Repository.Implementation
{
    public class ReservationRepository: IReservationRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Reservation> entities;
        string errorMessage = string.Empty;

        public ReservationRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Reservation>();
        }
        public IEnumerable<Reservation> GetAll()
        {
            return entities
                .Include(t => t.User)
                .Include(t => t.TravelPackage)
                .ToList();
        }

        public Reservation Get(Guid? id)
        {
            return entities
                .Include(t => t.User)
                .Include(t => t.TravelPackage)
                .SingleOrDefault(s => s.Id == id);
        }
        public void Insert(Reservation entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(Reservation entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(Reservation entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}