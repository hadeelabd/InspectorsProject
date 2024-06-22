using INSPECTORV2.Domain.Entities;
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

        public async Task<Inspector> Get(int id)
        {
            using var db = _contextFactory.CreateDbContext();

            var inspector = await db.Inspectors.FirstOrDefaultAsync(x => x.Id == id);
            return inspector;
        }

        public async Task<List<Inspector>> GetList(string name)
        {
            using var db = _contextFactory.CreateDbContext();

            var inspectors = db.Inspectors.Where(x => x.Name.Contains(name));
            return await inspectors.ToListAsync();
        }

        public async Task Save(Inspector inspector)
        {
            using var db = _contextFactory.CreateDbContext();

            var tmp = db.Inspectors.FirstOrDefault(x => x.Id == inspector.Id);

            if (tmp == null)
            {
                db.Inspectors.Add(inspector);
                await db.SaveChangesAsync();
            }
        }
        public async Task Update(Inspector inspector)
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

            await db.SaveChangesAsync();
            }
        }
        public async Task Delete(Inspector inspector)
        {
            using var db = _contextFactory.CreateDbContext();

            var tmp = db.Inspectors.FirstOrDefault(x => x.Id == inspector.Id);
            if (tmp != null)
            {
                db.Inspectors.Remove(tmp);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<Inspector>> GetAll()
        {
            using var db = _contextFactory.CreateDbContext();

          return [.. await db.Inspectors.ToListAsync()];
    }
    }
}
