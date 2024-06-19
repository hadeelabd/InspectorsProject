using INSPECTOR.Domain.Entities;

namespace InspectorServicesInterfaces
{
    public interface IInspectorService
    {
        void Delete(Inspector inspector);
        Inspector Get(int id);
        List<Inspector> GetList(string name);
       Task <List<Inspector>> GetAll();
        void Save(Inspector inspector);
        void Update(Inspector inspector);
    }
}
