using API_University.Data;
using System;

namespace API_University.Repositories
{
    public interface IStudentRepository
    {
        Task<Student> Get(int id);

        int GetLastId();

        Task<IEnumerable<Student>> GetAll();
        
        Task<Student> Create(Student student);

        Task<Student> Update(Student student);
    }
}
