using eFolder.Todo.Backup;
using eFolder.Todo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

// namespaces...
namespace eFolder.Todo.Controllers
{
  // public classes...
  public class BackupsController : ApiController
  {
    // private fields...
    /// <summary>
    /// Initializes a new instance of the <see cref="BackupsController"/> class.
    /// </summary>
    private readonly IBackupService _backupService;
    // public constructors...
    public BackupsController(IBackupService backupService)
    {
      _backupService = backupService;
    }

    // public methods...
    [Route("exports/{backupId}")]
    [HttpGet]
    public IHttpActionResult Export(int backupId)
    {
      IHttpActionResult rtnVal = Ok("Username;TodoItemId;Subject;DueDate;Done");
      var sb = new StringBuilder();
      sb.AppendLine("Username;TodoItemId;Subject;DueDate;Done");
      if (0 < backupId)
      {
        var backups = _backupService.GetAll();
        var filtered = backups.FirstOrDefault((b) => b.Id == backupId);
        // Create the csv stuff
        if (!ReferenceEquals(null, filtered))
        {
          foreach (var item in filtered.Items)
          {
            sb.AppendFormat("{0};{1};{2};{3};{4}{5}", item.UserName, item.Id, item.Subject, item.DueDate, item.Done, Environment.NewLine);
          }
        }
      }
      // TODO: set a file name header on the response
      rtnVal = ResponseMessage(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(sb.ToString(), Encoding.UTF8, "text/csv"), RequestMessage = Request
      });
      return rtnVal;
    }

    [Route("backups")]
    [HttpGet]
    public IHttpActionResult Get()
    {
      IHttpActionResult rtnVal = Ok(Enumerable.Empty<BackupViewModel>());
      var backups = (_backupService.GetAll() ?? Enumerable.Empty<Backup.Backup>()).Select((b) => new BackupViewModel(b));
      rtnVal = Ok(backups);
      return rtnVal;
    }

    //[Route("backups")]
    //public IHttpActionResult Post()
    //{
    //  IHttpActionResult rtnVal = NotFound();
    //  // Generate a random id
    //  var id = (new Random()).Next(1, int.MaxValue);
    //  var status = new Backup(id);
    //  rtnVal = Ok(status);
    //  return rtnVal;
    //}

    [Route("backups")]
    [HttpPost]
    public async Task<IHttpActionResult> PostAsync()
    {
      IHttpActionResult rtnVal = NotFound();
      var id = await _backupService.InitiateAsync();
      var status = new
      {
        BackupId = id
      };
      rtnVal = Ok(status);
      return rtnVal;
    }
  }
}
