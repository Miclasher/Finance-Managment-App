using FinanceManagmentApp.Domain.Entities;
using FinanceManagmentApp.Services;
using FinanceManagmentApp.Shared;
using Moq;

namespace FinanceManagmentApp.Tests
{
    [TestClass]
    public class TransactionTypeServiceTests : ServiceTestsBase
    {
        private TransactionTypeService _transactionTypeService = null!;

        [TestInitialize]
        public void Initialize()
        {
            _transactionTypeService = new TransactionTypeService(_mockRepositoryManager.Object);
        }

        [TestMethod]
        public async Task CreateAsyncTest1()
        {
            var transType = new TransactionTypeForCreateDTO { Name = "Test", IsExpense = true };

            await _transactionTypeService.CreateAsync(transType);

            _mockTransactionTypeRepository.Verify(r => r.AddAsync(It.IsAny<TransactionType>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task CreateAsyncTest2()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _transactionTypeService.CreateAsync(null!));
        }

        [TestMethod]
        public async Task DeleteAsyncTest1()
        {
            var transType = new TransactionType { Id = Guid.NewGuid(), FinancialOperations = new List<FinancialOperation>() };
            _mockTransactionTypeRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(transType);

            await _transactionTypeService.DeleteAsync(transType.Id);

            _mockTransactionTypeRepository.Verify(r => r.Remove(It.IsAny<TransactionType>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsyncTest2()
        {
            _mockTransactionTypeRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))!.ReturnsAsync((TransactionType)null!);

            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => _transactionTypeService.DeleteAsync(Guid.NewGuid()));
        }

        [TestMethod]
        public async Task DeleteAsyncTest3()
        {
            var transType = new TransactionType { Id = Guid.NewGuid(), FinancialOperations = new List<FinancialOperation> { new FinancialOperation() } };
            _mockTransactionTypeRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(transType);

            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _transactionTypeService.DeleteAsync(transType.Id));
        }

        [TestMethod]
        public async Task GetAllAsyncTest1()
        {
            var transTypes = new List<TransactionType> { new TransactionType { Id = Guid.NewGuid(), Name = "Test", IsExpense = true } };
            _mockTransactionTypeRepository.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(transTypes);

            var result = await _transactionTypeService.GetAllAsync();

            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public async Task GetByIdAsyncTest1()
        {
            var transType = new TransactionType { Id = Guid.NewGuid(), Name = "Test", IsExpense = true };
            _mockTransactionTypeRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(transType);

            var result = await _transactionTypeService.GetByIdAsync(transType.Id);

            Assert.AreEqual(transType.Id, result.Id);
        }

        [TestMethod]
        public async Task GetByIdAsyncTest2()
        {
            _mockTransactionTypeRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))!.ReturnsAsync((TransactionType)null);

            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => _transactionTypeService.GetByIdAsync(Guid.NewGuid()));
        }

        [TestMethod]
        public async Task UpdateAsyncTest1()
        {
            var transType = new TransactionType { Id = Guid.NewGuid(), Name = "Test", IsExpense = true };
            _mockTransactionTypeRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(transType);

            var updateDto = new TransactionTypeForUpdateDTO { Id = transType.Id, Name = "Updated", IsExpense = false };

            await _transactionTypeService.UpdateAsync(updateDto);

            _mockTransactionTypeRepository.Verify(r => r.Update(It.IsAny<TransactionType>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task UpdateAsyncTest2()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _transactionTypeService.UpdateAsync(null!));
        }

        [TestMethod]
        public async Task UpdateAsyncTest3()
        {
            _mockTransactionTypeRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))!.ReturnsAsync((TransactionType)null);

            var updateDto = new TransactionTypeForUpdateDTO { Id = Guid.NewGuid(), Name = "Updated", IsExpense = false };

            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => _transactionTypeService.UpdateAsync(updateDto));
        }
    }
}
