using Runner.Models;

namespace Runner.Repository.Interface
{
    public interface IClubRepository
    {

        Task<IEnumerable<Club>> GetAll();
        Task<Club> GetByIdAsync(int id);
        Task<Club> GetByIdAsyncNoTracking(int id);
        
        Task<IEnumerable<Club>> GetClubByCity(string city);
        Task<Club> GetClubsByName(string name);
        Task<IEnumerable<Club>> GetByPerson(string Id);
        bool Add(Club club);
        bool Delete(Club club);
        bool Update(Club club);
        bool Save();

    }
}
