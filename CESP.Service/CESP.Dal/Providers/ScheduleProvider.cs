using System;
using System.Collections;
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

        public ScheduleProvider(ICespRepository cespRepository, IMapper mapper, ICespResourceProvider cespResourceProvider)
        {
            _cespRepository = cespRepository;
            _mapper = mapper;
            _cespResourceProvider = cespResourceProvider;
        }

//        public async Task<List<Schedule>> GetSchedulesByBunch(string bunch)
//        {
//            var bunchId = await _cespRepository.GetGroupBunchIdBySysNameOrNull(bunch);
//
//            return bunchId == null
//                ? new List<Schedule>()
//                : await GetSchedulesByBunchId((int) bunchId);
//        }

        // todo сделать нормально
        public async Task<List<ScheduleSection>> GetSchedules()
        {
            var result = new List<ScheduleSection>();

            var bunches = await _cespRepository.GetGroupBunches();

            foreach (var bunch in bunches)
            {
                var groups = await _cespRepository.GetStudentGroupsByBunchId(bunch.Id);

                var scheduleSegments = groups.GroupBy(gr => gr.LanguageLevelId);

                var segments = new List<ScheduleSegment>();
                foreach (var scheduleSegment in scheduleSegments)
                {
                    var items = new List<ScheduleItem>();
                    foreach (var group in scheduleSegment)
                    {
                        var schedules = await _cespRepository
                            .GetSchedulesByGroupId(group.Id);
                        var prices = await _cespRepository
                            .GetPricesByGroupId(group.Id);

                        var scheduleItem = _mapper.Map<(
                                StudentGroupDto,
                                TeacherDto,
                                ScheduleDto,
                                PriceDto),
                                ScheduleItem>
                            ((group, group.Teacher, schedules.FirstOrDefault(), prices.FirstOrDefault()));
                        scheduleItem.TeacherPhoto = _cespResourceProvider.GetFullUrl(scheduleItem.TeacherPhoto);

                        items.Add(scheduleItem);
                    }

                    var level = scheduleSegment.FirstOrDefault().LanguageLevel;
                    segments.Add(_mapper.Map<(
                            LanguageLevelDto, 
                            IEnumerable<ScheduleItem>),
                            ScheduleSegment>
                            ((level, items)));
                }

                result.Add(_mapper.Map<(
                        GroupBunchDto,
                        IEnumerable<ScheduleSegment>),
                        ScheduleSection>
                    ((bunch, segments.OrderBy(s => s.LevelRang))));
            }

            return result.ToList(); 
        }

        public async Task<List<ScheduleSection>> GetSchedulesByLevels(string[] levelNames)
        {
            var result = new List<ScheduleSection>();

            var bunches = await _cespRepository.GetGroupBunches();

            foreach (var bunch in bunches)
            {
                var groups = await _cespRepository.GetStudentGroupsByBunchId(bunch.Id, levelNames);

                var scheduleSegments = groups.GroupBy(gr => gr.LanguageLevelId);

                var segments = new List<ScheduleSegment>();
                foreach (var scheduleSegment in scheduleSegments)
                {
                    var items = new List<ScheduleItem>();
                    foreach (var group in scheduleSegment)
                    {
                        var schedules = await _cespRepository
                            .GetSchedulesByGroupId(group.Id);
                        var prices = await _cespRepository
                            .GetPricesByGroupId(group.Id);

                        var scheduleItem = _mapper.Map<(
                                StudentGroupDto,
                                TeacherDto,
                                ScheduleDto,
                                PriceDto),
                                ScheduleItem>
                            ((group, group.Teacher, schedules.FirstOrDefault(), prices.FirstOrDefault()));
                        scheduleItem.TeacherPhoto = _cespResourceProvider.GetFullUrl(scheduleItem.TeacherPhoto);

                        items.Add(scheduleItem);
                    }

                    var level = scheduleSegment.FirstOrDefault().LanguageLevel;
                    segments.Add(_mapper.Map<(
                            LanguageLevelDto, 
                            IEnumerable<ScheduleItem>),
                            ScheduleSegment>
                            ((level, items)));
                }

                result.Add(_mapper.Map<(
                        GroupBunchDto,
                        IEnumerable<ScheduleSegment>),
                        ScheduleSection>
                    ((bunch, segments.OrderBy(s => s.LevelRang))));
            }

            return result.ToList(); 
        }


        public async Task<List<GroupBunch>> GetBunches()
        {
            var bunches = await _cespRepository.GetGroupBunches();
            return bunches.Select(b => _mapper.Map<GroupBunch>(b)).ToList();
        }
    }
}