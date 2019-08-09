using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace INC.Runtime.Queue
{
    public class JobPrioritySchedule : IJobPrioritySchedule
    {
        private IJobPriorityScheduleConfig _config;
        private System.Timers.Timer _timer;
        public event ElapsedEventHandler CallBack;

        public JobPrioritySchedule(IJobPriorityScheduleConfig config)
        {
            this._config = config;
        }

        public IJobPriorityScheduleConfig Config
        {
            get
            {
                return this._config;
            }
            set
            {
                this._config = value;
            }
        }
        
        public void Start()
        {
            _timer = new System.Timers.Timer(this._config.GetTimeInterval());
            _timer.AutoReset = true;
            _timer.Elapsed += this.ElapsedEventHandle;
            _timer.Start();
        }

        private void ElapsedEventHandle(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            try
            {
                if (this.CallBack != null)
                    this.CallBack(sender, e);
            }
            finally
            {
                _timer.Interval = this._config.GetTimeInterval();
                _timer.Start();
            }
        }

        public void Stop()
        {
            this._timer.Stop();
            this._timer.Dispose();
        }
    }
}
