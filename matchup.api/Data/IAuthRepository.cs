using System;
using System.Threading.Tasks;
using matchup.api.Models;
using Microsoft.EntityFrameworkCore;

namespace matchup.api.Data
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string username, string password);
         Task<bool> UserExists(string username);
    }

    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext ctx)
        {
            _context = ctx;
        }
         public async Task<User> Register(User user, string password)
         {
             byte[] passwordHash, passwordsalt;
             CreatePasswordHash(password, out passwordHash, out passwordsalt);

             user.PasswordHash = passwordHash;
             user.PasswordSalt = passwordsalt;

             await _context.Users.AddAsync(user);
             await _context.SaveChangesAsync();

             return user;
         }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordsalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordsalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            
        }

        public async Task<User> Login(string username, string password)
         {
             var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
             if (user == null) return null;
             if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) return null;
             return user;
            
         }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }

        public async Task<bool> UserExists(string username)
         {
             if (await _context.Users.AnyAsync(x => x.Username == username)) return true;
             return false;
         }
    }
}