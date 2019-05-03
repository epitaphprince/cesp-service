using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CESP.Core.Contracts;
using CESP.Core.Models;
using CESP.Database.Context.Payments.Models;
using CESP.Database.Context.Schedules.Models;
using CESP.Database.Context.StudentGroups.Models;

namespace CESP.Dal.Providers
{
    public class ScheduleProvider: IScheduleProvider
    {
        private ICespRepository _cespRepository;
        private readonly IMapper _mapper;

        public ScheduleProvider(ICespRepository cespRepository, IMapper mapper)
        {
            _cespRepository = cespRepository;
            _mapper = mapper;
        }

        public async Task<List<Schedule>> GetSchedulesByBunchId(int bunchId)
        {
            var result = new List<Schedule>();
            
            var groups = await _cespRepository.GetStudentGroupsByBunchId(bunchId);

            foreach (var group in groups)
            {
                var schedules = await _cespRepository.GetSchedulesByGroupId(group.Id);

                var prices = await _cespRepository.GetPricesByGroupId(group.Id);

                var durations = await _cespRepository.GetDurationsByGroupId(group.Id);

                result.Add(_mapper.Map<(
                        StudentGroupDto,
                        List<ScheduleDto>,
                        List<PriceDto>, 
                        List<GroupDurationDto>), 
                        Schedule>
                    ((group, schedules, prices, durations)));
            }
            
            return result.OrderBy(r => r.LevelRang).ToList();
        }
    }
}