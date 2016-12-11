using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoRepository
{
    class DuplicateTodoItemException : Exception
    {
        public DuplicateTodoItemException(string message) : base(message)
        {
        }
    }
}
