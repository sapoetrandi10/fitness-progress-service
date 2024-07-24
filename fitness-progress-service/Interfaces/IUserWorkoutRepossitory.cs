using fitness_db.Models;
using fitness_progress_service.Dto.Req;

namespace fitness_progress_service.Interfaces
{
    public interface IUserWorkoutRepossitory
    {
        public bool CreateUserWorkout(UserWorkout UserWorkout);
        public UserWorkout UpdateUserWorkout(UserWorkout UserWorkout);
        public bool DeleteUserWorkout(UserWorkout UserWorkout);
        public ICollection<UserWorkout> GetUserWorkouts();
        public UserWorkout GetUserWorkout(int userWorkoutId);
        //public Workout GetUserWorkoutPeriod(string workoutPeriod);
    }
}
