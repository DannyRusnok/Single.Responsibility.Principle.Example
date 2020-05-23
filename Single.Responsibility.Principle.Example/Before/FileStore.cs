using System;
using System.IO;
using NLog.Fluent;

namespace BussinessLogic.Before
{
    public class FileStore
    {
        public FileStore(string workingDirectory)
        {
            if (workingDirectory == null)
                throw new ArgumentNullException(nameof(workingDirectory));
            if (!Directory.Exists(workingDirectory))
                throw new ArgumentException("Boo", nameof(workingDirectory));

            this.WorkingDirectory = workingDirectory;
        }

        public string WorkingDirectory { get; }

        public void Save(int id, string message)
        {
            Log.Info($"Saving message {id}");
            var path = this.GetFileName(id);
            File.WriteAllText(path, message);
            Log.Info($"Saved message {id}");
        }

        public string Read(int id)
        {
            Log.Debug($"Reading message {id}.");
            var path = this.GetFileName(id);

            if (!File.Exists(path))
            {
                Log.Debug($"No message {id} found.");
                return string.Empty;
            }

            var message = File.ReadAllText(path);
            Log.Debug($"Returning message {id}.");
            return message;
        }

        public string GetFileName(int id)
        {
            return Path.Combine(this.WorkingDirectory, id + ".txt");
        }
    }
}
