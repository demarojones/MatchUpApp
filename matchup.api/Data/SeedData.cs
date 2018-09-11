using System.Collections.Generic;
using matchup.api.Entities;
using Newtonsoft.Json;

namespace matchup.api.Data
{
    public class SeedData
    {
        private readonly DataContext _context;

        public SeedData(DataContext context)
        {
            this._context = context;
        }

        public void SeedUsers() {
            var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
            var users = JsonConvert.DeserializeObject<List<User>>(userData);

            foreach (var user in users)
            {
                byte[] passwdhash, passwdsalt;
                CreatePasswordHash("password", out passwdhash, out passwdsalt);

                user.PasswordHash = passwdhash;
                user.PasswordSalt = passwdsalt;
                user.Username = user.Username.ToLower();

                _context.Users.Add(user);
            }

            _context.SaveChanges();
            
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordsalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordsalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}