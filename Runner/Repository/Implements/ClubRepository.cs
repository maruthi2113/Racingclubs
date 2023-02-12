using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using Runner.Data;
using Runner.Models;
using Runner.Repository.Interface;

namespace Runner.Repository.Implements
{
    public class ClubRepository : IClubRepository
    {
        private readonly ApplicationDbContext _context;

        public ClubRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Club club)
        {
            _context.Clubs.Add(club);
           
            return Save();
        }

        public bool Delete(Club club)
        {
            _context.Clubs.Remove(club);

            return Save();
        }

        public async Task<IEnumerable<Club>> GetAll()
        {
            return await  _context.Clubs.ToListAsync();
        }
        public async Task<IEnumerable<Club>> GetByPerson(string Id)
        {
            return await _context.Clubs.Where(n=>n.AppUserId==Id).ToListAsync();
        }
        public async Task<Club> GetByIdAsync(int id)
        {
            return await _context.Clubs.Include(n => n.AppUser).Include(n=>n.Address).FirstOrDefaultAsync(n=>n.Id==id);
            //return await _context.Clubs.FirstOrDefaultAsync(n => n.Id == id);
           // return await _context.Clubs.Include(n=>n.Address).FirstOrDefaultAsync(n => n.Id == id);
        }
        public async Task<Club> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Clubs.Include(n => n.Address).AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<IEnumerable<Club>> GetClubByCity(string city)
        {
            return await _context.Clubs.Where(n => n.Address.City.Contains(city)).ToListAsync();
        }
        
        public async Task<Club> GetClubsByName(string name)
        {
            return await _context.Clubs.FirstOrDefaultAsync(s=>s.Name==name);
            //return await _context.Clubs.FindAsync(s=>s.Name==name);
        }
        public bool Save()
        {
           var saved= _context.SaveChanges();
            return saved > 0 ? true : false; 
        }

        public bool Update(Club club)
        {
            _context.Clubs.Update(club);
            return Save();
        }

  
    }
}
