using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrefetchVideo.Models
{
    public class TaskListModel
    {

        public int Status { get; set; }
        public List<TaskModel> IslemList { get; set; }
        public int DenemeSayi { get; set; }

        public async Task RunIslem()
        {
            List<Task> TaskList = this.IslemList.Select(d => d.Islem).ToList();

            while (this.Status < 2)
            {

                Task t = Task.Run(() => TaskExecute());

                await t;
                Thread.Sleep(100);
                this.Status = (int)t.Status;
                DenemeSayi++;
                if (DenemeSayi > 3)
                {
                    this.Status = 20;

                }
            }
        }

        public void TaskExecute()
        {
            List<Task> TaskList = this.IslemList.Select(d => d.Islem).ToList();
            foreach (var item in TaskList)
                item.Start();
        }

    }
    public class TaskModel
    {
        public Task Islem { get; set; }


    }

    public class DateModel
    {
        public List<DateTime> DateList { get; set; }
    }
}
