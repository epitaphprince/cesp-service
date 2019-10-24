using System.Collections.Generic;
using AutoMapper;

namespace CESP.Dal.Mapping
{
    public static class MappingProfilesDto
    {
        public static List<Profile> GetProfiles()
        {
            return new List<Profile>
            {
                new TeacherMappingProfile(),
                new CourseMappingProfile(),
                new FeedbackMappingProfile(),
                new ScheduleMappingProfile(),
                new PriceMappingProfile(),
                new DurationMappingProfile(),
                new LessonTimeMappingProfile(),
                new EventMappingProfile(),
                new SpeakingClubMappingProfile(),
                new PartnerMappingProfile(),
                new LevelMappingProfile(),
                new UserMappingProfile()
            };
        }
    }
}