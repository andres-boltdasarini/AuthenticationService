namespace AuthenticationService
{
    public class Logger
    {
        private readonly string _logDirectory;
        private const string EventsFile = "events.txt";
        private const string ErrorsFile = "errors.txt";

        public Logger(IWebHostEnvironment env)
        {
            // Создаем уникальную папку для логов при запуске
            _logDirectory = Path.Combine(
                env.ContentRootPath,
                "Logs",
                DateTime.Now.ToString("yyyyMMdd_HHmmss")
            );
            Directory.CreateDirectory(_logDirectory);
        }

        public void WriteEvent(string eventMessage)
        {
            string filePath = Path.Combine(_logDirectory, EventsFile);
            File.AppendAllText(filePath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [EVENT] {eventMessage}\n");
        }

        public void WriteError(string errorMessage)
        {
            string filePath = Path.Combine(_logDirectory, ErrorsFile);
            File.AppendAllText(filePath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [ERROR] {errorMessage}\n");
        }
    }
}