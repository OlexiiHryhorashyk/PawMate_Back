using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PawMate.Models.ImageDTOs;
using PawMate.Models.InvoiceDTOs;
using PawMate.Services.Interfaces;
using Services.Interfaces;

namespace PawMate.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _service;
        private readonly HttpClient _httpClient;

        public InvoiceController(IServiceManager service, IHttpClientFactory httpClientFactory)
        {
            _service = service.Invoice;
            _httpClient = httpClientFactory.CreateClient(nameof(InvoiceController));
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
        public async Task<IActionResult> Update([FromForm] UpdateInvoiceDTO invoice)
        {

            using var content = new MultipartFormDataContent
            {
                {new StreamContent(invoice.PhotoData.OpenReadStream()), "NewImage", invoice.PhotoData.FileName},
                {new StringContent(invoice.Photo), "OldImageUrl"}
            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, "https://localhost:7070/api/Image/Update");
            request.Content = content;

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return Problem(statusCode: 500, detail: "Failed to update");
            }

            invoice.Photo = await response.Content.ReadAsStringAsync();
            return Ok(await _service.UpdateInvoice(invoice));
        }

        [HttpPost("AddInvoice")]
        public async Task<IActionResult> Add([FromForm] CreateInvoiceDTO invoice)
        {
            using var content = new MultipartFormDataContent
            {
                {new StreamContent(invoice.PhotoData.OpenReadStream()), "Image", invoice.PhotoData.FileName},
                {new StringContent(invoice.Name), "UserName"},
                {new StringContent(invoice.UserId.ToString()),"Id"}
            };

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7070/api/Image/Upload");
            request.Content = content;

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return Problem(statusCode: 500, detail: "Failed to add");
            }

            invoice.Photo = await response.Content.ReadAsStringAsync();

            return Ok(await _service.AddInvoice(invoice));
        }

        [HttpDelete("{invoiceId}/delete")]
        public async Task<IActionResult> Delete([FromRoute] int invoiceId)
        {
            InvoiceDTO invoice = await _service.GetInvoiceById(invoiceId);

            if (invoice == null)
            {
                return BadRequest("No such invoice with id");
            }

            HttpResponseMessage response = await _httpClient.DeleteAsync($"https://localhost:7070/api/Image/Delete?url={invoice.Photo}");

            if (!response.IsSuccessStatusCode)
            {
                return Problem(statusCode: 500, detail: "Failed to delete");
            }

            return Ok(await _service.DeleteInvoice(invoiceId));
        }

        [HttpGet("AddLike/{invoiceId}")]
        public async Task<IActionResult> AddLike([FromRoute] int invoiceId, int userId)
        {
            return Ok(await _service.AddLike(invoiceId, userId));
        }
    }
}
