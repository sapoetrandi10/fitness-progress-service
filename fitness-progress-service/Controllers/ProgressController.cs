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
            try
            {
                if (reqProgress == null)
                    return BadRequest(new
                    {
                        status = "Failed",
                        message = "Request not valid"
                    });

                //DateTimeOffset localDateTimeOffset = new DateTimeOffset(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Asia/Jakarta").BaseUtcOffset);
                var datetime = DateTime.UtcNow;
                var progress = new Progress
                {
                    UserID = reqProgress.UserID,
                    Weight = reqProgress.Weight,
                    ProgressDate = datetime
                };

                if (!_progressRep.CreateProgress(progress))
                {
                    return StatusCode(500, new
                    {
                        status = "Success",
                        message = "Something went wrong while saving",
                    }); ;
                }

                return Ok(new
                {
                    status = "succes",
                    message = "Progress Succsessfuly created!",
                    data = progress
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

        [HttpPut("{progressId}")]
        public IActionResult UpdateProgress(int progressId, [FromBody] ReqProgressDto reqProgress)
        {
            try
            {
                if (reqProgress == null)
                    return BadRequest(new
                    {
                        status = "Failed",
                        message = "Requset not valid"
                    });

                var isProgressExist = _progressRep.GetProgress(progressId);

                if (isProgressExist == null)
                    return Ok(new
                    {
                        status = "Failed",
                        message = "Your Progress not found!"
                    });


                isProgressExist.ProgressID = progressId;
                isProgressExist.Weight = reqProgress.Weight;

                var updatedProgress = _progressRep.UpdateProgress(isProgressExist);
                if (updatedProgress == null)
                {
                    return StatusCode(500, new
                    {
                        status = "Failed",
                        message = "Something went wrong updating progress"
                    });
                }

                return Ok(new
                {
                    status = "Success",
                    message = "Progress Successfully updated",
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

        [HttpDelete("{progressId}")]
        public IActionResult DeleteProgress(int progressId)
        {
            try
            {
                var isProgressExist = _progressRep.GetProgress(progressId);

                if (isProgressExist == null)
                    return Ok(new
                    {
                        status = "Failed",
                        message = "Your Progress not found!"
                    });

                if (!_progressRep.DeleteProgress(isProgressExist))
                {
                    return StatusCode(500, new
                    {
                        status = "Failed",
                        message = "Something went wrong deleting user"
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
        public IActionResult GetProgresss()
        {
            try
            {
                var allProgresss = _progressRep.GetProgresses();

                if (allProgresss.Count <= 0)
                {
                    return Ok(new
                    {
                        status = "Success",
                        message = "Progress is empty!",
                        data = allProgresss
                    });
                }

                return Ok(new
                {
                    status = "Success",
                    message = "All Progresss Successfully fetched",
                    data = allProgresss
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

        [HttpGet("{progressId}")]
        public IActionResult GetProgress(int progressId)
        {
            try
            {
                var progress = _progressRep.GetProgress(progressId);

                if (progress == null)
                {
                    return Ok(new
                    {
                        status = "Success",
                        message = "Your Progress not found!",
                        data = progress
                    });
                }

                return Ok(new
                {
                    status = "Success",
                    message = "User Nutrition Successfully fetched",
                    data = progress
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
