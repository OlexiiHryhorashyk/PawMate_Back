using Microsoft.AspNetCore.Mvc;
using Models.InvoiceDTOs;
using Services.Interfaces;

namespace PawMate.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _service;

        public InvoiceController(IServiceManager service)
        {
            _service = service.Invoice;
        }

        [HttpGet("GetAllInvoices")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllInvoices());
        }

        [HttpGet("{invoiceId}/GetInvoice")]
        public async Task<IActionResult> GetById([FromRoute] int invoiceId)
        {
            return Ok(await _service.GetInvoiceById(invoiceId));
        }

        [HttpGet("{userId}/GetInvoiceByUser")]
        public async Task<IActionResult> GetByUserId([FromRoute] int userId)
        {
            return Ok(await _service.GetInvoicesByUserId(userId));
        }

        [HttpPut("UpdateInvoice")]
        public async Task<IActionResult> Update([FromBody] UpdateInvoiceDTO invoice)
        {
            return Ok(await _service.UpdateInvoice(invoice));
        }

        [HttpPost("AddInvoice")]
        public async Task<IActionResult> Add([FromBody] CreateInvoiceDTO invoice)
        {
            return Ok(await _service.AddInvoice(invoice));
        }

        [HttpDelete("{invoiceId}/delete")]
        public async Task<IActionResult> Delete([FromRoute] int invoiceId)
        {
            return Ok(await _service.DeleteInvoice(invoiceId));
        }

        [HttpGet("AddLike/{invoiceId}")]
        public async Task<IActionResult> AddLike([FromRoute] int invoiceId, int userId)
        {
            return Ok(await _service.AddLike(invoiceId, userId));
        }
    }
}
