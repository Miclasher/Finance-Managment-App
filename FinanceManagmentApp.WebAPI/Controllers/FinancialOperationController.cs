using FinanceManagmentApp.Services.Abstractions;
using FinanceManagmentApp.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManagmentApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FinancialOperationController : ControllerBase
    {
        private readonly IFinancialOperationService _financialOperationService;

        public FinancialOperationController(IFinancialOperationService financialOperationService)
        {
            _financialOperationService = financialOperationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinancialOperationDTO>>> GetAll()
        {
            var finOps = await _financialOperationService.GetAllAsync(User);

            return Ok(finOps);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FinancialOperationDTO>> GetById(Guid id)
        {
            var finOp = await _financialOperationService.GetByIdAsync(User, id);

            return Ok(finOp);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FinancialOperationForCreateDTO financialOperation)
        {
            await _financialOperationService.CreateAsync(User, financialOperation);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] FinancialOperationForUpdateDTO financialOperation)
        {
            await _financialOperationService.UpdateAsync(User, financialOperation);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _financialOperationService.DeleteAsync(User, id);

            return Ok();
        }
    }
}
