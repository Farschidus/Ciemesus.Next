using Ciemesus.Core.Data;
using FluentValidation.Results;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Ciemesus.Core.User
{
    public static class UserValidation
    {
        public static async Task<ValidationFailure> ValidateUserIdDoesExist(CiemesusDb db, string id, string propertyName)
        {
            var exists = await db.Users.AnyAsync(l => l.Id == id);

            return exists ? null : new ValidationFailure(propertyName, $"{propertyName}: {id} does not exist");
        }

        public static async Task<ValidationFailure> ValidateUserEmailDoesExist(CiemesusDb db, string email, string propertyName)
        {
            var exists = await FindUserByEmail(db, email);

            return !exists ? new ValidationFailure(propertyName, $"{propertyName}: {email} does not exist") : null;
        }

        public static async Task<ValidationFailure> ValidateUserEmailIsNotInUse(CiemesusDb db, string email, string propertyName)
        {
            var exists = await FindUserByEmail(db, email);

            return exists ? new ValidationFailure(propertyName, $"{propertyName}: {email} is already in use") : null;
        }

        public static async Task<ValidationFailure> ValidateUserEmailIsNotInUseByOtherUsers(CiemesusDb db, string email, string id, string propertyName)
        {
            var exists = await db.Users.AnyAsync(l => l.Email.ToLower() == email.ToLower() && l.Id != id);

            return exists ? new ValidationFailure(propertyName, $"{propertyName}: {email} is already in use") : null;
        }

        private static async Task<bool> FindUserByEmail(CiemesusDb db, string email)
        {
            return await db.Users.AnyAsync(l => l.Email.ToLower() == email.ToLower());
        }
    }
}
