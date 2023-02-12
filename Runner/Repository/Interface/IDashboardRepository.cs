using Runner.Models;

namespace Runner.Repository.Interface
{
    public interface IDashboardRepository
    {

        public Task<List<Race>> GetAllUserRaces();
        public Task<List<Club>> GetAllUserClubs();
        public Task<AppUser> GetUserById(string id);
        public Task<AppUser> GetUserByIdNoTracing(string id);
        public bool Update(AppUser user);
        public bool Save();
    }
}
