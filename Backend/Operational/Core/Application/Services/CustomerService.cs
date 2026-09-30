using Operational.Application.DTOs.Customer;
using Operational.Application.Interfaces;
using Operational.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Operational.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            
        }

        public async Task<CustomerResponseDto> CreateCustomerAsync(CreateCustomerDto dto, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            var customer = new Customer
            {
                Name = dto.Name,
                Phone = dto.Phone,
                Email = dto.Email,
                Address = dto.Address,
                CreatedAt= now,
                UpdatedAt= now
            };
            
            await _customerRepository.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CustomerResponseDto
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Phone = customer.Phone,
                Email = customer.Email,
                Address = customer.Address,
                IsActive = customer.IsActive,
                CreatedAt = customer.CreatedAt,
                UpdatedAt = customer.UpdatedAt
            };
        }

        public async Task<List<CustomerResponseDto>> GetCustomersAsync(CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetCustomersAsync(cancellationToken);
            
            return customers.Select(c=> new CustomerResponseDto
            {
                CustomerId = c.CustomerId,
                Name= c.Name,
                Phone= c.Phone,
                Email= c.Email,
                Address= c.Address,
                IsActive= c.IsActive,
                CreatedAt= c.CreatedAt,
                UpdatedAt= c.UpdatedAt
            })
                .ToList();
        }

        public async Task<CustomerResponseDto?> GetCustomerByIdAsync(int customerId, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId, cancellationToken);
            
            if (customer == null)
            {
                return null;
            }

            return new CustomerResponseDto
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Phone = customer.Phone,
                Email = customer.Email,
                Address = customer.Address,
                IsActive = customer.IsActive,
                CreatedAt = customer.CreatedAt,
                UpdatedAt = customer.UpdatedAt
            };

        }

        public async Task<bool> UpdateCustomerAsync(int  customerId, UpdateCustomerDto dto, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId, cancellationToken);

            if(customer == null)
            {
                return false;
            }

            customer.Name= dto.Name;
            customer.Phone= dto.Phone;
            customer.Email= dto.Email;
            customer.Address= dto.Address;
            customer.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;

        }

        public async Task<bool> DeactivateCustomerAsync(int customerId, CancellationToken cancellationToken){
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId, cancellationToken);

            if( customer == null)
            {
                return false;
            }

            customer.IsActive = false;
            customer.UpdatedAt= DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

        

    }
}
