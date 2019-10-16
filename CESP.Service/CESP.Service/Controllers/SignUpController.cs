using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CESP.Core.Utils;
using CESP.Service.ViewModels;
using Microsoft.AspNetCore.Mvc;
using CESP.Core.Managers.Users;
using CESP.Core.Models;

namespace CESP.Service.Controllers
{
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
        public async Task<ActionResult> Send(EmailRequest request)
        {
            if (request == null 
                || string.IsNullOrEmpty(request.Contact)
                || await _userManager.IsExists(request.Contact))
            {
                return new BadRequestResult();
            }

            SendEmail(request.Name, request.Contact, request.Body);

            await _userManager.Save(_mapper.Map<User>(request));
            
            return await Task.FromResult(new OkResult());
        }

        private string SendEmail(string name, string contact, string body)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Имя: " + name);
            stringBuilder.AppendLine("Контакт: " + contact);
            

            stringBuilder.AppendLine();
            stringBuilder.AppendLine(body);
            stringBuilder.AppendLine();
            var str = _emailSender.SendMail("Обратная связь с сайта centroespanol.ru",
                stringBuilder.ToString());
            return str;
        }
    }
}