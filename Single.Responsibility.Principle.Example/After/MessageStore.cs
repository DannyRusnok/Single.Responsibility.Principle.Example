namespace BussinessLogic.After
{
    public class MessageStore
    {
        private readonly StoreLogger log;
        private readonly FileStore fileStore;

        public MessageStore(string workingDirectory)
        {
            this.log = new StoreLogger();
            this.fileStore = new FileStore(workingDirectory);
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
            var path = GetFileName(id);

            if (!fileStore.Exists(path))
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
            return fileStore.GetFileInfo(id);
        }
    }
}
