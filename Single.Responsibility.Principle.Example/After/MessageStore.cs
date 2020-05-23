using System;
using System.IO;

namespace Single.Responsibility.Principle.Example.After
{
    public class MessageStore
    {
        private readonly StoreLogger log;
        private readonly FileStore fileStore;
        private readonly string workingDirectory;

        public MessageStore(string workingDirectory)
        {
            if (workingDirectory == null)
                throw new ArgumentNullException(nameof(workingDirectory));
            if (!Directory.Exists(workingDirectory))
                throw new ArgumentException("Boo", nameof(workingDirectory));

            this.workingDirectory = workingDirectory;
            this.log = new StoreLogger();
            this.fileStore = new FileStore();
        }

        public void Save(int id, string message)
        {
            log.Saving(id);
            fileStore.WriteAllText(GetFileName(id), message);
            log.Saved(id);
        }

        public string Read(int id)
        {
            log.Reading(id);
            var path = this.GetFileName(id);

            if (!File.Exists(path))
            {
                log.DidNotFind(id);
                return string.Empty;
            }

            var message = fileStore.ReadAllText(path);
            log.Returning(id);
            return message;
        }

        public string GetFileName(int id)
        {
            return fileStore.GetFileInfo(id, workingDirectory);
        }
    }
}
