using System.Collections.Generic;
using System.Threading.Tasks;
using matchup.api.Entities;
using Microsoft.EntityFrameworkCore;

namespace matchup.api.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _ctx;

        public DatingRepository(DataContext context)
        {
            this._ctx = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _ctx.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _ctx.Remove(entity);
        }

        public async Task<User> GetUser(int userId)
        {
            var user = await _ctx.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == userId);

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _ctx.Users.Include(p => p.Photos).ToListAsync();
            return users;
        }

        public async Task<bool> SaveAll()
        {
            return await _ctx.SaveChangesAsync() > 0;
        }
    }
}