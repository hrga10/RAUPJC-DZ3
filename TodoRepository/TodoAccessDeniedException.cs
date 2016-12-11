using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoRepository
{
    class TodoAccessDeniedException : Exception
    {
        public TodoAccessDeniedException(string message) : base(message)
        {
        }
    }
}
