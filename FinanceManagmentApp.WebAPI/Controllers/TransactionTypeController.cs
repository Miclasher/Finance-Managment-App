using FinanceManagmentApp.Services.Abstractions;
using FinanceManagmentApp.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManagmentApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionTypeController : ControllerBase
    {
        private readonly ITransactionTypeService _transactionTypeService;

        public TransactionTypeController(ITransactionTypeService transactionTypeeService)
        {
            _transactionTypeService = transactionTypeeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionTypeDTO>>> GetAll()
        {
            var trTypes = await _transactionTypeService.GetAllAsync(User);

            return Ok(trTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionTypeDTO>> GetById(Guid id)
        {
            var trType = await _transactionTypeService.GetByIdAsync(User, id);

            return Ok(trType);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _transactionTypeService.DeleteAsync(User, id);

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(TransactionTypeForUpdateDTO transactionType)
        {
            await _transactionTypeService.UpdateAsync(User, transactionType);

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Create(TransactionTypeForCreateDTO transactionType)
        {
            await _transactionTypeService.CreateAsync(User, transactionType);

            return Ok();
        }
    }
}
