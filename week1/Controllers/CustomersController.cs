using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using week1.DTOs;
using week1.Models;
namespace week1.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private List<Customer> customerList = new List<Customer>();

        public CustomersController(IMapper mapper)
        {
            customerList.Add(new Customer() {Id=1, FirstName="A",BankAccount="1234",ATMCode="6342",Balance=100});
            customerList.Add(new Customer() {Id=2, FirstName="B",BankAccount="2345",ATMCode="2745",Balance=200});
            customerList.Add(new Customer() {Id=3, FirstName="C",BankAccount="3456",ATMCode="3746",Balance=300});
            customerList.Add(new Customer() {Id=4, FirstName="D",BankAccount="4567",ATMCode="0985",Balance=400});
            customerList.Add(new Customer() {Id=5, FirstName="E",BankAccount="6789",ATMCode="7458",Balance=500});
            this._mapper = mapper;
        }

        // [HttpGet]
        // public IActionResult GetAllCustomers() {
        //     return Ok(customerList);
        // }

        [HttpGet]
        public IActionResult GetAllCustomers() {
            var sum = customerList.Sum(x=> x.Balance);
            var count = customerList.Count;
            var detail = _mapper.Map<List<CustomerDTO_ToReturn>>(customerList);
            
            var result = new CustomerDTO_ToReturn_Summary();

            result.SumBalance = sum;
            result.CustomerCount = count;
            result.Detail = detail;
            
            return Ok(result);
        }

        // [HttpGet("{id}")]  // url/api/Customer/1
        // public IActionResult GetCustomer(int id) {
        //     var result = customerList.Where(x => x.Id == id).SingleOrDefault();

        //     var resultToReturn = new CustomerDTO_ToReturn();
        //     //No AutoMapper
        //     resultToReturn.Id = result.Id;
        //     resultToReturn.FirstName = result.FirstName;
        //     resultToReturn.BankAccount = result.BankAccount;
        //     resultToReturn.Balance = result.Balance;

        //     return Ok(resultToReturn);
        // }

        [HttpGet("{id}")]  // url/api/Customer/1
        public IActionResult GetCustomer(int id) {
            var customerFromGet = customerList.Where(x => x.Id == id).SingleOrDefault();
            var result = _mapper.Map<CustomerDTO_ToReturn>(customerFromGet);

            return Ok(result);
        }
    }
}