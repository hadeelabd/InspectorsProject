

using INSPECTOR.Domain.Entities;

namespace InspectorServicesInterfaces
{
    public interface ITeacherService
    {
        void Delete(Teacher teacher);
        Teacher Get(int id);
        List<Teacher> GetList(string name);
       List<Teacher> GetAll();
        void Save(Teacher teacher);
        void Update(Teacher teacher);
    }
}
