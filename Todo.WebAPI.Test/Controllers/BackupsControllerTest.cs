using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eFolder.Todo.Controllers;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Results;
using System.Threading.Tasks;
using System.Net;
using eFolder.Todo.Backup;
using eFolder.Todo.Models;
using eFolder.Todo.Backup.Fakes;
using System.Collections.Generic;
using System.Linq;

namespace eFolder.Todo.Tests.Controllers
{
  [TestClass]
  public class BackupsControllerTest
  {
    //[TestMethod]
    //public void PostTest()
    //{
    //  //Backup accounts
    //  //This API will initiate a complete backup of all todo items in the TodoItemServer. The backup is asynchronous and the API will return the the id for the initiated backup.

    //  //Request: POST /backups
    //  //Request body: N/A
    //  //Response body:

    //  //```
    //  //{
    //  // “backupId”: < backupId >
    //  //}
    //  //```

    //  BackupsController controller = new BackupsController()
    //  {
    //    Request = new HttpRequestMessage() { Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } } }
    //  };

    //  // Start with a synchronous operation, then change it to async
    //  var actual = controller.StartBackup() as OkNegotiatedContentResult<Backup>;
    //  Assert.IsNotNull(actual);
    //  var status = actual.Content;
    //  Assert.IsNotNull(status);
    //  Assert.IsTrue(0 < status.BackupId);
    //}

    [TestMethod]
    public async Task PostAsyncTest()
    {
      //Backup accounts
      //This API will initiate a complete backup of all todo items in the TodoItemServer. The backup is asynchronous and the API will return the the id for the initiated backup.

      //Request: POST /backups
      //Request body: N/A
      //Response body:

      //```
      //{
      // “backupId”: < backupId >
      //}
      //```      

      // Arrange
      IBackupService backupService = new StubIBackupService() {
        InitiateAsync = () =>
        {
          // Generate a random id
          var generator = new Random();
          var id = 0;
          lock (generator)
          {
            id = (new Random()).Next(1, int.MaxValue);
          }

          return Task.FromResult(id);
        }
      };
      
      BackupsController target = new BackupsController(backupService)
      {
        Request = new HttpRequestMessage() { Method = HttpMethod.Post, Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } } }
      };

