using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CESP.Core.Contracts;
using CESP.Core.Models;

namespace CESP.Dal.Providers
{
    public class FeedbackProvider: IFeedbackProvider
    {
        private ICespRepository _cespRepository;
        private readonly IMapper _mapper;

        public FeedbackProvider(ICespRepository cespRepository, IMapper mapper)
        {
            _cespRepository = cespRepository;
            _mapper = mapper;
        }

        public async Task<List<Feedback>> GetListFeedback(int? count)
        {
            var feedbacks = await _cespRepository.GetFeedbacks(count);
            return feedbacks.Select(t => _mapper.Map<Feedback>(t)).ToList();
        }
    }
}