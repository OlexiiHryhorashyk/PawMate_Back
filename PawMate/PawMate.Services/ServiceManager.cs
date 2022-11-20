using AutoMapper;
using PawMate.Domain.RepositoryInterfaces;
using PawMate.Services.Interfaces;
using Services.Interfaces;

namespace PawMate.Services
{
    public class ServiceManager: IServiceManager
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public IInvoiceService Invoice => new InvoiceService(_repositoryManager, _mapper);
        public IUserService User => new UserService(_repositoryManager, _mapper);
    }
}
