using System;
using System.Collections.Generic;
using System.Linq;
using CustomerApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using CustomerApi.Repositories;

namespace CustomerApi.Controllers
{
    /// Web API controllers should derive from ControllerBase, not Controller.
    /// Controller adds support for views, which Web API controllers do not
    /// need.
    ///
    /// ApiController makes attribute routing a requirement. Each class and
    /// action requires an attribute.
    ///
    /// ApiController makes model validation errors automatically trigger a
    /// 400. You do *not* need to do the following in each action:
    ///
    /// <example>
    /// if (!ModelState.IsValid) {
    ///     return BadRequest(ModelState);
    /// }
    /// </example>
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly CustomerRepository _customerRepository;

        public CustomerController(ILogger<CustomerController> logger,
            CustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        // TODO: What should this return? ActionResult?
        //
        // TODO: What is the difference between IAsyncResult / IActionResult /
        // ActionResult / ActionResult<T>?
        [HttpGet]
        // [Authorize]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        public ActionResult<IList<Customer>> Get()
        {
            return Ok(_customerRepository.GetCustomers());
        }

        [HttpGet]
        [Route("{id:length(24)}")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        public ActionResult<Customer> GetById(string id) {
            return Ok(_customerRepository.Get(id));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Customer> Create(Customer customer) {
            _customerRepository.Create(customer);
            return CreatedAtAction(nameof(GetById), new { id = customer.Id}, customer);
        }



    }
}
