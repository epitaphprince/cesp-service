using System.Collections.Generic;
using System.Threading.Tasks;
using CESP.Core.Contracts;
using CESP.Core.Models;

namespace CESP.Core.Managers.Feedbacks
{
    public class FeedbackManager: IFeedbackManager
    {
        private readonly IFeedbackProvider _feedbackProvider;
        private readonly ICespResourceProvider _cespResourceProvider;

        public FeedbackManager(IFeedbackProvider provider, ICespResourceProvider resourceProvider)
        {
            _feedbackProvider = provider;
            _cespResourceProvider = resourceProvider;
        }

        public async Task<List<Feedback>> GetList(int? count)
        {
            var feedbacks = await _feedbackProvider.GetListFeedback(count);

            feedbacks.ForEach(
                c =>
                {
                    c.Photo = _cespResourceProvider.GetFullUrl(c.Photo);
                });
            
            return feedbacks;
        }
    }
}