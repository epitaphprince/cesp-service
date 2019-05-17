using System.Collections.Generic;
using System.Threading.Tasks;
using CESP.Core.Contracts;
using CESP.Core.Models;

namespace CESP.Core.Managers.SpeakingClub
{
    public class SpeakingClubManager: ISpeakingClubManager
    {
        private readonly ISpeakingClubProvider _speakingClubProvider;
        private readonly ICespResourceProvider _cespResourceProvider;
        
        public SpeakingClubManager(
            ISpeakingClubProvider speakingClubProvider,
            ICespResourceProvider resourceProvider )
        {
            _speakingClubProvider = speakingClubProvider;
            _cespResourceProvider = resourceProvider;

        }

        public async Task<List<SpeakingClubMeetingShort>> GetList(int? count)
        {
            var meetings = await _speakingClubProvider.GetMeetings(count);

            meetings.ForEach(m =>
            {
                if (string.IsNullOrEmpty(m.Info))
                {
                    m.SysName = null;
                }

                m.Photo = _cespResourceProvider.GetFullUrl(m.Photo);
            });
            
            return meetings;
        }

        public async Task<SpeakingClubMeeting> Get(string sysName)
        {
            return await _speakingClubProvider.GetMeeting(sysName);
        }
    }
}