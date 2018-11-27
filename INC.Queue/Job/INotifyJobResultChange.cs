using System;
using System.Collections.Generic;
using System.Text;
using static INC.Queue.Delegate.JobDelegate;

namespace INC.Queue
{
    public interface INotifyJobResultChange<T>
    {
        event JobResultChangeEventHandler<T> OnResultChange;
    }
}
