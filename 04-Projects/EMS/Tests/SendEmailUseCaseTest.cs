//// Test Project - SendEmailUseCaseTests.cs
//using EMS.Application.Interfaces;
//using EMS.Application.UseCases;
//using Moq;
//using Xunit;

//public class SendEmailUseCaseTest
//{
//    [Fact]
//    public async Task ExecuteAsync_ShouldReturnSuccess_WhenEmailIsSent()
//    {
//        // Arrange
//        var mockEmailService = new Mock<IEmailService>();
//        mockEmailService
//            .Setup(service => service.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
//            .ReturnsAsync(true); // Mock the email service to return true (success)

//        var sendEmailUseCase = new SendEmailUseCase(mockEmailService.Object);

//        var request = new SendEmailRequest
//        {
//            To = "test@example.com",
//            Subject = "Test Email",
//            Body = "This is a test email."
//        };

//        // Act
//        var response = await sendEmailUseCase.ExecuteAsync(request);

//        // Assert
//        Assert.True(response.Success);
//        Assert.Equal("Email sent successfully.", response.Message);
//        mockEmailService.Verify(service => service.SendEmailAsync(request.To, request.Subject, request.Body), Times.Once);
//    }

//    [Fact]
//    public async Task ExecuteAsync_ShouldReturnFailure_WhenEmailFailsToSend()
//    {
//        // Arrange
//        var mockEmailService = new Mock<IEmailService>();
//        mockEmailService
//            .Setup(service => service.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
//            .ReturnsAsync(false); // Mock the email service to return false (failure)

//        var sendEmailUseCase = new SendEmailUseCase(mockEmailService.Object);

//        var request = new SendEmailRequest
//        {
//            To = "test@example.com",
//            Subject = "Test Email",
//            Body = "This is a test email."
//        };

//        // Act
//        var response = await sendEmailUseCase.ExecuteAsync(request);

//        // Assert
//        Assert.False(response.Success);
//        Assert.Equal("Failed to send email.", response.Message);
//        mockEmailService.Verify(service => service.SendEmailAsync(request.To, request.Subject, request.Body), Times.Once);
//    }
//}