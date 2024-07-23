using fitness_db.Data;
using fitness_db.Models;
using fitness_progress_service.Dto.Req;
using fitness_progress_service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fitness_progress_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserWorkoutController : Controller
    {
        private readonly IUserWorkoutRepossitory _userWorkoutRep;
        private readonly IUserRepository _userRep;
        private readonly IWorkoutRepository _workoutRep;
        public UserWorkoutController(IUserWorkoutRepossitory userWorkoutRepossitory, IUserRepository userRep, IWorkoutRepository workoutRep)
        {
            _userWorkoutRep = userWorkoutRepossitory;
            _userRep = userRep;
            _workoutRep = workoutRep;

        }

        [HttpPost]
        public IActionResult CreateUserWorkout([FromBody] ReqUserWorkoutDto reqUserWorkout)
        {

            try
            {
                if (reqUserWorkout == null)
                    return BadRequest(new
                    {
                        status = "failed",
                        message = "Requset not valid"
                    });

                var checkUser = _userRep.GetUser(reqUserWorkout.UserID);
                if (checkUser == null)
                    return BadRequest(new
                    {
                        status = "failed",
                        message = "User not found!"
                    });

                var checkWorkout = _workoutRep.GetWorkout(reqUserWorkout.WorkoutID);
                if (checkWorkout == null)
                    return BadRequest(new
                    {
                        status = "failed",
                        message = "Workout not found!"
                    });

                var userWorkout = new UserWorkout
                {
                    UserID = reqUserWorkout.UserID,
                    WorkoutID = reqUserWorkout.WorkoutID,
                    WorkoutDuration = reqUserWorkout.WorkoutDuration,
                    UserWorkoutDate = reqUserWorkout.UserWorkoutDate,
                };

                if (!_userWorkoutRep.CreateUserWorkout(userWorkout))
                {
                    ModelState.AddModelError("", "Something went wrong while saving");
                    return StatusCode(500, ModelState);
                }

                return Ok(new
                {
                    status = "success",
                    message = "User Workout Successfully created"
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    status = "failed",
                    message = e.Message,
                    excption = e.InnerException.Message
                });
                throw;
            }
        }

        [HttpDelete("{userWorkoutId}")]
        public IActionResult DeleteUserWorkout(int userWorkoutId)
        {
            var isUserWorkoutExist = _userWorkoutRep.GetUserWorkout(userWorkoutId);

            if (isUserWorkoutExist == null)
                return NotFound(new
                {
                    status = "failed",
                    message = "User Workout not found!"
                });

            if (!_userWorkoutRep.DeleteUserWorkout(isUserWorkoutExist))
            {
                return BadRequest(new
                {
                    status = "failed",
                    message = "Something went wrong deleting user workout!"
                });
            }

            return Ok(new
            {
                status = "success",
                message = "User Workout Successfully Deleted"
            });
        }
    }
}
