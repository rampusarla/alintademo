using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alinta.Core.Messages;
using Alinta.Core.Infrastructure;
using Alinta.Core.Interfaces;
using Alinta.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Alinta.Core.Filters;
using Alinta.Core.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Alinta.Controllers
{    
    [ApiValidationFilter]    
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _objCustomer;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerRepository objCustomer, ILogger<CustomerController> logger)
        {
            _objCustomer = objCustomer;
            _logger = logger;
        }
        

        [HttpGet]
        [Route("api/Customer/Index")]
        public async Task<IActionResult> Index([FromQuery]string searchString = null)
        {
            try
            {
                var customers = await _objCustomer.GetAllCustomers(searchString);                
                return Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorMessages.UnhandledExpcetion);
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorMessages.UnhandledExpcetion);                
            }
        }

        [HttpPost]
        [Route("api/Customer/Create")]
        public async Task<IActionResult> Create(CustomerViewModel customer)
        {                                        
            try
            {
                var response = await _objCustomer.AddCustomer(customer);
                return Ok(response);                   
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorMessages.UnhandledExpcetion);
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorMessages.UnhandledExpcetion);                
            }
        }

        [HttpGet]
        [Route("api/Customer/Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {            
            try
            {
                var customer = await _objCustomer.GetCustomerData(id);

                if (customer == null)
                {
                    return NotFound(ErrorMessages.CustomerNotFound);
                }

                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorMessages.UnhandledExpcetion);
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorMessages.UnhandledExpcetion);
            }            
        }
        [HttpPut]
        [Route("api/Customer/Edit/{id}")]
        public async Task<IActionResult> Edit(int id, CustomerViewModel customerVM)
        {            
            try
            {
                var customer = await _objCustomer.GetCustomerData(id);
                if(customer == null)
                {
                    return NotFound(ErrorMessages.CustomerNotFound);
                }
                var response = await _objCustomer.UpdateCustomer(id, customerVM);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorMessages.UnhandledExpcetion);
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorMessages.UnhandledExpcetion);
            }            
        }
        [HttpDelete]
        [Route("api/Customer/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {                        
            try
            {
                var customer = await _objCustomer.GetCustomerData(id);
                if(customer == null)
                {
                    return NotFound(ErrorMessages.CustomerNotFound);
                }
                var response = await _objCustomer.DeleteCustomer(customer);                
                return Ok(response);                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ErrorMessages.UnhandledExpcetion);
                return StatusCode(StatusCodes.Status500InternalServerError, ErrorMessages.UnhandledExpcetion);
            }            
        }
    }
}
