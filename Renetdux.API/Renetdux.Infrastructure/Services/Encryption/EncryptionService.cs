using System;
using System.Collections.Generic;
using System.Text;

namespace Renetdux.Infrastructure.Services.Encryption
{
    public class EncryptionService : IEncryptionService
    {
        public bool ValidatePassword(string password1, string password2)
        {
            // TODO: Implement real encryption validation

            if (password1 == password2)
                return true;

            return false;
        }

        public string HashPassword(string password)
        {
            // TODO: Implement real hash

            return password;
        }
    }
}
