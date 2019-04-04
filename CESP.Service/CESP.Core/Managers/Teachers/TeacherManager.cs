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
        private readonly ICespRepository _cespRepository;
        private readonly ICespResourceProvider _cespResourceProvider;
        
        public TeacherManager(
            ICespRepository cespRepository,
            ICespResourceProvider resourceProvider )
        {
            _cespRepository = cespRepository;
            _cespResourceProvider = resourceProvider;

        }

        public async Task<List<Teacher>> GetList()
        {
            var teachers = await _cespRepository.GetListTeacher();

            teachers.ForEach(t => t.Photo = GetUrl(t.Photo));
            
            return teachers;
        }

        private string GetUrl(string urlPart)
        {
            var url = new Uri(new Uri(_cespResourceProvider.GetImagesBasePath()), urlPart);
            return url.OriginalString;
        }
    }
}