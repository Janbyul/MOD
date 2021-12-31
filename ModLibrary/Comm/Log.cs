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
    public static class Log
    {
        private static readonly ILog log = LogManager.GetLogger("Log"); // 로그 객체 설정 Iog4net.config 파일 참조

        /// <summary>
        /// Log Level Debug로 Log를 생성함
        /// ClassName 삽입 => System.Diagnostics.StackTrace 클래스 사용
        /// MethodName 삽입 => System.Runtime.CompilerServices.CallerMemberName 사용 (.net Framework 4.5 이후 System.Diagnostics.StackFrame 클래스에서 변경)
        /// </summary>
        /// <param name="Message">메세지</param>
        /// <param name="caller">호출 메서드 이름</param>
        public static void Debug(string Message, [CallerMemberName] string caller = "")
        {
            // 호출 Class명
            string ClassName = new StackTrace().GetFrame(1).GetMethod().ReflectedType.Name;
            // 호출 함수명 
            //string FuncName = new StackFrame(1, true).GetMethod().Name;

            log.Debug($"CLASS=[{ClassName}] FUNCTION=[{caller}] MESSAGE=[{Message}]");
        }

        public static void Info(string Message, [CallerMemberName] string caller = "")
        {
            string ClassName = new StackTrace().GetFrame(1).GetMethod().ReflectedType.Name;
            log.Info($"CLASS=[{ClassName}] FUNCTION=[{caller}] MESSAGE=[{Message}]");
        }

        public static void Warn(string Message, [CallerMemberName] string caller = "")
        {
            string ClassName = new StackTrace().GetFrame(1).GetMethod().ReflectedType.Name;
            log.Warn($"CLASS=[{ClassName}] FUNCTION=[{caller}] MESSAGE=[{Message}]");
        }

        public static void Error(string Message, [CallerMemberName] string caller = "")
        {
            string ClassName = new StackTrace().GetFrame(1).GetMethod().ReflectedType.Name;
            log.Error($"CLASS=[{ClassName}] FUNCTION=[{caller}] MESSAGE=[{Message}]");
        }

        public static void Fatal(string Message, [CallerMemberName] string caller = "")
        {
            string ClassName = new StackTrace().GetFrame(1).GetMethod().ReflectedType.Name;
            log.Fatal($"CLASS=[{ClassName}] FUNCTION=[{caller}] MESSAGE=[{Message}]");
        }
    }
}
