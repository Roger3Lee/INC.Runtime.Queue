using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace INC.Queue
{
    public class JobContainer : IDisposable
    {
        private ConcurrentQueue<JobBase> _jobs = new ConcurrentQueue<JobBase>();

        /// <summary>
        /// Add Job
        /// </summary>
        /// <param name="job"></param>
        public void AddJob(JobBase job)
        {
            _jobs.Enqueue(job);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public JobBase GetJob()
        {
            JobBase job = null;
            if (this._jobs.TryDequeue(out job))
            {
                return job;
            }
            else
            {
                return null;
            }
        }

        public int Count => _jobs.Count;

        public void Dispose()
        {
            _jobs = null;
        }
    }
}
