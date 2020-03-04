using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;


namespace Modelos
{
    public class Protector
    {
        private static readonly byte[] salt = Encoding.Unicode.GetBytes("7BANANAS");

        public static (string, string) ObtenerEncryptedPassword(string password, string username)
        {
            // generate a random salt
            var rng = RandomNumberGenerator.Create();
            var saltBytes = new byte[16];
            rng.GetBytes(saltBytes);
            var saltText = Convert.ToBase64String(saltBytes);
            // generate the salted and hashed password 
            var saltedhashedPassword = SaltAndHashPassword(
              password, saltText);

            return (saltedhashedPassword, saltText);

        }

        public static string SaltAndHashPassword(
          string password, string salt)
        {
            var sha = SHA256.Create();
            var saltedPassword = password + salt;
            return Convert.ToBase64String(
              sha.ComputeHash(Encoding.Unicode.GetBytes(
              saltedPassword)));
        }
    }
}
