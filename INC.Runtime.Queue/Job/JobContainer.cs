using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace INC.Runtime.Queue
{
    public class JobContainer : IDisposable
    {
        private ConcurrentQueue<JobBase> _normaljobs = new ConcurrentQueue<JobBase>();
		private ConcurrentQueue<JobBase> _hightJobs = new ConcurrentQueue<JobBase>();
		private ConcurrentQueue<JobBase> _lowJobs = new ConcurrentQueue<JobBase>();

        /// <summary>
        /// Add Job
        /// </summary>
        /// <param name="job"></param>
        public void AddJob(JobBase job)
        {
			switch (job.Priority)
			{
				case JobPriority.NORMAL:
					_normaljobs.Enqueue(job);
					break;
				case JobPriority.HIGHEST:
					_hightJobs.Enqueue(job);
					break;
				case JobPriority.LOWEST:
					_lowJobs.Enqueue(job);
					break;
			}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public JobBase GetJob()
        {
            JobBase job = null;
			if (this._hightJobs.TryDequeue(out job) || this._normaljobs.TryDequeue(out job)
				|| this._lowJobs.TryDequeue(out job))
            {
                return job;
            }
            else
            {
                return null;
            }
        }

		public int Count
		{
			get
			{
				lock (this)
				{
					return _normaljobs.Count + _hightJobs.Count + _lowJobs.Count;
				}
			}
		} 

        public void Dispose()
        {
            _normaljobs = null;
			_hightJobs = null;
			_lowJobs = null;
        }
    }
}
