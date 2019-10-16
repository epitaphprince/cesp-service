namespace CESP.Service.ViewModels
{
    public class EmailRequest
    {
        public string Name { get; set; }

        // todo regex
        public string Contact { get; set; }
        
        public string Body { get; set; }
    }
}