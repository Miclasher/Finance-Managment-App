using FinanceManagmentApp.Domain.Entities;
using FinanceManagmentApp.Services;
using FinanceManagmentApp.Services.Abstractions;
using Moq;
using System.Security.Claims;

namespace FinanceManagmentApp.Tests
{
    [TestClass]
    public class SummaryServiceTests : ServiceTestsBase
    {
        private SummaryService _summaryService = null!;
        private Mock<IJwtUtility> _mockJwtUtility = null!;
        private ClaimsPrincipal _user = null!;

        [TestInitialize]
        public void Initialize()
        {
            _mockJwtUtility = new Mock<IJwtUtility>();
            _summaryService = new SummaryService(_mockJwtUtility.Object, _mockRepositoryManager.Object);
            _user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            }));
        }

        [TestMethod]
        public async Task GetDateRangeSummaryAsyncTest1()
        {
            var userId = Guid.NewGuid();
            var startDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-7));
            var endDate = DateOnly.FromDateTime(DateTime.UtcNow);
            var finOps = new List<FinancialOperation>
            {
                new FinancialOperation { Id = Guid.NewGuid(), UserId = userId, Amount = 100, Date = DateTime.UtcNow, TransactionType = new TransactionType { IsExpense = false } },
                new FinancialOperation { Id = Guid.NewGuid(), UserId = userId, Amount = 50, Date = DateTime.UtcNow, TransactionType = new TransactionType { IsExpense = true } }
            };

            _mockJwtUtility.Setup(j => j.GetUserIdFromJwt(It.IsAny<ClaimsPrincipal>())).Returns(userId);
            _mockFinancialOperationRepository.Setup(r => r.GetAllByUserAndDateRangeAsync(userId, startDate, endDate, It.IsAny<CancellationToken>())).ReturnsAsync(finOps);

            var result = await _summaryService.GetDateRangeSummaryAsync(_user, startDate, endDate);

            Assert.AreEqual(100, result.TotalIncome);
            Assert.AreEqual(50, result.TotalExpense);
            Assert.AreEqual(2, result.Operations.Count());
        }

        [TestMethod]
        public async Task GetDateRangeSummaryAsyncTest2()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await _summaryService.GetDateRangeSummaryAsync(null!, DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-7)), DateOnly.FromDateTime(DateTime.UtcNow)));
        }

        [TestMethod]
        public async Task GetDaySummaryAsyncTest1()
        {
            var userId = Guid.NewGuid();
            var date = DateOnly.FromDateTime(DateTime.UtcNow);
            var finOps = new List<FinancialOperation>
            {
                new FinancialOperation { Id = Guid.NewGuid(), UserId = userId, Amount = 100, Date = DateTime.UtcNow, TransactionType = new TransactionType { IsExpense = false } },
                new FinancialOperation { Id = Guid.NewGuid(), UserId = userId, Amount = 50, Date = DateTime.UtcNow, TransactionType = new TransactionType { IsExpense = true } }
            };

            _mockJwtUtility.Setup(j => j.GetUserIdFromJwt(It.IsAny<ClaimsPrincipal>())).Returns(userId);
            _mockFinancialOperationRepository.Setup(r => r.GetAllByUserAndDateRangeAsync(userId, date, date, It.IsAny<CancellationToken>())).ReturnsAsync(finOps);

            var result = await _summaryService.GetDaySummaryAsync(_user, date);

            Assert.AreEqual(100, result.TotalIncome);
            Assert.AreEqual(50, result.TotalExpense);
            Assert.AreEqual(2, result.Operations.Count());
        }

        [TestMethod]
        public async Task GetDaySummaryAsyncTest2()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await _summaryService.GetDaySummaryAsync(null!, DateOnly.FromDateTime(DateTime.UtcNow)));
        }
    }
}
