using fitness_progress_service.Interfaces;
using fitness_db.Models;
using fitness_progress_service.Dto.Req;
using Microsoft.AspNetCore.Mvc;

namespace fitness_progress_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressController : Controller
    {
        private readonly IProgressRepository _progressRep;
        public ProgressController(IProgressRepository progressRepository)
        {
            _progressRep = progressRepository;
        }

        [HttpPost]
        public IActionResult CreateProgress([FromBody] ReqProgressDto reqProgress)
        {
            if (reqProgress == null)
                return BadRequest(new
                {
                    status = "failed",
                    message = "Request not valid"
                });

            var progress = new Progress
            {
                UserID = reqProgress.UserID,
                Weight = reqProgress.Weight,
                ProgressDate = DateTime.Now
            };

            if (!_progressRep.CreateProgress(progress))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok(new
            {
                status = "succes",
                message = "Progress Succsessfuly created!"
            });
        }

        [HttpPut("{progressId}")]
        public IActionResult UpdateNutrition(int progressId, [FromBody] ReqProgressDto reqProgress)
        {
            if (reqProgress == null)
                return BadRequest(new
                {
                    status = "failed",
                    message = "Requset not valid"
                });

            var isProgressExist = _progressRep.GetProgress(progressId);

            if (isProgressExist == null)
                return NotFound(new
                {
                    status = "failed",
                    message = "Progress not found!"
                });


            isProgressExist.ProgressID = progressId;
            isProgressExist.Weight = reqProgress.Weight;

            var updatedProgress = _progressRep.UpdateProgress(isProgressExist);
            if (updatedProgress == null)
            {
                return StatusCode(500, new
                {
                    status = "failed",
                    message = "Something went wrong updating progress"
                });
            }

            return Ok(new
            {
                status = "success",
                message = "Progress Successfully updated",
                data = updatedProgress
            });
        }

        [HttpDelete("{progressId}")]
        public IActionResult DeleteProgress(int progressId)
        {
            var isProgressExist = _progressRep.GetProgress(progressId);

            if (isProgressExist == null)
                return NotFound(new
                {
                    status = "failed",
                    message = "Your Progress not found!"
                });

            if (!_progressRep.DeleteProgress(isProgressExist))
            {
                return BadRequest(new
                {
                    status = "failed",
                    message = "Something went wrong deleting progress!"
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
