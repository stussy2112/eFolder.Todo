using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eFolder.Todo.Backup
{
  public enum BackupStatus
  {
    OK,
    InProgress,
    Failed
  }
  public class Backup
  {
    public Backup()
    {

    }
    public Backup(int id, DateTime date, BackupStatus status, IEnumerable<TodoItem> items)
    {
      _items = items;
      Status = status;
      Date = date;
      Id = id;
    }

    public int Id
    {
      get; private set;
    }

    public DateTime Date
    {
      get; private set;
    }

    public BackupStatus Status
    {
      get; private set;
    }

    private IEnumerable<TodoItem> _items;
    public IEnumerable<TodoItem> Items
    {
      get
      {
        return _items ?? Enumerable.Empty<TodoItem>();
      }
      //private set
      //{
      //  _items = value;
      //}
    }

  }
}
