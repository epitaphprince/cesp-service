using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CESP.Core.Contracts;
using CESP.Core.Models;

namespace CESP.Dal.Providers
{
    public class SpeakingClubProvider: ISpeakingClubProvider
    {
        private ICespRepository _cespRepository;
        private readonly IMapper _mapper;

        public SpeakingClubProvider(ICespRepository cespRepository, IMapper mapper)
        {
            _cespRepository = cespRepository;
            _mapper = mapper;
        }

        public async Task<List<SpeakingClubMeetingShort>> GetMeetings(int? count)
        {
            var meetings = await _cespRepository.GetSpeakingClubMeetings(count);
            return meetings.Select(t => _mapper.Map<SpeakingClubMeetingShort>(t)).ToList();
        }

        public async Task<SpeakingClubMeeting> GetMeeting(string sysName)
        {
            var meeting = await _cespRepository.GetSpeakingClubMeeting(sysName);
            return _mapper.Map<SpeakingClubMeeting>(meeting);
        }
    }
}