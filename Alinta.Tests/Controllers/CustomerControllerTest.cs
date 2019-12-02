using Alinta.Core.Infrastructure;
using Alinta.Core.Models;
using Alinta.Core.Repositories;
using Alinta.Tests.Fakes;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Alinta.Tests
{
    [TestClass]
    public class CustomerControllerTest
    {
        public Mock<DbSet<Customer>> mockSet { get; set; }
        public Mock<CustomerDBContext> mockContext { get; set; }
        public CustomerRepository repository { get; set; }
        public List<Customer> fakeCustomers  { get; set; }

        public CustomerControllerTest()
        {            
            fakeCustomers = new List<Customer>()
            {
                new Customer() { Id = 1, FirstName = "FirstNam1", LastName = "LastName2", DateOfBirth = new DateTime(1984, 01, 25) },
                new Customer() { Id = 2, FirstName = "FirstNam2", LastName = "LastName2", DateOfBirth = new DateTime(1980, 12, 12) },
                new Customer() { Id = 3, FirstName = "FirstNam3", LastName = "LastName3", DateOfBirth = new DateTime(1995, 07, 19) },
                new Customer() { Id = 4, FirstName = "FirstNam4", LastName = "LastName4", DateOfBirth = new DateTime(1991, 10, 3) },
                new Customer() { Id = 5, FirstName = "FirstNam5", LastName = "LastName5", DateOfBirth = new DateTime(2004, 04, 10) }
            };
           
            mockContext = new Mock<CustomerDBContext>();
            mockSet = new Mock<DbSet<Customer>>();
            mockContext.SetupGet(x => x.Customers).Returns(DbSetMocking.GetDbSet<Customer>(fakeCustomers.AsQueryable()).Object);

            repository = new CustomerRepository(mockContext.Object, null);

        }        

        [TestMethod]
        public void AddCustomer_saves_a_customer_via_context()
        {
            // Act
            var entityToBeAdded = new Customer() { Id = 6, FirstName = "FirstName1", LastName = "LastName2", DateOfBirth = new System.DateTime(2019, 01, 01) };
            mockContext.Setup(m => m.Customers.Add(It.IsAny<Customer>())).Callback<Customer>((entity) => {
                fakeCustomers.Add(entity);
            });

            //Arrage
            var customers = repository.AddCustomer(entityToBeAdded).Result;

            //Assert
            Assert.IsTrue(fakeCustomers.Count() == 6);
        }

        [TestMethod]
        public void GetCustomer_gets_a_specific_customer_via_context()
        {
            // Arrange
            var customerId = 2;            

            // Act
            var customer = repository.GetCustomerData(customerId).Result;

            // Assert
            Assert.IsNotNull(customer);
            Assert.AreEqual(customer.Id, customerId);
        }

        [TestMethod]
        public void UpdateCustomer_first_name_saves_a_customer_with_updated_data_via_context()
        {
            // Arrange
            var customerIdToGet = 1;
            var nameToChangeFooTo = "TestData";

            // Act
            var foo = repository.GetCustomerData(customerIdToGet).Result;
            foo.FirstName = nameToChangeFooTo;
            mockContext.Object.SaveChanges();
            var retrieveAgainCustomer = repository.GetCustomerData(customerIdToGet).Result;

            // Assert
            Assert.IsNotNull(foo);
            Assert.AreEqual(retrieveAgainCustomer.Id, customerIdToGet);
            Assert.AreEqual(retrieveAgainCustomer.FirstName, nameToChangeFooTo);
        }

        [TestMethod]
        public void DeleteCustomer_deletes_specific_customer_via_context()
        {
            // Arrange.
            var entityToBeRemoved = new Customer() { Id = 1, FirstName = "FirstNam1", LastName = "LastName2", DateOfBirth = new DateTime(1984, 01, 25) };
            mockContext.Setup(m => m.Customers.Remove(It.IsAny<Customer>())).Callback<Customer>((entity) => {
                fakeCustomers.RemoveAll(e => e.Id == entityToBeRemoved.Id);
            });

            // Act            
            var deletedRecord = repository.DeleteCustomer(entityToBeRemoved).Result;            
            var fooRetrievedAgain = fakeCustomers.FirstOrDefault(c => c.Id == entityToBeRemoved.Id);

            // Assert
            Assert.IsNull(fooRetrievedAgain);
        }
    }
}
