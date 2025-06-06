﻿using AutoMapper;
using energo.customer.api.Dto;
using energo.domain.Contracts.Constants;
using energo.domain.Events.CustomerEvents;
using energo.domain.Models;
using energo.infrastructure.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace energo.customer.api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController(
    ILogger<CustomerController> logger,
    IValidator<CustomerDto> validator,
    IMapper mapper,
    IProducerService<Customer> producerService
    ) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddCustomerAsync([FromBody] CustomerDto customerDto)
    {
        var validationResult = validator.Validate(customerDto);
        if (!validationResult.IsValid)
        {
            var msg = string.Join("; ", validationResult.Errors.Select(p => p.ErrorMessage).ToList());
            logger.LogError(msg);
            return NotFound(msg);
        }

        var customer = mapper.Map<Customer>(customerDto);

        await producerService.ProduceAsync(customer, Topics.Customer, typeof(AddCustomerEvent).Name);

        return Ok(customer);
    }
}
