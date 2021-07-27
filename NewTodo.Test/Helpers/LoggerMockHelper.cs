using System;
using Microsoft.Extensions.Logging;
using Moq;

namespace NewTodo.Test.Helpers
{
    public static class LoggerMockHelper
    {
        public static void VerifyLogger<T>(Mock<ILogger<T>> loggerMock,LogLevel logLevel, string logMessage)
        {
            loggerMock.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == logLevel),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Equals(logMessage)),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true))
                , Times.Once());
        }
    }
}