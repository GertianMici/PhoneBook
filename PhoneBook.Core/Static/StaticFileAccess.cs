using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Core.Static
{
    public static class StaticFileAccess
    {
        /// <summary>
        /// Reads from a file, by filepath
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static async Task<string> ReadFromFile(string filePath)
        {
            if (File.Exists(filePath))
                return await File.ReadAllTextAsync(filePath);
            else
                return string.Empty;
        }
        /// <summary>
        /// writes "to write" into a file by filepath
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="toWrite"></param>
        /// <returns></returns>
        public static async Task WriteToFile(string filepath, string toWrite)
        {
            await File.WriteAllTextAsync(filepath, toWrite);
        }
    }
}
