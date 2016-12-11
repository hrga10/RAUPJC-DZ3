using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoRepository
{
    public class TodoSqlRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;

        public TodoSqlRepository(TodoDbContext context)
        {
            _context = context;
        }
        public void Add(TodoItem todoItem)
        {
            if (todoItem == null)
            {
                throw new ArgumentNullException(nameof(todoItem));
            }
            if (_context.ToDoItems.Any(s => s.Id == todoItem.Id))
            {
                throw new DuplicateTodoItemException("The given Todo item already exists.");
            }

            _context.ToDoItems.Add(todoItem);
            _context.SaveChanges();
            //throw new NotImplementedException();
        }

        public TodoItem Get(Guid todoId, Guid userId)
        {

            TodoItem item = _context.ToDoItems.FirstOrDefault(s => s.Id == todoId);
            if (item.UserId.Equals(userId))
                return item;

            throw new TodoAccessDeniedException("User is not the owner of the Todo item.");
        }

        public List<TodoItem> GetActive(Guid userId)
        {
            return _context.ToDoItems
                .Where(s => s.UserId == userId)
                .Where(s => !s.IsCompleted)
                .ToList();
            //throw new NotImplementedException();
        }

        public List<TodoItem> GetAll(Guid userId)
        {
            return _context.ToDoItems
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.DateCreated)
                .ToList();
            //throw new NotImplementedException();
        }
        public List<TodoItem> GetCompleted(Guid userId)
        {
            return _context.ToDoItems
               .Where(s => s.UserId == userId)
               .Where(s => s.IsCompleted)
               .ToList();
            //throw new NotImplementedException();
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction, Guid userId)
        {
            return _context.ToDoItems
                .Where(s => s.UserId == userId)
                .Where(s => filterFunction(s))
                .ToList();
            //throw new NotImplementedException();
        }

        public bool MarkAsCompleted(Guid todoId, Guid userId)
        {
            // checking ownership
            TodoItem item = Get(todoId, userId);

            bool value = item.MarkAsCompleted();
            _context.SaveChanges();

            return value;
            //throw new NotImplementedException();
        }

        public bool Remove(Guid todoId, Guid userId)
        {
            // checking ownership
            TodoItem item = Get(todoId, userId);

            if (item == null)
                return false;

            _context.ToDoItems.Remove(item);
            _context.SaveChanges();

            return true;
            //throw new NotImplementedException();
        }


        public void Update(TodoItem todoItem, Guid userId)
        {
            // checking ownership
            TodoItem item = Get(todoItem.Id, userId);

            if (item != null)
            {
                item.UpdateValues(todoItem);
                _context.SaveChanges();
            }
            else    
                Add(todoItem);
            

            //throw new NotImplementedException();
        }
    }
}
