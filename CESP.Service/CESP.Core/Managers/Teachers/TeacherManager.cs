using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using CESP.Core.Contracts;
using CESP.Core.Models;

namespace CESP.Core.Managers.Teachers
{
    public class TeacherManager: ITeacherManager
    {
        private readonly ITeacherProvider _teacheProvider;
        private readonly ICespResourceProvider _cespResourceProvider;
        
        public TeacherManager(
            ITeacherProvider teacheProvider,
            ICespResourceProvider resourceProvider )
        {
            _teacheProvider = teacheProvider;
            _cespResourceProvider = resourceProvider;

        }

        public async Task<List<Teacher>> GetList(int? count)
        {
            var teachers = await _teacheProvider.GetTeachers(count);

            teachers.ForEach(t => t.Photo = _cespResourceProvider.GetFullUrl(t.Photo));
            
            return teachers;
        }
    }
}