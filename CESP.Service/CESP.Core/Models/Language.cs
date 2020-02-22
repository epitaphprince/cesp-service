namespace CESP.Core.Models
{
    public class Language
    {
        public string Name { get; set; }
        public string Color { get; set; }

        public Language(string name)
        {
            Name = name;
        }

        public Language()
        {
        }
    }
}