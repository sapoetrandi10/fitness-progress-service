using fitness_db.Models;

namespace fitness_progress_service.Interfaces
{
    public interface IUserNutritionRepository
    {
        public bool CreateUserNutrition(UserNutrition UserNutrition);
        //public Nutrition UpdateUserNutrition(Nutrition Nutrition);
        public bool DeleteUserNutrition(UserNutrition UserNutrition);
        //public ICollection<Nutrition> GetUserNutritions();
        public UserNutrition GetUserNutrition(int userNutritionId);
        //public Nutrition GetUserNutritionPeriod(string workoutPeriod);
    }
}
