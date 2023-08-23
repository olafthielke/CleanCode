using CleanCode.ExtractUntilYouDrop.UseCase.Entities;

namespace CleanCode.ExtractUntilYouDrop.UseCase.Interfaces
{
    public interface IRegisterCustomerUseCase
    {
        Task<Customer> RegisterCustomer(CustomerRegistration registration);
    }
}
