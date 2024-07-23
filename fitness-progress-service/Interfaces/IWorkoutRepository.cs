using fitness_db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fitness_progress_service.Interfaces
{
    public interface IWorkoutRepository
    {
        public bool CreateWorkout(Workout Workout);
        public Workout UpdateWorkout(Workout Workout);
        public bool DeleteWorkout(Workout Workout);
        public ICollection<Workout> GetWorkouts();
        public Workout GetWorkout(int workoutId);
    }
}
