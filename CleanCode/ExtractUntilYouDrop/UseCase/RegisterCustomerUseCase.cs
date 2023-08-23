using CleanCode.ExtractUntilYouDrop.UseCase.Entities;
using CleanCode.ExtractUntilYouDrop.UseCase.Exceptions;
using CleanCode.ExtractUntilYouDrop.UseCase.Interfaces;

namespace CleanCode.ExtractUntilYouDrop.UseCase
{
    public class RegisterCustomerUseCase : IRegisterCustomerUseCase
    {
        public ICustomerRepository Repository { get; }


        public RegisterCustomerUseCase(ICustomerRepository repository)
        {
            Repository = repository;
        }


        public async Task<Customer> RegisterCustomer(CustomerRegistration reg)
        {
            if (reg == null)
                throw new MissingCustomerRegistration();

            var errors = new ValidationException();
            if (string.IsNullOrWhiteSpace(reg.FirstName))
                errors.Add("Missing first name.");
            if (string.IsNullOrWhiteSpace(reg.LastName))
                errors.Add("Missing last name.");
            if (string.IsNullOrWhiteSpace(reg.EmailAddress))
                errors.Add("Missing email address.");
            if (errors.HasErrors)
                throw errors;

            var existCust = await Repository.GetCustomer(reg.EmailAddress);
            if (existCust != null)
                throw new DuplicateCustomerEmailAddress(reg.EmailAddress);

            var customer = new Customer(Guid.NewGuid(), reg.FirstName, reg.LastName, reg.EmailAddress);

            await Repository.SaveCustomer(customer);
            return customer;
        }
    }
}