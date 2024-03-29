﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoRepository
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsCompleted { get; set; }
        /// <summary >
        /// Nullable date time .
        /// DateTime is value type and won ’t allow nulls .
        /// DateTime ? is nullable DateTime and will accept nulls .
        /// Use null when todo completed date does not exist (e.g. todo is still not completed )
        /// </ summary >
        public DateTime? DateCompleted { get; set; }
        public DateTime DateCreated { get; set; }
        /// <summary >
        /// User id that owns this TodoItem
        /// </ summary >
        public Guid UserId { get; set; }
        public TodoItem(string text, Guid userId)
        {
            Id = Guid.NewGuid(); // Generates new unique identifier
            Text = text;
            IsCompleted = false;
            DateCreated = DateTime.Now; // Set creation date as current time
            UserId = userId;
        }
        public TodoItem()
        {
            // entity framework needs this one
            // not for use :)
        }

        public void UpdateValues(TodoItem other)
        {
            Text = other.Text;
            IsCompleted = other.IsCompleted;
            DateCreated = other.DateCreated;
            DateCompleted = other.DateCompleted;
        }

        public bool MarkAsCompleted()
        {
            bool oldValue = IsCompleted;
            IsCompleted = true;
            DateCompleted = DateTime.Now;

            return oldValue == false;
        }
    }
}
