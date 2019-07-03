using MeBank.Models.Concrete;

namespace MeBank.Services.Abstract
{
    public interface IPaymentRepositoryService : 
        IEntityRepositoryService<Payment>
    {
    }
}