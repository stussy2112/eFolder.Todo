using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFolder.Todo.Backup
{
  public interface IBackupService
  {
    IEnumerable<Backup> GetAll();

    Task<int> InitiateAsync();


  }
}
