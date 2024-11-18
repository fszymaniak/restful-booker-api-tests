using System;

namespace RestfulBooker.ApiTests.Services
{
    public interface ITestLogger
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message, Exception ex = null);
        void LogDebug(string message);
    }

    public class TestLogger : ITestLogger
    {
        private readonly bool _enableDebugLogging;

        public TestLogger(bool enableDebugLogging = false)
        {
            _enableDebugLogging = enableDebugLogging;
        }

        public void LogInfo(string message)
        {
            Console.WriteLine($"[INFO] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
        }

        public void LogWarning(string message)
        {
            Console.WriteLine($"[WARN] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
        }

        public void LogError(string message, Exception ex = null)
        {
            Console.WriteLine($"[ERROR] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
            if (ex != null)
            {
                Console.WriteLine($"[ERROR] Exception: {ex.Message}");
                Console.WriteLine($"[ERROR] StackTrace: {ex.StackTrace}");
            }
        }

        public void LogDebug(string message)
        {
            if (_enableDebugLogging)
            {
                Console.WriteLine($"[DEBUG] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
            }
        }
    }
}
