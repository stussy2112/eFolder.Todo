using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eFolder.Todo.Backup
{
  public class TodoItem
  {
    public TodoItem(int id, string userName, string subject, DateTime dueDate, bool done)
    {
      Id = id;
      UserName = userName;
      Subject = subject;
      DueDate = dueDate;
      Done = done;
    }
    public TodoItem()
    {
      
    }
    public bool Done
    {
      get;
      set;
    }
    public DateTime DueDate
    {
      get; set;
    }
    public int Id
    {
      get;
      set;
    }

    public string Subject
    {
      get;
      set;
    }

    public string UserName
    {
      get; set;
    }
  }
}
