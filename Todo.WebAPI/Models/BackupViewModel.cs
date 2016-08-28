using System;
using eFolder.Todo.Backup;

namespace eFolder.Todo.Models
{

  public class BackupViewModel
  {
    public BackupViewModel(Backup.Backup source)
    {
      if (!ReferenceEquals(null, source))
      {
        BackupId = source.Id;
        Status = source.Status;
        Date = source.Date;
      }
    }

    public long BackupId
    {
      get;
      set;
    }

    public DateTime Date
    {
      get;
      set;
    }

    public BackupStatus Status
    {
      get;
      set;
    }

  }
}
