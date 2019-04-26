using System.Collections.Generic;
using System.Threading.Tasks;
using CESP.Core.Models;

namespace CESP.Core.Contracts
{
    public interface IFeedbackProvider
    {
        Task<List<Feedback>> GetListFeedback(int? count);
    }
}