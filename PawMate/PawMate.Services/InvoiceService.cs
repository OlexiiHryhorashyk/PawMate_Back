using AutoMapper;
using PawMate.Domain.Entities;
using PawMate.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using PawMate.Models.InvoiceDTOs;
using PawMate.Services.Interfaces;

namespace PawMate.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public InvoiceService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<InvoiceDTO>> GetAllInvoices()
        {
            var result = await _repository.InvoiceRepository.GetAllAsync();
            return _mapper.Map<List<InvoiceDTO>>(result);
        }

        public async Task<InvoiceDTO> GetInvoiceById(int Id)
        {
            var result = await _repository.InvoiceRepository.GetByIdAsync(Id);
            return _mapper.Map<InvoiceDTO>(result);
        }

        public async Task<List<InvoiceDTO>> GetInvoicesByUserId(int userId)
        {
            var result = await _repository.InvoiceRepository.FindByCondition(invoice => invoice.UserId.Equals(userId)).ToListAsync();
            return _mapper.Map<List<InvoiceDTO>>(result);
        }

        public async Task<InvoiceDTO> AddInvoice(CreateInvoiceDTO invoice)
        {
            var result = await _repository.InvoiceRepository.CreateInvoiceAsync(_mapper.Map<Invoice>(invoice));
            return _mapper.Map<InvoiceDTO>(result);
        }

        public async Task<InvoiceDTO> UpdateInvoice(UpdateInvoiceDTO invoice)
        {
            var result = await _repository.InvoiceRepository.UpdateInvoiceAsync(_mapper.Map<Invoice>(invoice));
            return _mapper.Map<InvoiceDTO>(result);
        }

        public async Task<int> DeleteInvoice(int id)
        {
            await _repository.InvoiceRepository.DeleteInvoiceAsync(id);
            return id;
        }

        public async Task<InvoiceDTO> AddLike(int invoiceId, int userId)
        {
            await _repository.LikedRepository.CreateAsync(new Liked
            {
                InvoiceId = invoiceId,
                UserId = userId
            });
            await _repository.SaveAsync();

            var result = await _repository.InvoiceRepository.GetByIdAsync(invoiceId);
            return _mapper.Map<InvoiceDTO>(result);
        }
    }
}
