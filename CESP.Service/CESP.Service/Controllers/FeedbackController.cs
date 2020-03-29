using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CESP.Core.Managers.Feedbacks;
using CESP.Service.ViewModels.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CESP.Service.Controllers
{
    [ApiController]
    [Route("feedbacks")]
    public class FeedbackController : Controller
    {
        private readonly IFeedbackManager _feedbackManager;
        private readonly IMapper _mapper;

        public FeedbackController(
            IFeedbackManager feedbackManager,
            IMapper mapper)
        {
            _feedbackManager = feedbackManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetList(int? count)
        {
            if (count < 0)
                count = 0;

            var feedbacks = await _feedbackManager.GetList(count);

            return Ok(feedbacks.Select(t => _mapper.Map<FeedbackResponse>(t)));
        }
    }
}