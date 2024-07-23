using fitness_db.Data;
using fitness_progress_service.Interfaces;
using fitness_db.Models;
using fitness_progress_service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace fitness_progress_service.Repositories
{
    public class ProgressRepository : IProgressRepository
    {
        private FitnessContext _fitnessCtx;
        public ProgressRepository(FitnessContext context)
        {
            _fitnessCtx = context;
        }

        public bool CreateProgress(Progress progress)
        {
            //hitung nutrisi
            var userNutri = _fitnessCtx.userNutritions.Where(un => un.UserID == progress.UserID && un.UserNutritionDate.Date == progress.ProgressDate.Date).ToList();
            if (userNutri.Count > 0)
            {
                foreach (var un in userNutri)
                {
                    var nutri = _fitnessCtx.nutritions.Where(n => n.NutritionID == un.NutritionID).ToList();
                    foreach (var n in nutri)
                    {
                        progress.CaloriesConsumed += un.Qty * n.Calories;
                    }
                }
            }

            //hitung kalori
            var userWorkout = _fitnessCtx.userWorkouts.Where(uw => uw.UserID == progress.UserID && uw.UserWorkoutDate.Date == progress.ProgressDate.Date).ToList();
            if (userWorkout.Count > 0)
            {
                foreach (var uw in userWorkout)
                {
                    var workout = _fitnessCtx.workouts.Where(w => w.WorkoutID == uw.WorkoutID).ToList();
                    foreach (var w in workout)
                    {
                        progress.CaloriesBurned += uw.WorkoutDuration / w.Duration * w.CaloriesBurned;
                    }
                }
            }

            //var userNutrition = new UserNutrition()
            //{
            //    UserID = user.UserID,
            //    NutritionID = nutrition.NutritionID,
            //    UserNutritionDate = UserNutrition.UserNutritionDate,
            //    Qty = UserNutrition.Qty
            //};

            _fitnessCtx.progresses.Add(progress);
            var saved = _fitnessCtx.SaveChanges();

            return saved > 0 ? true : false;
        }

        public Progress UpdateProgress(Progress Progress)
        {
            var updated = _fitnessCtx.SaveChanges();

            if (updated < 0)
            {
                return Progress = null;
            }

            return Progress;
        }

        public bool DeleteProgress(Progress progress)
        {
            _fitnessCtx.Remove(progress);
            var saved = _fitnessCtx.SaveChanges();

            return saved > 0 ? true : false;
        }

        public Progress GetProgress(int progressId)
        {
            var progress = _fitnessCtx.progresses.Where(p => p.ProgressID == progressId).FirstOrDefault();
            if (progress == null)
            {
                return null;
            }
            return progress;
        }

    }
}
