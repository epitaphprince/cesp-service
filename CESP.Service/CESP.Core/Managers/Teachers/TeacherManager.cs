using System.Collections.Generic;
using System.Threading.Tasks;
using CESP.Core.Contracts;
using CESP.Core.Models;

namespace CESP.Core.Managers.Teachers
{
    public class TeacherManager : ITeacherManager
    {
        private readonly ITeacherProvider _teacherProvider;
        private readonly ICespResourceProvider _cespResourceProvider;

        public TeacherManager(
            ITeacherProvider teacherProvider,
            ICespResourceProvider resourceProvider)
        {
            _teacherProvider = teacherProvider;
            _cespResourceProvider = resourceProvider;
        }

        public async Task<List<Teacher>> GetList(int? count)
        {
            var teachers = await _teacherProvider.GetList(count);

            teachers.ForEach(t =>
            {
                t.Photo = _cespResourceProvider.GetFullUrl(t.Photo);
                t.SmallPhoto = _cespResourceProvider.GetFullUrl(t.SmallPhoto);
                t.LargePhoto = _cespResourceProvider.GetFullUrl(t.LargePhoto);
            });

            return teachers;
        }

        public async Task Update(Teacher teacher)
        {
            await _teacherProvider.Update(teacher);
        }
    }
}