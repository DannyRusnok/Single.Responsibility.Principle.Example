using System;
using System.IO;

namespace BussinessLogic.After
{
    public class FileStore
    {
        private string workingDirectory;

        public FileStore(string workingDirectory)
        {
            if (workingDirectory == null)
                throw new ArgumentNullException(nameof(workingDirectory));
            if (!Directory.Exists(workingDirectory))
                throw new ArgumentException("Boo", nameof(workingDirectory));

            this.workingDirectory = workingDirectory;
        }

        public void WriteAllText(string path, string message)
        {
            File.WriteAllText(path, message);
        }

        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        public string GetFileInfo(int id)
        {
            return Path.Combine(workingDirectory, id + ".txt");
        }

        public bool Exists(string path)
        {
            return File.Exists(path);
        }
    }
}
