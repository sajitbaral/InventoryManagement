using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Operational.Application.DTOs.Customer;
using Operational.Application.Interfaces;

namespace Operational.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CustomerResponseDto>>> GetCustomers(CancellationToken cancellationToken)
        {
            var customers = await _customerService.GetCustomersAsync(cancellationToken);
            return Ok(customers);
        }

        [HttpGet("{customerId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerResponseDto?>> GetCustomerById(int customerId, CancellationToken cancellationToken)
        {
            var customer = await _customerService.GetCustomerByIdAsync(customerId, cancellationToken);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomerResponseDto>> CreateCustomer(CreateCustomerDto dto, CancellationToken cancellationToken)
        {
            var customer = await _customerService.CreateCustomerAsync(dto, cancellationToken);
            return CreatedAtAction(
                nameof(GetCustomerById),
                new { customerId = customer.CustomerId },
                customer
                );
        }

        [HttpPut("{customerId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> UpdateCustomer(int customerId, UpdateCustomerDto dto, CancellationToken cancellationToken)
        {
            var updated = await _customerService.UpdateCustomerAsync(customerId, dto, cancellationToken);

            if (!updated)
            {
                return NotFound(new
                {
                    Message = $"Customer with CustomerId {customerId} not found."
                });
            }

            return NoContent();
        }

        [HttpPatch("{customerId:int}/deactivate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeactivateCustomer (int customerId,  CancellationToken cancellationToken)
        {
            var deactivated = await _customerService.DeactivateCustomerAsync(customerId, cancellationToken);
            if (!deactivated)
            {
                return NotFound(new
                {
                    Message = $"Customer with CustomerId {customerId} not found."
                });
            }

            return NoContent();
        }

    }
}
