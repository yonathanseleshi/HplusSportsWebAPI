using System.Collections.Generic;
using System.Threading.Tasks;
using HPlusSportsAPI.Models;

namespace HPlusSportsAPI.Repositories.Interfaces
{
    public interface ISalespersonRepository
    {
        Task<Salesperson> Add(Salesperson item);

        IEnumerable<Salesperson> GetAll();

        Task<Salesperson> Find(int id);

        Task<Salesperson> Remove(int id);

        Task<Salesperson> Update(Salesperson item);

        Task<bool> Exists(int id);
    }
}