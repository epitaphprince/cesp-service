namespace CESP.Core.Utils
{
    public interface IEmailSender
    {
        void SendMail(string subject, string body);
        
        void SendMail(string subject, string body, string to);
    }
}