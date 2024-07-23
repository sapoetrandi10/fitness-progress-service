using fitness_db.Models;
using fitness_progress_service.Dto.Req;
using fitness_progress_service.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
                        status = "failed",
                        message = "Requset not valid"
                    });

                var checkUser = _userRep.GetUser(reqUserNutrition.UserID);
                if (checkUser == null)
                    return BadRequest(new
                    {
                        status = "failed",
                        message = "User not found!"
                    });

                var checkNutrition = _nutritionRep.GetNutrition(reqUserNutrition.NutritionID);
                if (checkNutrition == null)
                    return BadRequest(new
                    {
                        status = "failed",
                        message = "Nutrition not found!"
                    });

                var userNutrition = new UserNutrition
                {
                    UserID = reqUserNutrition.UserID,
                    NutritionID = reqUserNutrition.NutritionID,
                    UserNutritionDate = reqUserNutrition.UserNutritionDate,
                    Qty = reqUserNutrition.Qty,
                };

                if (!_userNutritionRep.CreateUserNutrition(userNutrition))
                {
                    ModelState.AddModelError("", "Something went wrong while saving");
                    return StatusCode(500, ModelState);
                }

                return Ok(new
                {
                    status = "success",
                    message = "User Nutrition Successfully created"
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

        [HttpDelete("{userNutritionId}")]
        public IActionResult DeleteUserNutrition(int userNutritionId)
        {
            var isUserNutritionExist = _userNutritionRep.GetUserNutrition(userNutritionId);

            if (isUserNutritionExist == null)
                return NotFound(new
                {
                    status = "failed",
                    message = "User Nutrition not found!"
                });

            if (!_userNutritionRep.DeleteUserNutrition(isUserNutritionExist))
            {
                return BadRequest(new
                {
                    status = "failed",
                    message = "Something went wrong deleting user nutrition!"
                });
            }

            return Ok(new
            {
                status = "success",
                message = "User Nutrition Successfully Deleted"
            });
        }
    }
}
