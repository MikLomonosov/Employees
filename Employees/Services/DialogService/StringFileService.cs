using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Employees.Services.DialogService
{
    public class StringFileService : IFileService
    {
        public string Open(string fileName)
        {
            string filePath = null;
            
            using(FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                filePath = fileName;
            }
            return filePath;
        }
    }
}
