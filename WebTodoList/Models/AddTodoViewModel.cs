using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTodoList.Models
{
    public class AddTodoViewModel
    {
        [Required, MaxLength(60)]
        public string Text { get; set; }
    }
}
