using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using log4net;

namespace ModLibrary.Comm
{
    /// <summary>
    /// Log 생성 클래스
    /// log4net 사용, log4net.config 참조, AssmblyInfo.cs 바인딩
    /// 2021-12-31 Debug, Info, Warn, Error, Fatal 기능 구현
    /// </summary>
    public class Log
    {
        private readonly ILog log = LogManager.GetLogger(""); // 로그 객체 설정 Iog4net.config 파일 참조

        public event Action<LogModel> LogEvent;

        /// <summary>
        /// Log Level Debug로 Log를 생성함
        /// ClassName 삽입 => System.Diagnostics.StackTrace 클래스 사용
        /// MethodName 삽입 => System.Runtime.CompilerServices.CallerMemberName 사용 (.net Framework 4.5 이후 System.Diagnostics.StackFrame 클래스에서 변경)
        /// </summary>
        /// <param name="Message">메세지</param>
        /// <param name="caller">호출 메서드 이름</param>
        public void Debug(string message, [CallerMemberName] string caller = "")
        {
#if DEBUG
            // 로그 시간을 최우선으로 가져옴
            DateTime LogTime = DateTime.Now;

            // 호출 Class명
            string className = new StackTrace().GetFrame(1).GetMethod().ReflectedType.Name;
            // 호출 함수명 
            //string FuncName = new StackFrame(1, true).GetMethod().Name;

            // JSON 형식으로 Log 생성
            log.Debug($"'DATE':'{LogTime:yyyy-MM-dd HH:mm:ss.fff}', 'CLASS':'{className}', 'FUNCTION':'{caller}' 'MESSAGE':'{message}'");

            // 실시간 로그 기능 구현을 위한 이벤트 전달
            LogEvent?.Invoke(new LogModel() { Date = LogTime, Level = LogLevel.DEBUG, ClassName = className, Function = caller, Message = message });
#endif
        }

        public void Info(string message, [CallerMemberName] string caller = "")
        {
            DateTime LogTime = DateTime.Now;
            string className = new StackTrace().GetFrame(1).GetMethod().ReflectedType.Name;
            log.Info($"'DATE':'{LogTime:yyyy-MM-dd HH:mm:ss.fff}', 'CLASS':'{className}', 'FUNCTION':'{caller}' 'MESSAGE':'{message}'");
            LogEvent?.Invoke(new LogModel() { Date = LogTime, Level = LogLevel.INFO, ClassName = className, Function = caller, Message = message });
        }

        public void Warn(string message, [CallerMemberName] string caller = "")
        {
            DateTime LogTime = DateTime.Now;
            string className = new StackTrace().GetFrame(1).GetMethod().ReflectedType.Name;
            log.Warn($"'DATE':'{LogTime:yyyy-MM-dd HH:mm:ss.fff}', 'CLASS':'{className}', 'FUNCTION':'{caller}' 'MESSAGE':'{message}'");
            LogEvent.Invoke(new LogModel() { Date = LogTime, Level = LogLevel.WARN, ClassName = className, Function = caller, Message = message });
        }

        public void Error(string message, [CallerMemberName] string caller = "")
        {
            DateTime LogTime = DateTime.Now;
            string className = new StackTrace().GetFrame(1).GetMethod().ReflectedType.Name;
            log.Error($"'DATE':'{LogTime:yyyy-MM-dd HH:mm:ss.fff}', 'CLASS':'{className}', 'FUNCTION':'{caller}' 'MESSAGE':'{message}'");
            LogEvent.Invoke(new LogModel() { Date = LogTime, Level = LogLevel.ERROR, ClassName = className, Function = caller, Message = message });
        }

        public void Fatal(string message, [CallerMemberName] string caller = "")
        {
            DateTime LogTime = DateTime.Now;
            string className = new StackTrace().GetFrame(1).GetMethod().ReflectedType.Name;
            log.Fatal($"'DATE':'{LogTime:yyyy-MM-dd HH:mm:ss.fff}', 'CLASS':'{className}', 'FUNCTION':'{caller}' 'MESSAGE':'{message}'");
            LogEvent.Invoke(new LogModel() { Date = LogTime, Level = LogLevel.FATAL, ClassName = className, Function = caller, Message = message });
        }
    }
    
    public class LogModel
    {
        public DateTime Date { get; set; }
        public LogLevel Level { get; set; }
        public string ClassName { get; set; }
        public string Function { get; set; }
        public string Message { get; set; }
    }

    public enum LogLevel
    {
        DEBUG = 0,
        INFO = 1,
        WARN = 2,
        ERROR = 3,
        FATAL = 4
    }
}
