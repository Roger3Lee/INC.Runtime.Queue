using System;
using System.Collections.Generic;
using System.Text;

namespace INC.Runtime.Queue
{
    public interface INotifyJobResultChange<T>
    {
        event INC.Runtime.Queue.Delegate.JobDelegate.JobResultChangeEventHandler<T> OnResultChange;
    }
}
