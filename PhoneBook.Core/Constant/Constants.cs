using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Core.Constant
{
    public static class Constants
    {
        public static string DatabasePath { get; } = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "PhoneBook.DAL", "Data");
    }
}
