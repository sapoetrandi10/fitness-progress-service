using fitness_db.Models;

namespace fitness_progress_service.Interfaces
{
    public interface IUserNutritionRepository
    {
        public bool CreateUserNutrition(UserNutrition UserNutrition);
        public UserNutrition UpdateUserNutrition(UserNutrition Nutrition);
        public bool DeleteUserNutrition(UserNutrition UserNutrition);
        public ICollection<UserNutrition> GetUserNutritions();
        public UserNutrition GetUserNutrition(int userNutritionId);
        //public Nutrition GetUserNutritionPeriod(string workoutPeriod);
    }
}
