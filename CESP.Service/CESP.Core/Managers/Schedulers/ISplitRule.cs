using System.Collections.Generic;
using CESP.Core.Models;

namespace CESP.Core.Managers.Schedulers
{
    public interface ISplitRule
    {
        List<ScheduleSegment> Split(ScheduleSegment segment);
    }
}