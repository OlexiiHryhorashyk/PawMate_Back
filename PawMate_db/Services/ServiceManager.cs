using AutoMapper;
using Domain.RepositoryInterfaces;
using Services.Interfaces;

namespace Services
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
