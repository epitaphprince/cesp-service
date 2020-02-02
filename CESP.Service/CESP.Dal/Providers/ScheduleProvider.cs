using System;
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

        public async Task<List<ScheduleItem>> GetScheduleItems()
        {
            var groups = await _cespRepository.GetStudentGroups();
            return await GetAllItems(groups);
        }

        public async Task<List<ScheduleItem>> GetScheduleItemsByLevels(string[] levelNames)
        {
            var groups = await _cespRepository.GetStudentGroupsByLevels(levelNames);
            return await GetAllItems(groups);
        }

        private async Task<List<ScheduleItem>> GetAllItems(IEnumerable<StudentGroupDto> studentGroups)
        {
            var items = new List<ScheduleItem>();
            foreach (StudentGroupDto groupDto in studentGroups)
            {
                var schedule = await _cespRepository
                    .GetScheduleByGroupIdFirstOrDefault(groupDto.Id);

                var price = await _cespRepository
                    .GetPriceByGroupIdFirstOrDefault(groupDto.Id);

                var scheduleItem = _mapper.Map<(
                        StudentGroupDto,
                        TeacherDto,
                        ScheduleDto,
                        PriceDto,
                        LanguageLevelDto),
                        ScheduleItem>
                    ((groupDto, groupDto.Teacher, schedule, price, groupDto.LanguageLevel));

                scheduleItem.TeacherPhoto = _cespResourceProvider
                    .GetFullUrl(scheduleItem.TeacherPhoto);

                if (scheduleItem.Teacher != null)
                {
                    scheduleItem.Teacher.Photo =
                        _cespResourceProvider.GetFullUrl(scheduleItem.Teacher.Photo);
                    scheduleItem.Teacher.SmallPhoto =
                        _cespResourceProvider.GetFullUrl(scheduleItem.Teacher.SmallPhoto);
                    scheduleItem.Teacher.LargePhoto =
                        _cespResourceProvider.GetFullUrl(scheduleItem.Teacher.LargePhoto);
                }

                items.Add(scheduleItem);
            }

            return items;
        }

        public async Task<List<GroupBunch>> GetBunches()
        {
            var bunches = await _cespRepository.GetGroupBunches();
            return bunches.Select(b => _mapper.Map<GroupBunch>(b)).ToList();
        }
    }
}