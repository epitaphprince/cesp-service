using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CESP.Core.Contracts;
using CESP.Core.Models;
using CESP.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace CESP.Dal.Repositories.Cesp
{
    public class CespRepository: ICespRepository
    {
        private readonly CespContext _context;
        private readonly IMapper _mapper;

        public CespRepository(CespContext cespContext, IMapper mapper)
        {
            _context = cespContext;
            _mapper = mapper;
        }

        public async Task<List<Teacher>> GetListTeacher()
        {
            var teachers = 
                await _context
                    .Teachers
                    .Include(t => t.Photo)
                    .ToListAsync();

            return teachers.Select(t => _mapper.Map<Teacher>(t)).ToList();
        }
    }
}