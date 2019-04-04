namespace CESP.Dal.Infrastructure
{
    public interface IFolderProvider
    {  
        
        
        bool Exists(string folderPath);

        void Create(string folderPath); 
    }
}