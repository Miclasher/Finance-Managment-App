using FinanceManagmentApp.Domain.Entities;
using FinanceManagmentApp.Services;
using FinanceManagmentApp.Shared;
using Moq;
using System.Security.Claims;
using FinanceManagmentApp.Services.Abstractions;

namespace FinanceManagmentApp.Tests
{
    [TestClass]
    public class TransactionTypeServiceTests : ServiceTestsBase
    {
        private TransactionTypeService _transactionTypeService = null!;
        private Mock<IJwtUtility> _mockJwtUtility = null!;
        private ClaimsPrincipal _user = null!;
        private Guid _userId;

        [TestInitialize]
        public void Initialize()
        {
            _mockJwtUtility = new Mock<IJwtUtility>();
            _transactionTypeService = new TransactionTypeService(_mockRepositoryManager.Object, _mockJwtUtility.Object);
            _userId = Guid.NewGuid();
            _user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, _userId.ToString())
            }));

            _mockJwtUtility.Setup(j => j.GetUserIdFromJwt(It.IsAny<ClaimsPrincipal>())).Returns(_userId);
        }

        [TestMethod]
        public async Task CreateAsyncTest1()
        {
            var transType = new TransactionTypeForCreateDTO { Name = "Test", IsExpense = true };

            await _transactionTypeService.CreateAsync(_user, transType);

            _mockTransactionTypeRepository.Verify(r => r.AddAsync(It.IsAny<TransactionType>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task CreateAsyncTest2()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _transactionTypeService.CreateAsync(null!, new TransactionTypeForCreateDTO()));
        }

        [TestMethod]
        public async Task DeleteAsyncTest1()
        {
            var transType = new TransactionType { Id = Guid.NewGuid(), UserId = _userId, FinancialOperations = new List<FinancialOperation>() };
            _mockTransactionTypeRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(transType);

            await _transactionTypeService.DeleteAsync(_user, transType.Id);

            _mockTransactionTypeRepository.Verify(r => r.Remove(It.IsAny<TransactionType>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsyncTest2()
        {
            _mockTransactionTypeRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))!.ReturnsAsync((TransactionType)null!);

            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => _transactionTypeService.DeleteAsync(_user, Guid.NewGuid()));
        }

        [TestMethod]
        public async Task DeleteAsyncTest3()
        {
            var transType = new TransactionType { Id = Guid.NewGuid(), UserId = _userId, FinancialOperations = new List<FinancialOperation> { new FinancialOperation() } };
            _mockTransactionTypeRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(transType);

            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _transactionTypeService.DeleteAsync(_user, transType.Id));
        }

        [TestMethod]
        public async Task GetAllAsyncTest1()
        {
            var transTypes = new List<TransactionType> { new TransactionType { Id = Guid.NewGuid(), Name = "Test", IsExpense = true, UserId = _userId } };
            _mockTransactionTypeRepository.Setup(r => r.GetAllByUserAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(transTypes);

            var result = await _transactionTypeService.GetAllAsync(_user);

            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public async Task GetByIdAsyncTest1()
        {
            var transType = new TransactionType { Id = Guid.NewGuid(), Name = "Test", IsExpense = true, UserId = _userId };
            _mockTransactionTypeRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(transType);

            var result = await _transactionTypeService.GetByIdAsync(_user, transType.Id);

            Assert.AreEqual(transType.Id, result.Id);
        }

        [TestMethod]
        public async Task GetByIdAsyncTest2()
        {
            _mockTransactionTypeRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))!.ReturnsAsync((TransactionType)null!);

            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => _transactionTypeService.GetByIdAsync(_user, Guid.NewGuid()));
        }

        [TestMethod]
        public async Task UpdateAsyncTest1()
        {
            var transType = new TransactionType { Id = Guid.NewGuid(), Name = "Test", IsExpense = true, UserId = _userId };
            _mockTransactionTypeRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(transType);

            var updateDto = new TransactionTypeForUpdateDTO { Id = transType.Id, Name = "Updated", IsExpense = false };

            await _transactionTypeService.UpdateAsync(_user, updateDto);

            _mockTransactionTypeRepository.Verify(r => r.Update(It.IsAny<TransactionType>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task UpdateAsyncTest2()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _transactionTypeService.UpdateAsync(_user, null!));
        }

        [TestMethod]
        public async Task UpdateAsyncTest3()
        {
            _mockTransactionTypeRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))!.ReturnsAsync((TransactionType)null!);

            var updateDto = new TransactionTypeForUpdateDTO { Id = Guid.NewGuid(), Name = "Updated", IsExpense = false };

            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => _transactionTypeService.UpdateAsync(_user, updateDto));
        }
    }
}
