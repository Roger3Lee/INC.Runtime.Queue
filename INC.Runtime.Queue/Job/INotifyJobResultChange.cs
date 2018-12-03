using System;
using System.Collections.Generic;
using System.Text;
using static INC.Runtime.Queue.Delegate.JobDelegate;

namespace INC.Runtime.Queue
{
    public interface INotifyJobResultChange<T>
    {
        event JobResultChangeEventHandler<T> OnResultChange;
    }
}
