using fitness_db.Models;
using fitness_progress_service.Dto.Req;
using fitness_progress_service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace fitness_progress_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserNutritionController : Controller
    {
        private readonly IUserNutritionRepository _userNutritionRep;
        private readonly IUserRepository _userRep;
        private readonly INutritionRepository _nutritionRep;
        public UserNutritionController(IUserNutritionRepository userNutritionRepossitory, IUserRepository userRep, INutritionRepository nutritionRep)
        {
            _userNutritionRep = userNutritionRepossitory;
            _userRep = userRep;
            _nutritionRep = nutritionRep;
        }

        [HttpPost]
        public IActionResult CreateUserNutrition([FromBody] ReqUserNutritionDto reqUserNutrition)
        {

            try
            {
                if (reqUserNutrition == null)
                    return BadRequest(new
                    {
                        status = "Failed",
                        message = "Request not valid"
                    });

                var checkUser = _userRep.GetUser(reqUserNutrition.UserID);
                if (checkUser == null)
                    return Ok(new
                    {
                        status = "Failed",
                        message = "User not found!",
                        data = checkUser
                    });

                var checkNutrition = _nutritionRep.GetNutrition(reqUserNutrition.NutritionID);
                if (checkNutrition == null)
                    return Ok(new
                    {
                        status = "Failed",
                        message = "Nutrition not found!",
                        data = checkNutrition
                    });

                //DateTimeOffset localDateTimeOffset = new DateTimeOffset(reqUserNutrition.UserNutritionDate, TimeZoneInfo.FindSystemTimeZoneById("Asia/Jakarta").BaseUtcOffset);
                var datetime = reqUserNutrition.UserNutritionDate;
                var userNutrition = new UserNutrition
                {
                    UserID = reqUserNutrition.UserID,
                    NutritionID = reqUserNutrition.NutritionID,
                    UserNutritionDate = datetime,
                    Qty = reqUserNutrition.Qty,
                };

                if (!_userNutritionRep.CreateUserNutrition(userNutrition))
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
                    message = "User Nutrition Successfully created",
                    data = userNutrition
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
                
            }
        }

        [HttpPut("{userNutritionId}")]
        public IActionResult UpdateUserNutrition(int userNutritionId, [FromBody] ReqUserNutritionDto reqUserNutrition)
        {
            try
            {
                if (reqUserNutrition == null)
                    return BadRequest(new
                    {
                        status = "Failed",
                        message = "Requset not valid"
                    });

                var isUserNutritionExist = _userNutritionRep.GetUserNutrition(userNutritionId);

                if (isUserNutritionExist == null)
                    return Ok(new
                    {
                        status = "Failed",
                        message = "User Nutrition not found!"
                    });

                //DateTimeOffset localDateTimeOffset = new DateTimeOffset(reqUserNutrition.UserNutritionDate, TimeZoneInfo.FindSystemTimeZoneById("Asia/Jakarta").BaseUtcOffset);
                var datetime = reqUserNutrition.UserNutritionDate;
                isUserNutritionExist.UserNutritionID = userNutritionId;
                isUserNutritionExist.Qty = reqUserNutrition.Qty;
                isUserNutritionExist.UserNutritionDate = datetime;

                var updatedProgress = _userNutritionRep.UpdateUserNutrition(isUserNutritionExist);
                if (updatedProgress == null)
                {
                    return StatusCode(500, new
                    {
                        status = "Failed",
                        message = "Something went wrong updating user nutrition"
                    });
                }

                return Ok(new
                {
                    status = "Success",
                    message = "User Nutrition Successfully updated",
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

        [HttpDelete("{userNutritionId}")]
        public IActionResult DeleteUserNutrition(int userNutritionId)
        {
            try
            {
                var isUserNutritionExist = _userNutritionRep.GetUserNutrition(userNutritionId);

                if (isUserNutritionExist == null)
                    return Ok(new
                    {
                        status = "Failed",
                        message = "User Nutrition not found!"
                    });

                if (!_userNutritionRep.DeleteUserNutrition(isUserNutritionExist))
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
                    message = "User Nutrition Successfully Deleted"
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
        public IActionResult GetUserNutritions()
        {
            try
            {
                var allUserNutritions = _userNutritionRep.GetUserNutritions();

                if (allUserNutritions.Count <= 0)
                {
                    return Ok(new
                    {
                        status = "Success",
                        message = "User Nutrition is empty!",
                        data = allUserNutritions
                    });
                }

                return Ok(new
                {
                    status = "Success",
                    message = "All User Nutrition Successfully fetched",
                    data = allUserNutritions
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

        [HttpGet("{userNutritionId}")]
        public IActionResult GetUserNutrition(int userNutritionId)
        {
            try
            {
                var userNutrition = _userNutritionRep.GetUserNutrition(userNutritionId);

                if (userNutrition == null)
                {
                    return Ok(new
                    {
                        status = "Failed",
                        message = "User Nutrition not found!",
                        data = userNutrition
                    });
                }

                return Ok(new
                {
                    status = "Success",
                    message = "User Nutrition Successfully fetched",
                    data = userNutrition
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
