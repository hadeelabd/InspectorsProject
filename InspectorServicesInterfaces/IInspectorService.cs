using INSPECTORV2.Domain.Entities;

namespace InspectorServicesInterfaces
{
    public interface IInspectorService
    {
      // void Delete(Inspector inspector);
       // Inspector Get(int id);
       // List<Inspector> GetList(string name);
     
        Task Delete(Inspector inspector);
        Task<Inspector> Get(int id);
        Task<List<Inspector>> GetList(string name);
        Task <List<Inspector>> GetAll();

        Task Save(Inspector inspector);
        Task Update(Inspector inspector);
        //  void Save(Inspector inspector);
        // void Update(Inspector inspector);
    }
}
