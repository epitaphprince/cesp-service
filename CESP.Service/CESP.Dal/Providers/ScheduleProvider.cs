using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CESP.Core.Contracts;
using CESP.Core.Models;
using CESP.Database.Context.Education.Models;
using CESP.Database.Context.Payments.Models;
using CESP.Database.Context.Schedules.Models;
using CESP.Database.Context.StudentGroups.Models;

namespace CESP.Dal.Providers
{
    public class ScheduleProvider : IScheduleProvider
    {
        private readonly ICespRepository _cespRepository;
        private readonly ICespResourceProvider _cespResourceProvider;
        private readonly IMapper _mapper;

        public ScheduleProvider(ICespRepository cespRepository, IMapper mapper,
            ICespResourceProvider cespResourceProvider)
        {
            _cespRepository = cespRepository;
            _mapper = mapper;
            _cespResourceProvider = cespResourceProvider;
        }

        public async Task<List<ScheduleSegment>> GetSchedules()
        {
            var groups = await _cespRepository.GetStudentGroups();
            return await GetSchedules(groups);
        }

        public async Task<List<ScheduleSegment>> GetSchedulesByLevels(string[] levelNames)
        {
            var groups = await _cespRepository.GetStudentGroupsByLevels(levelNames);
            return await GetSchedules(groups);
        }
        
        private async Task<List<ScheduleSegment>> GetSchedules(IEnumerable<StudentGroupDto> groups)
        {
            var scheduleSegmentsDto = groups.GroupBy(gr => gr.LanguageLevelId);

            var segments = new List<ScheduleSegment>();
            foreach (var scheduleSegmentDto in scheduleSegmentsDto)
            {
                var items = new List<ScheduleItem>();
                foreach (var groupDto in scheduleSegmentDto)
                {
                    var schedules = await _cespRepository
                        .GetSchedulesByGroupId(groupDto.Id);
                    var prices = await _cespRepository
                        .GetPricesByGroupId(groupDto.Id);

                    var scheduleItem = _mapper.Map<(
                            StudentGroupDto,
                            TeacherDto,
                            ScheduleDto,
                            PriceDto),
                            ScheduleItem>
                        ((groupDto, groupDto.Teacher, schedules.FirstOrDefault(), prices.FirstOrDefault()));
                    scheduleItem.TeacherPhoto = _cespResourceProvider.GetFullUrl(scheduleItem.TeacherPhoto);

                    items.Add(scheduleItem);
                }

                var level = scheduleSegmentDto.FirstOrDefault().LanguageLevel;
                segments.Add(_mapper.Map<(
                        LanguageLevelDto,
                        IEnumerable<ScheduleItem>),
                        ScheduleSegment>
                    ((level, items)));
            }

            return segments.ToList();
        }


        public async Task<List<GroupBunch>> GetBunches()
        {
            var bunches = await _cespRepository.GetGroupBunches();
            return bunches.Select(b => _mapper.Map<GroupBunch>(b)).ToList();
        }
    }
}