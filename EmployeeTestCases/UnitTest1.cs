using ExampleRESTFULLAPI.ApiControllers;
using ExampleRESTFULLAPI.Controllers;
using ExampleRESTFULLAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WpfApp1;
using Xunit;

namespace EmployeeTestCases
{
    public class UnitTest1
    {

        private readonly IEmployeeApiFake _service;
        public UnitTest1()
        {
            _service = new EmployeeApiFake();
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var items = _service.GetAllEmployeeAsync();

 
            // Assert
            Assert.Equal(3, items.Count);
        }


        [Fact]
        public void Remove_ExistingGuidPassed_RemovesOneItem()
        {
            EmployeeDetails emp = new EmployeeDetails() { id = 1, name = "Aradhana", email = "aradhana@gmail.com", gender = "female" };
            // Act
            var response = _service.DeleteEmployeeDetails(emp);

            // Assert
            var items = Assert.IsType<List<EmployeeDetails>>(_service.GetAllEmployeeAsync());
            Assert.Equal(3, items.Count);
        }

    }
}
