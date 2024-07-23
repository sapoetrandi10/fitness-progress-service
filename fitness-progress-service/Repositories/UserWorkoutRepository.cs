using fitness_db.Data;
using fitness_db.Models;
using fitness_progress_service.Interfaces;
using fitness_progress_service.Dto.Req;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fitness_progress_service.Repositories
{
    public class UserWorkoutRepository : IUserWorkoutRepossitory
    {
        private readonly FitnessContext _fitnessCtx;
        public UserWorkoutRepository(FitnessContext context)
        {
            _fitnessCtx = context;
        }
        public bool CreateUserWorkout(UserWorkout UserWorkout)
        {
            var user = _fitnessCtx.users.Where(u => u.UserID == UserWorkout.UserID).FirstOrDefault();
            var workout = _fitnessCtx.workouts.Where(w => w.WorkoutID == UserWorkout.WorkoutID).FirstOrDefault();

            if (user == null || workout == null)
            {
                return false;
            }

            var userWorkout = new UserWorkout()
            {
                UserID = user.UserID,
                WorkoutID = workout.WorkoutID,
                WorkoutDuration = UserWorkout.WorkoutDuration,
                UserWorkoutDate = UserWorkout.UserWorkoutDate,
            };

            _fitnessCtx.userWorkouts.Add(userWorkout);
            var saved = _fitnessCtx.SaveChanges();

            return saved > 0 ? true : false;
        }

        public bool DeleteUserWorkout(UserWorkout UserWorkout)
        {
            _fitnessCtx.Remove(UserWorkout);
            var saved = _fitnessCtx.SaveChanges();

            return saved > 0 ? true : false;
        }

        public UserWorkout GetUserWorkout(int userWorkoutId)
        {
            var userWorkout = _fitnessCtx.userWorkouts.Where(uw => uw.UserWorkoutID == userWorkoutId).FirstOrDefault();
            if (userWorkout == null)
            {
                return null;
            }
            return userWorkout;
        }
    }
}
