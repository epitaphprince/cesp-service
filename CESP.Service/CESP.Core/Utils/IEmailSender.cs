namespace CESP.Core.Utils
{
    public interface IEmailSender
    {
        string SendMail(string subject, string body);
        
        string SendMail(string subject, string body, string to);
    }
}