# Single.Responsibility.Principle.Example
Example of Breaking Class into Multiple Classes to Prove Single Responsibility Principle

Repository contains implementation of saving messages into files. First, one class(FileStore) implementation in folder "Before".
Then, Single Responsibility Principle was applied at class(FileStore) and was divide into three classes(MessageStore, FileStore and StoreLogger)

Absolutly identical unit tests was applied to both of implementation to prove that non behaviour was dropped.
