using INSPECTOR.Domain.Entities;
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
            _contextFactory = contextFactory;
        }


        public Teacher Get(int id)
        {
            using var db = _contextFactory.CreateDbContext();

            var teacher = db.Teachers.FirstOrDefault(x => x.Id == id);
            return teacher;
        }

        public List<Teacher> GetList(string name)
        {
            using var db = _contextFactory.CreateDbContext();

            var teachers = db.Teachers.Where(x => x.Name.Contains(name));
            return [.. teachers];
        }

        public void Save(Teacher teacher)
        {
            using var db = _contextFactory.CreateDbContext();

            var tmp = db.Teachers.FirstOrDefault(x => x.Id == teacher.Id);

            if (tmp == null)
            {
                db.Teachers.Add(teacher);
                db.SaveChanges();
            }
        }
        public void Update(Teacher teacher)
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


                db.SaveChanges();
            }
        }
        public void Delete(Teacher teacher)
        {
            using var db = _contextFactory.CreateDbContext();

            var tmp = db.Teachers.FirstOrDefault(x => x.Id == teacher.Id);
            if (tmp != null)
            {
                db.Teachers.Remove(tmp);
                db.SaveChanges();
            }
        }

        public List<Teacher> GetAll()
        {
            using var db = _contextFactory.CreateDbContext();

            return db.Teachers.ToList();
        }
    }
}
