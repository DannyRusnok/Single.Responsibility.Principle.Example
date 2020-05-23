using NLog.Fluent;

namespace Single.Responsibility.Principle.Example.After
{
    public class StoreLogger
    {
        public void Saving(int id)
        {
            Log.Info($"Saving message {id}.");
        }

        public void Saved(int id)
        {
            Log.Info($"Saved message {id}.");
        }

        public void Reading(int id)
        {
            Log.Debug($"Reading message {id}.");
        }

        public void DidNotFind(int id)
        {
            Log.Debug($"No message {id} found.");
        }

        public void Returning(int id)
        {
            Log.Debug($"Returning message {id}.");
        }
    }
}
