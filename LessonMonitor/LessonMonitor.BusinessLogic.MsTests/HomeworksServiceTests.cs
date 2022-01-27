using Microsoft.VisualStudio.TestTools.UnitTesting;
using LessonMonitor.Core;
using Moq;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using AutoFixture;
using FluentAssertions;

namespace LessonMonitor.BusinessLogic.MsTests
{
    [TestClass]
    public class HomeworksServiceTests
    {
        private readonly Mock<IHomeworksRepository> _homeworksRepositoryMock;
        private HomeworksService _service;

        public HomeworksServiceTests()
        {
            _homeworksRepositoryMock = new Mock<IHomeworksRepository>();
            _service = new HomeworksService(_homeworksRepositoryMock.Object);
        }

        //[TestInitialize]
        //public void Setup()
        //{
        //    _guidTestInitialize = Guid.NewGuid().ToString() + "testInitialize";
        //}

        //private void Log()
        //{
        //    Logger.LogMessage(_guidStor);
        //    Logger.LogMessage(_guidTestInitialize);

        //}

        [TestMethod]

        public void Create_HomeworkIsValid_ShouldCreateNewHomework()
        {
            // arrange - preparing datas                
            var fixture = new Fixture();

            var homework = fixture.Build<Homework>()
                .Without(x => x.MentorId)
                .Create();
            // act - start test code
            var result = _service.Create(homework);

            // assert - checking/validating test results
            result.Should().BeTrue();
            Assert.IsTrue(result);
            _homeworksRepositoryMock.Verify(x => x.Add(homework), Times.Once);
        }

        [TestMethod]
        public void Create_HomeworkIsNull_ShouldThrowArgumentNullException()
        {
            // arrange - preparing datas

            Homework homework = null;

            // act - start test code
            bool result = false;

            var exception = Assert.ThrowsException<ArgumentNullException>(() => result = _service.Create(homework));
            // assert - checking/validating test results
            exception.Should().NotBeNull()
                .And
                .Match<ArgumentNullException>(x => x.ParamName == "homework");

            Assert.IsFalse(result);
            _homeworksRepositoryMock.Verify(x => x.Add(homework), Times.Never);
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(-2)]
        [DataRow(-12312)]
        public void Create_HomeworkIsInvalid_ShouldThrowBusinessException(int memberId)
        {
            // arrange
            var homework = new Homework();
            homework.MemberId = memberId;
            // act 
            bool result = false;
            var exception = Assert.ThrowsException<BusinessException>(() => result = _service.Create(homework));            // assert

            // assert
            exception.Should().NotBeNull()
                .And
                .Match<BusinessException>(x => x.Message == HomeworksService.HOMEWORK_IS_INVALID);

            result.Should().BeFalse();

            //Assert.IsNotNull(exception);
            //Assert.AreEqual(HomeworksService.HOMEWORK_IS_INVALID, exception.Message);
            Assert.IsFalse(result);
            _homeworksRepositoryMock.Verify(x => x.Add(homework), Times.Never);

        }

        [TestMethod]
        public void Delete_ShouldDeleteHomework()
        {
            // arrange
            var homeworkId = 1;
            // act
            var result = _service.Delete(homeworkId);

            // assert
            Assert.IsTrue(result);
            _homeworksRepositoryMock.Verify(x => x.Delete(homeworkId), Times.Once);
        }
    }
}
