using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using INC.Runtime.Queue.Delegate;

namespace INC.Runtime.Queue
{
	/// <summary>
	/// Job , not paramter not result
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public sealed class Job : JobBase
	{
		private Action action;
		public Job(Action action, Action<JobBase> jobComplateCallback, JobPriority priority = JobPriority.NORMAL)
			: base(priority, jobComplateCallback)
		{
			this.action = action;
		}

		public Job(Action action,  JobPriority priority = JobPriority.NORMAL)
			: this(action, null, priority)
		{
		}

		public override void DoAction()
		{
			this.action();
		}
	}

	/// <summary>
	/// Job , have paramter not result
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public sealed class Job<T> : JobBase
	{
		private Action<T> action;

		public Job(T data, Action<T> action, Action<JobBase> jobComplateCallback, JobPriority priority = JobPriority.NORMAL)
			: base(priority, jobComplateCallback)
		{
			this.Data = data;
			this.action = action;
		}

		public Job(T data, Action<T> action, JobPriority priority = JobPriority.NORMAL)
			: this(data, action, null, priority)
		{
		}

		public T Data { get; private set; }

		public override void DoAction()
		{
			this.action(Data);
		}
	}

	/// <summary>
	/// Job , have paramter have result
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public sealed class Job<T1, T2> : JobBase, INotifyJobResultChange<T2>
	{
		private Func<T1, T2> _action;
		private T2 _result;
		public event JobDelegate.JobResultChangeEventHandler<T2> OnResultChange;

		public Job(T1 data, Func<T1, T2> action, Action<JobBase> jobComplateCallback, JobPriority priority = JobPriority.NORMAL)
			: base(priority, jobComplateCallback)
		{
			this.Data = data;
			this._action = action;
		}

		public Job(T1 data, Func<T1, T2> action, JobPriority priority = JobPriority.NORMAL)
			: this(data, action, null, priority)
		{
		}

		public T1 Data { get; private set; }


		public T2 Result
		{
			get
			{
				return _result;
			}
			private set
			{
				_result = value;
				if (this.OnResultChange != null)
					this.OnResultChange.Invoke(this, new JobResultChangeEventArgs<T2>() { JobResult = value });
			}
		}

		public override void DoAction()
		{
			this.Result = _action(this.Data);
		}
	}

	public abstract class JobBase
	{
		public JobBase(JobPriority priority = JobPriority.NORMAL, Action<JobBase> jobComplateCallback = null)
		{
			this.Id = Guid.NewGuid();
			this.Priority = priority;
			this.CompleteCallback = jobComplateCallback;
		}

		public Guid Id { get; private set; }

		public JobPriority Priority { get; private set; }

		public bool IsFault { get; private set; }

		public Exception Exception { get; private set; }

		/// <summary>
		/// set fault 
		/// </summary>
		/// <param name="ex"></param>
		public void SetFault(Exception ex)
		{
			this.IsFault = true;
			this.Exception = ex;
		}

		/// <summary>
		/// Execute job action
		/// </summary>
		public abstract void DoAction();

		public Action<JobBase> CompleteCallback { get; set; }
	}

	public enum JobPriority
	{
		NORMAL = 0,
		LOWEST = 1,
		HIGHEST = 2,
	}
}
