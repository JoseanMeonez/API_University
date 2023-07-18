using API_University.Data;

namespace API_University.Repositories
{
    public interface IClassRepository
    {
        Task<Class> Get(int id);

        Task<IEnumerable<Class>> GetAll();
        Task<Class> Create(Class classes);

        Task<Class> Update(Class classes);
    }
}
