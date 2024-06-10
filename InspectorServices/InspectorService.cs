﻿using INSPECTOR.Domain.Entities;
using ClassLibrary.persistance;
using InspectorServicesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace InspectorServices
{
    public class InspectorService : IInspectorService
    {
        private readonly IDbContextFactory<LibraryContext> _contextFactory;

        public InspectorService(IDbContextFactory<LibraryContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }


        public Inspector Get(int id)
        {
            using var db = _contextFactory.CreateDbContext();

            var Inspectors = db.Inspectors.FirstOrDefault(x => x.Id == id);
            
            return Inspectors;
        }

        public List<Inspector> GetList(string name)
        {
            using var db = _contextFactory.CreateDbContext();

            var Inspectors = db.Inspectors.Where(x => x.Name.Contains(name));
            return [.. Inspectors];
        }

        public void Save(Inspector inspector)
        {
            using var db = _contextFactory.CreateDbContext();

            var tmp = db.Inspectors.FirstOrDefault(x => x.Id == inspector.Id);

            if (tmp == null)
            {
                db.Inspectors.Add(inspector);
                db.SaveChanges();
            }
        }
        public void Update(Inspector inspector)
        {
            using var db = _contextFactory.CreateDbContext();

            var tmp = db.Inspectors.FirstOrDefault(y => y.Id == inspector.Id);

            if (tmp != null)
            {
                tmp.Email = inspector.Email;
                tmp.Phone = inspector.Phone;
                tmp.Name = inspector.Name;
                tmp.Address = inspector.Address;
                tmp.Age = inspector.Age;
                tmp.Speialiation = inspector.Speialiation;



                db.SaveChanges();
            }
        }
        public void Delete(Inspector inspector)
        {
            using var db = _contextFactory.CreateDbContext();

            var tmp = db.Inspectors.FirstOrDefault(x => x.Id == inspector.Id);
            if (tmp != null)
            {
                db.Inspectors.Remove(tmp);
                db.SaveChanges();
            }
        }

        public List<Inspector> GetAll()
        {
            using var db = _contextFactory.CreateDbContext();

            return db.Inspectors.ToList();
        }
    }
}