namespace ProjectRLG.Infrastructure
{
    using System;
    using System.IO;
    using System.Text;

    public static class Logger
    {
        private const string LOG_FOLDER = "E:/ProjectRLG.Logs/";

        private static readonly Encoding FILE_ENCODING = Encoding.ASCII;
        private static readonly string DATE_SUFFIX = DateTime.Now.ToString("dd.MM.yy");
        private static readonly string DEBUG_FILE_PATH = string.Format("{0}debug-{1}.txt", LOG_FOLDER, DATE_SUFFIX);
        private static readonly string INFO_FILE_PATH = string.Format("{0}info-{1}.txt", LOG_FOLDER, DATE_SUFFIX);
        private static readonly string WARN_FILE_PATH = string.Format("{0}warn-{1}.txt", LOG_FOLDER, DATE_SUFFIX);
        private static readonly string ERROR_FILE_PATH = string.Format("{0}error-{1}.txt", LOG_FOLDER, DATE_SUFFIX);

        static Logger()
        {
            CheckForFilesAndCreate();
        }

        public static void Debug(string text)
        {
            string timeStamp = DateTime.Now.ToString("HH:mm:ss");
            using (StreamWriter sWriter = new StreamWriter(DEBUG_FILE_PATH, true, FILE_ENCODING))
            {
                if (!string.IsNullOrEmpty(text))
                    sWriter.WriteLine(text);
            }
        }
        public static void Info(string text)
        {
            string timeStamp = DateTime.Now.ToString("HH:mm:ss");
            using (StreamWriter sWriter = new StreamWriter(INFO_FILE_PATH, true, FILE_ENCODING))
            {
                if (!string.IsNullOrEmpty(text))
                    sWriter.WriteLine(text);
            }
        }
        public static void Warn(string text)
        {
            string timeStamp = DateTime.Now.ToString("HH:mm:ss");
            using (StreamWriter sWriter = new StreamWriter(WARN_FILE_PATH, true, FILE_ENCODING))
            {
                if (!string.IsNullOrEmpty(text))
                    sWriter.WriteLine(text);
            }
        }
        public static void Error(Exception ex, string text)
        {
            string timeStamp = DateTime.Now.ToString("HH:mm:ss");
            using (StreamWriter sWriter = new StreamWriter(DEBUG_FILE_PATH, true, FILE_ENCODING))
            {
                sWriter.WriteLine("---An error has occured on [{0}].---", timeStamp);
                sWriter.WriteLine(ex.ToString());
                if (!string.IsNullOrEmpty(text))
                    sWriter.WriteLine("--- text: {0}.---", text);

                sWriter.WriteLine("---error---");
            }
        }
        public static void CheckForFilesAndCreate(bool rewrite = false)
        {
            if (rewrite)
            {
                CreateEmptyFile(DEBUG_FILE_PATH);
                CreateEmptyFile(INFO_FILE_PATH);
                CreateEmptyFile(WARN_FILE_PATH);
                CreateEmptyFile(ERROR_FILE_PATH);
            }
            else
            {
                if (!File.Exists(DEBUG_FILE_PATH))
                {
                    CreateEmptyFile(DEBUG_FILE_PATH);
                }

                if (!File.Exists(INFO_FILE_PATH))
                {
                    CreateEmptyFile(INFO_FILE_PATH);
                }

                if (!File.Exists(WARN_FILE_PATH))
                {
                    CreateEmptyFile(WARN_FILE_PATH);
                }

                if (!File.Exists(ERROR_FILE_PATH))
                {
                    CreateEmptyFile(ERROR_FILE_PATH);
                }
            }
        }

        private static void CreateEmptyFile(string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.Create(path).Dispose();
        }
    }
}
