using System.Collections.Generic;
using System.Threading.Tasks;
using CESP.Core.Models;

namespace CESP.Core.Managers.SpeakingClub
{
    public interface ISpeakingClubManager
    {
        Task<List<SpeakingClubMeetingShort>> GetList(int? count);
        
        Task<SpeakingClubMeeting> Get(string sysName);
    }
}