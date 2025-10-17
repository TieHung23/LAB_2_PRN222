using System;
using System.Collections.Generic;
using System.Threading.Tasks; 
using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using EVDMS.DAL.Repositories.Abstractions;

namespace EVDMS.BLL.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }

        public async Task<Customer> CreateCustomerAsync(Customer newCustomer)
        {
            newCustomer.Id = Guid.NewGuid();
            return await _customerRepository.AddAsync(newCustomer);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            await _customerRepository.UpdateAsync(customer);
        }

        public async Task DeleteCustomerAsync(Guid id)
        {

            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer != null)
            {
                await _customerRepository.DeleteAsync(customer);
            }
        }

        public async Task<bool> EmailExistsAsync(string email, Guid? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(email)) return false; 
            return await _customerRepository.AnyAsync(c => c.Email.ToLower() == email.ToLower() && c.Id != excludeId);
        }

        public async Task<bool> PhoneNumberExistsAsync(string phoneNumber, Guid? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber)) return false;
            return await _customerRepository.AnyAsync(c => c.PhoneNumber == phoneNumber && c.Id != excludeId);
        }
        public async Task<bool> IdCardNumberExistsAsync(string idCardNumber, Guid? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(idCardNumber)) return false;
            return await _customerRepository.AnyAsync(c => c.IdCardNumber == idCardNumber && c.Id != excludeId);
        }
    }
}