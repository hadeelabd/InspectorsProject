using INSPECTORV2.Domain.Entities;

namespace InspectorServicesInterfacess
{
    public interface ITeacherService
    {
        Task Delete(Teacher teacher);
        Task<Teacher> Get(int id);
        Task<List<Teacher>> GetList(string name);
        Task<List<Teacher>> GetAll();
        Task Save(Teacher teacher);
        Task Update(Teacher teacher);
    }
}

