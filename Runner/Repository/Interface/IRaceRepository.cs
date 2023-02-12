using Runner.Models;

namespace Runner.Repository.Interface
{
    public interface IRaceRepository
    {
        Task<IEnumerable<Race>> GetAll();
        Task<Race> GetByIdAsync(int id);
        Task<Race> GetByIdAsyncNoTracking(int id);

        Task<IEnumerable<Race>> GetRacesByCity(string city);
        Task<Race> GetRacesByName(string name);
        Task<IEnumerable<Race>> GetByPerson(string Id);
        bool Add(Race race);
        bool Delete(Race race);
        bool Update(Race race);
        bool Save();
    }
}
