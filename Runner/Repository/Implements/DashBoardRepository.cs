using Microsoft.EntityFrameworkCore;
using Runner.Data;
using Runner.Helpers;
using Runner.Models;
using Runner.Repository.Interface;

namespace Runner.Repository.Implements
{
    public class DashBoardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashBoardRepository(ApplicationDbContext context,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor; 
        }

        public async Task<List<Club>> GetAllUserClubs()
        {
            var currentuser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userclubs = _context.Clubs.Where(a => a.AppUser.Id == currentuser.ToString());
            return userclubs.ToList();
;        }

        public async Task<List<Race>> GetAllUserRaces()
        {
            var currentuser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userracess = _context.Races.Where(a => a.AppUser.Id == currentuser.ToString());
            return userracess.ToList();
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<AppUser> GetUserByIdNoTracing(string id)
        {
            return await _context.Users.Where(n => n.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }
        public bool Update(AppUser user)
        {
            _context.Users.Update(user);
            return Save();
        }
        public bool Save()
        {
           var saved= _context.SaveChanges();
            return saved>0?true:false;
        }
    }
}
