using Microsoft.EntityFrameworkCore;
using Runner.Data;
using Runner.Models;
using Runner.Repository.Interface;

namespace Runner.Repository.Implements
{
    public class RaceRepository : IRaceRepository
    {
        private readonly ApplicationDbContext _context;

            public RaceRepository(ApplicationDbContext context)
            {
            _context = context;
             }
        public bool Add(Race race)
        {
            _context.Races.Add(race); 
            return Save();  
        }

        public bool Delete(Race race)
        {

            
            _context.Races.Remove(race);
            return Save();
        }

        public async Task<IEnumerable<Race>> GetAll()
        {
            return await _context.Races.ToListAsync();

        }

        public async Task<Race> GetByIdAsync(int id)
        {
            return await _context.Races.Include(n=>n.AppUser).Include(n=>n.Address).FirstOrDefaultAsync(n => n.Id == id);
        }
        public async Task<Race> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Races.Include(n => n.Address).AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<IEnumerable<Race>> GetByPerson(string Id)
        {
            return await _context.Races.Where(n=>n.AppUserId==Id).ToListAsync();
        }

        public async Task<IEnumerable<Race>> GetRacesByCity(string city)
        {
          
            return await _context.Races.Where(n => n.Address.City.Contains(city)).ToListAsync();
        }

        public async Task<Race> GetRacesByName(string name)
        {
            return await _context.Races.FirstOrDefaultAsync(n=>n.Name==name);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Race race)
        {

            _context.Races.Update(race);
            return Save();
        }
    }
}
