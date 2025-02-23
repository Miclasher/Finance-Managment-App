using FinanceManagmentApp.Domain.Entities;
using FinanceManagmentApp.Services;
using FinanceManagmentApp.Services.Abstractions;
using FinanceManagmentApp.Shared;
using Moq;
using System.Security.Claims;

namespace FinanceManagmentApp.Tests
{
    [TestClass]
    public class FinancialOperationServiceTests : ServiceTestsBase
    {
        private FinancialOperationService _financialOperationService = null!;
        private Mock<IJwtUtility> _mockJwtUtility = null!;
        private ClaimsPrincipal _user = null!;

        [TestInitialize]
        public void Initialize()
        {
            _mockJwtUtility = new Mock<IJwtUtility>();
            _financialOperationService = new FinancialOperationService(_mockRepositoryManager.Object, _mockJwtUtility.Object);
            _user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            }));
        }

        [TestMethod]
        public async Task CreateAsyncTest1()
        {
            var finOp = new FinancialOperationForCreateDTO { Amount = 100, Date = DateTime.UtcNow, TransactionTypeId = Guid.NewGuid() };
            _mockJwtUtility.Setup(j => j.GetUserIdFromJwt(It.IsAny<ClaimsPrincipal>())).Returns(Guid.NewGuid());
            _mockTransactionTypeRepository.Setup(r => r.GetAllByUserAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(new List<TransactionType> { new TransactionType { Id = finOp.TransactionTypeId } });

            await _financialOperationService.CreateAsync(_user, finOp);

            _mockFinancialOperationRepository.Verify(r => r.AddAsync(It.IsAny<FinancialOperation>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task CreateAsyncTest2()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await _financialOperationService.CreateAsync(_user, null!));
        }

        [TestMethod]
        public async Task DeleteAsyncTest1()
        {
            var finOp = new FinancialOperation { Id = Guid.NewGuid(), UserId = Guid.NewGuid() };
            _mockJwtUtility.Setup(j => j.GetUserIdFromJwt(It.IsAny<ClaimsPrincipal>())).Returns(finOp.UserId);
            _mockFinancialOperationRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(finOp);

            await _financialOperationService.DeleteAsync(_user, finOp.Id);

            _mockFinancialOperationRepository.Verify(r => r.Remove(It.IsAny<FinancialOperation>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsyncTest2()
        {
            _mockFinancialOperationRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))!.ReturnsAsync((FinancialOperation)null!);

            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(async () => await _financialOperationService.DeleteAsync(_user, Guid.NewGuid()));
        }

        [TestMethod]
        public async Task DeleteAsyncTest3()
        {
            var finOp = new FinancialOperation { Id = Guid.NewGuid(), UserId = Guid.NewGuid() };
            _mockJwtUtility.Setup(j => j.GetUserIdFromJwt(It.IsAny<ClaimsPrincipal>())).Returns(Guid.NewGuid());
            _mockFinancialOperationRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(finOp);

            await Assert.ThrowsExceptionAsync<AccessViolationException>(async () => await _financialOperationService.DeleteAsync(_user, finOp.Id));
        }

        [TestMethod]
        public async Task GetAllAsyncTest1()
        {
            var userId = Guid.NewGuid();
            var finOps = new List<FinancialOperation> { new FinancialOperation { Id = Guid.NewGuid(), UserId = userId } };
            _mockJwtUtility.Setup(j => j.GetUserIdFromJwt(It.IsAny<ClaimsPrincipal>())).Returns(userId);
            _mockFinancialOperationRepository.Setup(r => r.GetAllByUserAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(finOps);

            var result = await _financialOperationService.GetAllAsync(_user);

            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public async Task GetByIdAsyncTest1()
        {
            var userId = Guid.NewGuid();
            var finOp = new FinancialOperation { Id = Guid.NewGuid(), UserId = userId };
            _mockJwtUtility.Setup(j => j.GetUserIdFromJwt(It.IsAny<ClaimsPrincipal>())).Returns(userId);
            _mockFinancialOperationRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(finOp);

            var result = await _financialOperationService.GetByIdAsync(_user, finOp.Id);

            Assert.AreEqual(finOp.Id, result.Id);
        }

        [TestMethod]
        public async Task GetByIdAsyncTest2()
        {
            _mockFinancialOperationRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))!.ReturnsAsync((FinancialOperation)null!);

            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(async () => await _financialOperationService.GetByIdAsync(_user, Guid.NewGuid()));
        }

        [TestMethod]
        public async Task GetByIdAsyncTest3()
        {
            var finOp = new FinancialOperation { Id = Guid.NewGuid(), UserId = Guid.NewGuid() };
            _mockJwtUtility.Setup(j => j.GetUserIdFromJwt(It.IsAny<ClaimsPrincipal>())).Returns(Guid.NewGuid());
            _mockFinancialOperationRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(finOp);

            await Assert.ThrowsExceptionAsync<AccessViolationException>(async () => await _financialOperationService.GetByIdAsync(_user, finOp.Id));
        }

        [TestMethod]
        public async Task UpdateAsyncTest1()
        {
            var userId = Guid.NewGuid();
            var finOp = new FinancialOperation { Id = Guid.NewGuid(), UserId = userId };
            _mockJwtUtility.Setup(j => j.GetUserIdFromJwt(It.IsAny<ClaimsPrincipal>())).Returns(userId);
            _mockFinancialOperationRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(finOp);

            var updateDto = new FinancialOperationForUpdateAndSummaryDTO { Id = finOp.Id, Amount = 200, Date = DateTime.UtcNow, TransactionTypeId = Guid.NewGuid() };

            await _financialOperationService.UpdateAsync(_user, updateDto);

            _mockFinancialOperationRepository.Verify(r => r.Update(It.IsAny<FinancialOperation>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task UpdateAsyncTest2()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await _financialOperationService.UpdateAsync(_user, null!));
        }

        [TestMethod]
        public async Task UpdateAsyncTest3()
        {
            _mockFinancialOperationRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))!.ReturnsAsync((FinancialOperation)null!);

            var updateDto = new FinancialOperationForUpdateAndSummaryDTO { Id = Guid.NewGuid(), Amount = 200, Date = DateTime.UtcNow, TransactionTypeId = Guid.NewGuid() };

            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(async () => await _financialOperationService.UpdateAsync(_user, updateDto));
        }

        [TestMethod]
        public async Task UpdateAsyncTest4()
        {
            var finOp = new FinancialOperation { Id = Guid.NewGuid(), UserId = Guid.NewGuid() };
            _mockJwtUtility.Setup(j => j.GetUserIdFromJwt(It.IsAny<ClaimsPrincipal>())).Returns(Guid.NewGuid());
            _mockFinancialOperationRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(finOp);

            var updateDto = new FinancialOperationForUpdateAndSummaryDTO { Id = finOp.Id, Amount = 200, Date = DateTime.UtcNow, TransactionTypeId = Guid.NewGuid() };

            await Assert.ThrowsExceptionAsync<AccessViolationException>(async () => await _financialOperationService.UpdateAsync(_user, updateDto));
        }
    }
}
