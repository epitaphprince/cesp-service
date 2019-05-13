using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CESP.Core.Managers.Partners;
using CESP.Service.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CESP.Service.Controllers
{
    [Route("partners")]
    public class PartnerController: Controller
    {
        private readonly IPartnerManager _partnerManager;
        private readonly IMapper _mapper;

        public PartnerController(IPartnerManager partnerManager, 
            IMapper mapper)
        {
            _partnerManager = partnerManager;
            _mapper = mapper;
        }
        
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetList(int? count)
        {
            if (count < 0)
                count = 0;
            
            var partners = await _partnerManager.GetList(count);

            return  Ok(partners.Select(t => _mapper.Map<PartnerShortResponse>(t)));
        }
        
        [HttpGet]
        [Route("{sysname}")]
        public async Task<IActionResult> Get([FromRoute] string sysname)
        {
            var partner = await _partnerManager.Get(sysname);

            return Ok( _mapper.Map<PartnerResponse>(partner));
        }
        
    }
}