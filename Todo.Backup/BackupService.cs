using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFolder.Todo.Backup
{
  public class BackupService : IBackupService
  {
    public BackupService()
    {

    }

    public IEnumerable<Backup> GetAll()
    {
      var rtnVal = new List<Backup>();

      var items = new List<TodoItem>();
      var date = DateTime.Now;
      Random random = new Random();
      for (int i = 0; i < 2; i++)
      {
        date = date.AddDays(i);
        for (int j = 0; j < 5; j++)
        {
          date = date.AddHours(j);
          var done = random.Next(0, 1);
          items.Add(new TodoItem(i + j, "Sean Williams", string.Format("Subject {0}", i + j), date, Convert.ToBoolean(done)));
        }
      }
      //var date = DateTime.Now;
      for (int i = 0; i < 7; i++)
      {
        date = date.AddDays(i);
        for (int j = 0; j < 24; j++)
        {
          date = date.AddHours(j);
          rtnVal.Add(new Backup(i + j, date, BackupStatus.OK, items));
        }
      }

      return rtnVal;
    }

    public async Task<int> InitiateAsync()
    {
      // Generate a random id
      var generator = new Random();
      var id = 0;
      lock (generator)
      {
        id = (new Random()).Next(1, int.MaxValue);
      }
      await Task.Delay(1000);
      return id;
    }
  }
}
