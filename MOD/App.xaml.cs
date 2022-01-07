using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;

using ModLibrary.Comm;

namespace MOD
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        private static Mutex _mutex;

        public static readonly Log log = new Log();

        public static LogLevel RealTimeMinLogLevel { get; set; } = LogLevel.FATAL;
        public static List<LogModel> RealTimeLogModel;

        protected override void OnStartup(StartupEventArgs e)
        {
            string mutexName = "XAK";

            _mutex = new Mutex(true, mutexName, out bool createNew);

            if (!createNew)
            {
                _mutex = null;
                Shutdown();
            }

            log.Info("프로그램 시작");

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            log.Info("프로그램 종료");
            _mutex = null;
            base.OnExit(e);
        }
    }
}
