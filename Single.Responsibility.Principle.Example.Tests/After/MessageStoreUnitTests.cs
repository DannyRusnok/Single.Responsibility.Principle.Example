using System;
using System.IO;
using AutoFixture;
using FluentAssertions;
using Single.Responsibility.Principle.Example.After;
using Xunit;

namespace Single.Responsibility.Principle.Example.Tests.After
{
    public class MessageStoreUnitTests
    {
        private readonly Fixture fixture;

        public MessageStoreUnitTests()
        {
             fixture = new Fixture();
        }

        [Fact]
        public void When_Saving_Message_Then_Message_Should_Be_Stored_In_Specified_File()
        {
            //Arrange
            string expected = fixture.Create<string>();
            var messageStore = new MessageStore(Environment.CurrentDirectory);

            //Act
            messageStore.Save(50, expected);

            //Assert
            var textStoredInFile = File.ReadAllText(messageStore.GetFileName(50));
            textStoredInFile.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void When_Message_Is_Stored_In_File_Then_Read_Should_Be_Able_To_Return_it()
        {
            //Arrange
            string expected = fixture.Create<string>();
            var messageStore = new MessageStore(Environment.CurrentDirectory);
            messageStore.Save(50, expected);

            //Act
            var actual = messageStore.Read(50);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void When_Try_To_Read_NonExisting_File_Then_Empty_String_Should_Be_Return()
        {
            //Arrange
            var messageStore = new MessageStore(Environment.CurrentDirectory);

            //Act
            var actual = messageStore.Read(55);

            //Assert
            actual.Should().BeEquivalentTo(String.Empty);
        }

        [Fact]
        public void When_Getting_File_Name_Then_File_Name_Should_Be_In_Expected_Format()
        {
            //Arrange
            int id = fixture.Create<int>();
            var messageStore = new MessageStore(Environment.CurrentDirectory);

            //Act
            string actual = messageStore.GetFileName(id);

            //Assert
            var expected = Path.Combine(Environment.CurrentDirectory, id + ".txt");
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void When_Contructing_MessageStore_With_Null_As_DirectoryPath_Then_ArgumentNullException_Should_be_Thrown()
        {
            //Arrange
            //Act
            Func<MessageStore> act = () => new MessageStore(null);

            //Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void When_Contructing_MessageStore_With_NonExisting_DirectoryPath_Then_ArgumentlException_Should_be_Thrown()
        {
            //Arrange
            string invalidDirectory = fixture.Create<string>();

            //Act
            Func<MessageStore> act = () => new MessageStore(invalidDirectory);

            //Assert
            act.Should().Throw<ArgumentException>();
        }
    }
}
