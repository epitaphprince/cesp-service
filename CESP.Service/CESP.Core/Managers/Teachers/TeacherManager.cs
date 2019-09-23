using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
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
            var teachers = await _teacherProvider.GetTeachers(count);

            teachers.ForEach(t => t.Photo = _cespResourceProvider.GetFullUrl(t.Photo));

            return teachers;
        }
    }
}