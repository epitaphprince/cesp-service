using System.Collections.Generic;
using System.Threading.Tasks;
using CESP.Core.Models;

namespace CESP.Core.Managers.Feedbacks
{
    public interface IFeedbackManager
    {
        Task<List<Feedback>> GetList(int? count);
    }
}