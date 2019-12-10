using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSDosNote.Domain.Exceptions
{
    public class NoSuchCommandFoundException : Exception
    {
        public NoSuchCommandFoundException(string command) : base("No such command found: " + command)
        {
        }
    }
}