      // Act
      dynamic actual = await target.PostAsync();
      Assert.IsNotNull(actual);
      dynamic status = actual.Content;
      Assert.IsNotNull(status);
      Assert.IsTrue(0 < status.BackupId);
    }

    [TestMethod]
    public void GetOkTest()
    {
      //List backups
      //This API will list all backups that have initiated.Backup status is one of the following:
      //•	In progress
      //•	OK
      //•	Failed
      //Request: GET / backups
      //Request body: N / A
      //Response body:
      //  ```
      //  [
      // {
      // “backupId”: “<backup id>”,
      //  “date”: “<backup date>”,

      //“status”: “<backup status>”
      //    },
      //    {
      //      …
      //    }
      //  ]
      //  ```

      // Arrange
      BackupStatus backupStatus = BackupStatus.OK;
      IBackupService backupService = new StubIBackupService()
      {
        InitiateAsync = () =>
        {
          // Generate a random id
          var generator = new Random();
          var id = 0;
          lock (generator)
          {
            id = (new Random()).Next(1, int.MaxValue);
          }

          return Task.FromResult(id);
        },
        GetAll = () =>
        {
          var rtnVal = new List<Backup.Backup>();

          var date = DateTime.Now;
          for (int i = 0; i < 7; i++)
          {
            date = date.AddDays(i);
            for (int j = 0; j < 24; j++)
            {
              date = date.AddHours(j);
              rtnVal.Add(new Backup.Backup(i + j, date, backupStatus, null));
            }
          }

          return rtnVal;
        }
      };

      BackupsController target = new BackupsController(backupService)
      {
        Request = new HttpRequestMessage() { Method = HttpMethod.Get, Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } } }
      };

      // Act
      var actual = target.Get() as OkNegotiatedContentResult<IEnumerable<BackupViewModel>>;
      Assert.IsNotNull(actual);
      
      var backups = actual.Content;
      Assert.IsNotNull(backups);
      Assert.IsTrue(backups.Any());
      Assert.IsTrue(backups.All((b) => 0 <= b.BackupId));
      Assert.IsTrue(backups.All((b) => backupStatus == b.Status));
    }

    [TestMethod]
    public void GetInProgressTest()
    {
      //List backups
      //This API will list all backups that have initiated.Backup status is one of the following:
      //•	In progress
      //•	OK
      //•	Failed
      //Request: GET / backups
      //Request body: N / A
      //Response body:
      //  ```
      //  [
      // {
      // “backupId”: “<backup id>”,
      //  “date”: “<backup date>”,

      //“status”: “<backup status>”
      //    },
      //    {
      //      …
      //    }
      //  ]
      //  ```

      // Arrange
      BackupStatus backupStatus = BackupStatus.InProgress;
      IBackupService backupService = new StubIBackupService()
      {
        InitiateAsync = () =>
        {
          // Generate a random id
          var generator = new Random();
          var id = 0;
          lock (generator)
          {
            id = (new Random()).Next(1, int.MaxValue);
          }

          return Task.FromResult(id);
        },
        GetAll = () =>
        {
          var rtnVal = new List<Backup.Backup>();

          var date = DateTime.Now;
          for (int i = 0; i < 7; i++)
          {
            date = date.AddDays(i);
            for (int j = 0; j < 24; j++)
            {
              date = date.AddHours(j);
              rtnVal.Add(new Backup.Backup(i + j, date, backupStatus, null));
            }
          }

          return rtnVal;
        }
      };

      BackupsController target = new BackupsController(backupService)
      {
        Request = new HttpRequestMessage() { Method = HttpMethod.Get, Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } } }
      };

      // Act
      var actual = target.Get() as OkNegotiatedContentResult<IEnumerable<BackupViewModel>>;
      Assert.IsNotNull(actual);

      var backups = actual.Content;
      Assert.IsNotNull(backups);
      Assert.IsTrue(backups.Any());
      Assert.IsTrue(backups.All((b) => 0 <= b.BackupId));
      Assert.IsTrue(backups.All((b) => backupStatus == b.Status));
    }

    [TestMethod]
    public void GetFailedTest()
    {
      //List backups
      //This API will list all backups that have initiated.Backup status is one of the following:
      //•	In progress
      //•	OK
      //•	Failed
      //Request: GET / backups
      //Request body: N / A
      //Response body:
      //  ```
      //  [
      // {
      // “backupId”: “<backup id>”,
      //  “date”: “<backup date>”,

      //“status”: “<backup status>”
      //    },
      //    {
      //      …
      //    }
      //  ]
      //  ```

      // Arrange
      BackupStatus backupStatus = BackupStatus.Failed;
      IBackupService backupService = new StubIBackupService()
      {
        InitiateAsync = () =>
        {
          // Generate a random id
          var generator = new Random();
          var id = 0;
          lock (generator)
          {
            id = (new Random()).Next(1, int.MaxValue);
          }

          return Task.FromResult(id);
        },
        GetAll = () =>
        {
          var rtnVal = new List<Backup.Backup>();

          var date = DateTime.Now;
          for (int i = 0; i < 7; i++)
          {
            date = date.AddDays(i);
            for (int j = 0; j < 24; j++)
            {
              date = date.AddHours(j);
              rtnVal.Add(new Backup.Backup(i + j, date, backupStatus, null));
            }
          }

          return rtnVal;
        }
      };

      BackupsController target = new BackupsController(backupService)
      {
        Request = new HttpRequestMessage() { Method = HttpMethod.Get, Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } } }
      };

      // Act
      var actual = target.Get() as OkNegotiatedContentResult<IEnumerable<BackupViewModel>>;

      // Assert
      Assert.IsNotNull(actual);
      var backups = actual.Content;
      Assert.IsNotNull(backups);
      Assert.IsTrue(backups.Any());
      Assert.IsTrue(backups.All((b) => 0 <= b.BackupId));
      Assert.IsTrue(backups.All((b) => backupStatus == b.Status));
    }

    [TestMethod]
    public void ExportTest()
    {
      //Export backup
      //This API will return the content of a specified backup id the CSV format specified below.

      //Request: GET / exports /{
      //        backup id}
      //      Request body: N / A
      //Response body:
      //  ```
      //  Username;TodoItemId;Subject;DueDate;Done
      //  {username};{todoitemid};{subject};{duedate};{done}
      //  ```      

      // Arrange
      BackupStatus backupStatus = BackupStatus.InProgress;
      var items = new List<TodoItem>();
      var date = DateTime.Now;
      Random random = new Random();
      for (int i = 0; i < 2; i++)
      {
        date = date.AddDays(i);
        for (int j = 0; j < 5; j++)
        {
          date = date.AddHours(j);
          var done = random.Next(0, int.MaxValue);
          items.Add(new TodoItem(i + j, "Sean Williams", string.Format("Subject {0}", i + j), date, Convert.ToBoolean(done)));
        }
      }
      IBackupService backupService = new StubIBackupService()
      {
        InitiateAsync = () =>
        {
          // Generate a random id
          var generator = new Random();
          var id = 0;
          lock (generator)
          {
            id = random.Next(1, int.MaxValue);
          }

          return Task.FromResult(id);
        },
        GetAll = () =>
        {
          var rtnVal = new List<Backup.Backup>();

          date = DateTime.Now;
          for (int i = 0; i < 7; i++)
          {
            date = date.AddDays(i);
            for (int j = 0; j < 24; j++)
            {
              date = date.AddHours(j);
              rtnVal.Add(new Backup.Backup(i + j, date, backupStatus, items));
            }
          }

          return rtnVal;
        }
      };

      BackupsController target = new BackupsController(backupService)
      {
        Request = new HttpRequestMessage() { Method = HttpMethod.Get, Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } } }
      };


      // Act
      int backupId = 1;

      var actual = target.Export(backupId) as ResponseMessageResult;

      // Assert
      Assert.IsNotNull(actual);
      Assert.IsTrue(actual.Response.IsSuccessStatusCode);
      Assert.IsTrue(0 < actual.Response.Content.Headers.ContentLength);
    }
  }
}
