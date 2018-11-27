using System;
using System.Collections.Generic;
using System.Text;

namespace INC.Queue.Delegate
{
    public static class JobDelegate
    {
        public delegate void JobResultChangeEventHandler<T>(object sender, JobResultChangeEventArgs<T> args);
    }

    public class JobResultChangeEventArgs<T> : EventArgs
    {
        public T JobResult { get; set; }
    }
}
