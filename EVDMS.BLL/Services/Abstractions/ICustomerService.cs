using EVDMS.Core.Entities;

namespace EVDMS.BLL.Services.Abstractions
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(Guid id);
        Task<Customer> CreateCustomerAsync(Customer newCustomer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(Guid id);
        Task<bool> EmailExistsAsync(string email, Guid? excludeId = null);
        Task<bool> PhoneNumberExistsAsync(string phoneNumber, Guid? excludeId = null);
        Task<bool> IdCardNumberExistsAsync(string idCardNumber, Guid? excludeId = null);
    }
}