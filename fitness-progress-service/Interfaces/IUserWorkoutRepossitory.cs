using fitness_db.Models;
using fitness_progress_service.Dto.Req;

namespace fitness_progress_service.Interfaces
{
    public interface IUserWorkoutRepossitory
    {
        public bool CreateUserWorkout(UserWorkout UserWorkout);
        //public Workout UpdateUserWorkout(Workout Workout);
        public bool DeleteUserWorkout(UserWorkout UserWorkout);
        //public ICollection<Workout> GetUserWorkouts();
        public UserWorkout GetUserWorkout(int userWorkoutId);
        //public Workout GetUserWorkoutPeriod(string workoutPeriod);
    }
}
