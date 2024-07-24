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
                        status = "Failed",
                        message = "Requset not valid"
                    });

                var checkUser = _userRep.GetUser(reqUserWorkout.UserID);
                if (checkUser == null)
                    return Ok(new
                    {
                        status = "Failed",
                        message = "User not found!",
                        data = checkUser
                    });

                var checkWorkout = _workoutRep.GetWorkout(reqUserWorkout.WorkoutID);
                if (checkWorkout == null)
                    return Ok(new
                    {
                        status = "Failed",
                        message = "Workout not found!",
                        data = checkWorkout
                    });

                //DateTimeOffset localDateTimeOffset = new DateTimeOffset(reqUserWorkout.UserWorkoutDate, TimeZoneInfo.FindSystemTimeZoneById("Asia/Jakarta").BaseUtcOffset);
                var datetime = reqUserWorkout.UserWorkoutDate;
                var userWorkout = new UserWorkout
                {
                    UserID = reqUserWorkout.UserID,
                    WorkoutID = reqUserWorkout.WorkoutID,
                    WorkoutDuration = reqUserWorkout.WorkoutDuration,
                    UserWorkoutDate = datetime,
                };

                if (!_userWorkoutRep.CreateUserWorkout(userWorkout))
                {
                    return StatusCode(500, new
                    {
                        status = "Failed",
                        message = "Something went wrong while saving",
                    });
                }

                return Ok(new
                {
                    status = "Success",
                    message = "User Workout Successfully created",
                    data = userWorkout
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    status = "Failed",
                    message = e.Message,
                    excption = e.InnerException.Message
                });

            }
        }

        [HttpPut("{userWorkoutId}")]
        public IActionResult UpdateUserWorkout(int userWorkoutId, [FromBody] ReqUserWorkoutDto reqUserWorkout)
        {
            try
            {
                if (reqUserWorkout == null)
                    return BadRequest(new
                    {
                        status = "Failed",
                        message = "Requset not valid"
                    });

                var isUserWorkoutExist = _userWorkoutRep.GetUserWorkout(userWorkoutId);

                if (isUserWorkoutExist == null)
                    return Ok(new
                    {
                        status = "Failed",
                        message = "User Workout not found!"
                    });

                //DateTimeOffset localDateTimeOffset = new DateTimeOffset(reqUserWorkout.UserWorkoutDate, TimeZoneInfo.FindSystemTimeZoneById("Asia/Jakarta").BaseUtcOffset);
                var datetime = reqUserWorkout.UserWorkoutDate;
                isUserWorkoutExist.UserWorkoutID = userWorkoutId;
                isUserWorkoutExist.WorkoutDuration = reqUserWorkout.WorkoutDuration;
                isUserWorkoutExist.UserWorkoutDate = datetime;

                var updatedProgress = _userWorkoutRep.UpdateUserWorkout(isUserWorkoutExist);
                if (updatedProgress == null)
                {
                    return StatusCode(500, new
                    {
                        status = "Failed",
                        message = "Something went wrong updating user workout"
                    });
                }

                return Ok(new
                {
                    status = "Success",
                    message = "User Workout Successfully updated",
                    data = updatedProgress
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    status = "Failed",
                    message = e.Message,
                    excption = e.InnerException.Message
                });

            }
        }

        [HttpDelete("{userWorkoutId}")]
        public IActionResult DeleteUserWorkout(int userWorkoutId)
        {
            try
            {
                var isUserWorkoutExist = _userWorkoutRep.GetUserWorkout(userWorkoutId);

                if (isUserWorkoutExist == null)
                    return Ok(new
                    {
                        status = "Failed",
                        message = "User Workout not found!"
                    });

                if (!_userWorkoutRep.DeleteUserWorkout(isUserWorkoutExist))
                {
                    return StatusCode(500, new
                    {
                        status = "Failed",
                        message = "Something went wrong deleting user nutrition!"
                    });
                }

                return Ok(new
                {
                    status = "Success",
                    message = "User Workout Successfully Deleted"
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    status = "Failed",
                    message = e.Message,
                    excption = e.InnerException.Message
                });

            }
        }

        [HttpGet]
        public IActionResult GetUserWorkouts()
        {
            try
            {
                var allUserWorkouts = _userWorkoutRep.GetUserWorkouts();

                if (allUserWorkouts.Count <= 0)
                {
                    return Ok(new
                    {
                        status = "Failed",
                        message = "User Workout is empty!"
                    });
                }

                return Ok(new
                {
                    status = "Success",
                    message = "All User Workout Successfully fetched",
                    data = allUserWorkouts
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    status = "Failed",
                    message = e.Message,
                    excption = e.InnerException.Message
                });

            }
        }

        [HttpGet("{userWorkoutId}")]
        public IActionResult GetUserWorkout(int userWorkoutId)
        {
            try
            {
                var userWorkout = _userWorkoutRep.GetUserWorkout(userWorkoutId);

                if (userWorkout == null)
                {
                    return Ok(new
                    {
                        status = "Failed",
                        message = "User Workout not found!",
                        data = userWorkout
                    });
                }

                return Ok(new
                {
                    status = "Success",
                    message = "User Workout Successfully fetched",
                    data = userWorkout
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    status = "Failed",
                    message = e.Message,
                    excption = e.InnerException.Message
                });

            }
        }
    }
}
