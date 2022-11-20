using PawMate.Services.Interfaces;

namespace Services.Interfaces
{
    public interface IServiceManager
    {
        IInvoiceService Invoice { get; }
        IUserService User { get; }
    }
}
