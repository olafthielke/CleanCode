using CleanCode.ExtractUntilYouDrop.UseCase.Entities;

namespace CleanCode.ExtractUntilYouDrop.UseCase.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomer(string emailAddress);

        Task SaveCustomer(Customer customer);
    }
}