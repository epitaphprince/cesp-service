using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CESP.Core.Utils;
using Microsoft.AspNetCore.Mvc;
using CESP.Core.Managers.Users;
using CESP.Core.Models;
using CESP.Service.ViewModels.Requests;

namespace CESP.Service.Controllers
{
    [ApiController]
    [Route("sendemail")]
    public class SignUpController: Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public SignUpController(
            IEmailSender emailSender,
            IUserManager userManager,
            IMapper mapper)
        {
            _emailSender = emailSender;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Send([FromBody] EmailRequest request)
        {
            if (request == null 
                || string.IsNullOrEmpty(request.Contact))
            {
                return new BadRequestResult();
            }

            SendEmail(request.Name, request.Contact, request.Body, request.Source);

            await _userManager.Save(_mapper.Map<User>(request));
            
            return await Task.FromResult(new OkResult());
        }

        private void SendEmail(string name, string contact, string body, string source)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Имя: " + name);
            stringBuilder.AppendLine("Контакт: " + contact);
            stringBuilder.AppendLine("Со страницы: " + source);

            stringBuilder.AppendLine();
            stringBuilder.AppendLine(body);
            stringBuilder.AppendLine();
            _emailSender.SendMail("Обратная связь с сайта centroespanol.ru",
                stringBuilder.ToString());
        }
    }
}