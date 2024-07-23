using fitness_db.Data;
using fitness_db.Models;
using fitness_progress_service.Interfaces;

namespace fitness_progress_service.Repositories
{
    public class UserNutritionRepository : IUserNutritionRepository
    {
        private readonly FitnessContext _fitnessCtx;
        public UserNutritionRepository(FitnessContext context)
        {
            _fitnessCtx = context;
        }

        public bool CreateUserNutrition(UserNutrition UserNutrition)
        {
            var user = _fitnessCtx.users.Where(u => u.UserID == UserNutrition.UserID).FirstOrDefault();
            var nutrition = _fitnessCtx.nutritions.Where(w => w.NutritionID == UserNutrition.NutritionID).FirstOrDefault();

            if (user == null || nutrition == null)
            {
                return false;
            }

            var userNutrition = new UserNutrition()
            {
                UserID = user.UserID,
                NutritionID = nutrition.NutritionID,
                UserNutritionDate = UserNutrition.UserNutritionDate,
                Qty = UserNutrition.Qty
            };

            _fitnessCtx.userNutritions.Add(userNutrition);
            var saved = _fitnessCtx.SaveChanges();

            return saved > 0 ? true : false;
        }

        public bool DeleteUserNutrition(UserNutrition UserNutrition)
        {
            _fitnessCtx.Remove(UserNutrition);
            var saved = _fitnessCtx.SaveChanges();

            return saved > 0 ? true : false;
        }

        public UserNutrition GetUserNutrition(int userNutritionId)
        {
            var userNutrition = _fitnessCtx.userNutritions.Where(uw => uw.UserNutritionID == userNutritionId).FirstOrDefault();
            if (userNutrition == null)
            {
                return null;
            }
            return userNutrition;
        }
    }
}
