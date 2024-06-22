using INSPECTORV2.Domain.Entities;
using ClassLibrary.persistance;
using InspectorServicesInterfaces;
using Microsoft.EntityFrameworkCore;


namespace InspectorServices
{
    public class TeacherService : ITeacherService
    {
        private readonly IDbContextFactory<LibraryContext> _contextFactory;

        public TeacherService(IDbContextFactory<LibraryContext> contextFactory)
        {
            _contextFactory = _contextFactory;
        }

        public async Task<Teacher> Get(int id)
        {
            using var db = _contextFactory.CreateDbContext();
            var teacher = await db.Teachers.FirstOrDefaultAsync(x => x.Id == id);
            return teacher;
        }

        public async Task<List<Teacher>> GetList(string name)
        {
            using var db = _contextFactory.CreateDbContext();
            var teachers = db.Teachers.Where(x => x.Name.Contains(name));
            return await teachers.ToListAsync();
        }

        public async Task Save(Teacher teacher)
        {
            using var db = _contextFactory.CreateDbContext();
            var tmp = db.Teachers.FirstOrDefault(x => x.Id == teacher.Id);

            if (tmp == null)
            {
                db.Teachers.Add(teacher);
                await db.SaveChangesAsync();
            }
        }

        public async Task Update(Teacher teacher)
        {
            using var db = _contextFactory.CreateDbContext();
            var tmp = db.Teachers.FirstOrDefault(y => y.Id == teacher.Id);

            if (tmp != null)
            {
                tmp.Email = teacher.Email;
                tmp.Phone = teacher.Phone;
                tmp.Name = teacher.Name;
                tmp.Address = teacher.Address;
                tmp.Age = teacher.Age;
                tmp.Speialiation = teacher.Speialiation;

                await db.SaveChangesAsync();
            }
        }

        public async Task Delete(Teacher teacher)
        {
            using var db = _contextFactory.CreateDbContext();
            var tmp = db.Teachers.FirstOrDefault(x => x.Id == teacher.Id);
            if (tmp != null)
            {
                db.Teachers.Remove(tmp);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<Teacher>> GetAll()
        {
            using var db = _contextFactory.CreateDbContext();
            return [.. await db.Teachers.ToListAsync()];
        }
    }
}

