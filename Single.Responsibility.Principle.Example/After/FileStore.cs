using System.IO;

namespace Single.Responsibility.Principle.Example.After
{
    public class FileStore
    {
        public void WriteAllText(string path, string message)
        {
            File.WriteAllText(path, message);
        }

        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        public string GetFileInfo(int id, string workingDirectory)
        {
            return Path.Combine(workingDirectory, id + ".txt");
        }
    }
}
