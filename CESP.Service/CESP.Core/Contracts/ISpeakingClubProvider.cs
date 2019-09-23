using System.Collections.Generic;
using System.Threading.Tasks;
using CESP.Core.Models;

namespace CESP.Core.Contracts
{
    public interface ISpeakingClubProvider
    {
        Task<List<SpeakingClubMeetingShort>> GetMeetings(int? count);

        Task<SpeakingClubMeeting> GetMeeting(string sysName);
    }
}