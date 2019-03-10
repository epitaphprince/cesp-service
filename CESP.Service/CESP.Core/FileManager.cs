using System;
using System.IO;

namespace CESP.Core
{
    public class FileManager
    {
        public void Save(Stream content)
        {
            CheckFile(content);
            
            
        }
        
        private void CheckFile(Stream content)
        {
            if (content == null || content.Length == 0)
            {
                throw new ArgumentException("Loading file mustn't be empty");
            }
        }
    }
}