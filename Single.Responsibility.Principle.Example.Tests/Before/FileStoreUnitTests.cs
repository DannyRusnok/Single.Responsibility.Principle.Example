using System;
using System.IO;
using AutoFixture;
using BussinessLogic.Before;
using FluentAssertions;
using Xunit;

namespace Tests.Before
{
    public class FileStoreUnitTests
    {
        private readonly Fixture fixture;

        public FileStoreUnitTests()
        {
             fixture = new Fixture();
        }

        [Fact]
        public void When_Saving_Message_Then_Message_Should_Be_Stored_In_Specified_File()
        {
            //Arrange
            string expected = fixture.Create<string>();
            var fileStore = new FileStore(Environment.CurrentDirectory);

            //Act
            fileStore.Save(44, expected);

            //Assert
            var textStoredInFile = File.ReadAllText(fileStore.GetFileName(44));
            textStoredInFile.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void When_Message_Is_Stored_In_File_Then_Read_Should_Be_Able_To_Return_it()
        {
            //Arrange
            string expected = fixture.Create<string>();
            var fileStore = new FileStore(Environment.CurrentDirectory);
            fileStore.Save(44, expected);

            //Act
            var actual = fileStore.Read(44);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void When_Try_To_Read_NonExisting_File_Then_Empty_String_Should_Be_Return()
        {
            //Arrange
            var fileStore = new FileStore(Environment.CurrentDirectory);

            //Act
            var actual = fileStore.Read(45);

            //Assert
            actual.Should().BeEquivalentTo(String.Empty);
        }

        [Fact]
        public void When_Getting_File_Name_Then_File_Name_Should_Be_In_Expected_Format()
        {
            //Arrange
            int id = fixture.Create<int>();
            var fileStore = new FileStore(Environment.CurrentDirectory);

            //Act
            string actual = fileStore.GetFileName(id);

            //Assert
            var expected = Path.Combine(fileStore.WorkingDirectory, id + ".txt");
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void When_Contructing_FileStore_With_Null_As_DirectoryPath_Then_ArgumentNullException_Should_be_Thrown()
        {
            //Arrange
            //Act
            Func<FileStore> act = () => new FileStore(null);

            //Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void When_Contructing_FileStore_With_NonExisting_DirectoryPath_Then_ArgumentlException_Should_be_Thrown()
        {
            //Arrange
            string invalidDirectory = fixture.Create<string>();

            //Act
            Func<FileStore> act = () => new FileStore(invalidDirectory);

            //Assert
            act.Should().Throw<ArgumentException>();
        }
    }
}
