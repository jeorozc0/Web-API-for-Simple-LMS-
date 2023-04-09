using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using WebApi2;
using Xunit;

namespace MyWebAPI.Tests
{
    public class MyWebAPITests : IDisposable
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private readonly DbContextOptions<DomainContext> _dbOptions;

        public MyWebAPITests()
        {
            // Set up the in-memory database for testing
            _dbOptions = new DbContextOptionsBuilder<DomainContext>()
                .UseInMemoryDatabase(databaseName: "lms")
                .Options;

            // Create a new test server with the web app's Startup configuration
            _server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Testing") // Use a separate environment for testing
                .UseStartup<Startup>()
                .ConfigureTestServices(services =>
                {
                    // Replace the real DbContext with an in-memory DbContext
                    services.AddScoped<DomainContext>(provider => new DomainContext(_dbOptions));
                }));

            // Create a new HTTP client for interacting with the test server
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task TestGetEndpointReturnsData()
        {
            // Arrange
            var expectedData = new List<Course>()
            {
                new Course { ID = 1, Name = "CS416" },
            };
            using (var context = new DomainContext(_dbOptions))
            {
                context.Courses.AddRange(expectedData);
                context.SaveChanges();
            }

            // Act
            var response = await _client.GetAsync("/api/Course");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var actualData = JsonConvert.DeserializeObject<List<Course>>(responseContent);
            Assert.Equal(expectedData.Count, actualData.Count);
            for (int i = 0; i < expectedData.Count; i++)
            {
                Assert.Equal(expectedData[i].ID, actualData[i].ID);
                Assert.Equal(expectedData[i].Name, actualData[i].Name);
            }
        }

        [Fact]
                    // Arrange
        public async Task TestGetEndpointReturnsDataWithRelatedModules(){
            var Course = new List<Course>()
            {
                new Course { ID = 1, Name = "CS416" },
            };

            var expectedData = new List<Module>()
            {
                new Module { ID = 1, Name = "Module 1", CourseID = 1 },
                new Module { ID = 2, Name = "Module 2", CourseID = 1 }
            };
            using (var context = new DomainContext(_dbOptions))
            {
                context.Modules.AddRange(expectedData);
                context.SaveChanges();
            }

            // Act
            var response = await _client.GetAsync("/api/Course/1");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var actualData = JsonConvert.DeserializeObject<List<Module>>(responseContent);
            Assert.Equal(expectedData.Count, actualData.Count);
            for (int i = 0; i < expectedData.Count; i++)
            {
                Assert.Equal(expectedData[i].ID, actualData[i].ID);
                Assert.Equal(expectedData[i].Name, actualData[i].Name);
                Assert.Equal(expectedData[i].CourseID, actualData[i].CourseID);

            }
        }

        [Fact]
                    // Arrange
        public async Task TestGetEndpointReturnsCourses(){
            var expectedData = new List<Course>()
            {
                new Course { ID = 1, Name = "CS416" },
                new Course { ID = 2, Name = "CS420" },
                new Course { ID = 3, Name = "CS418" },
            };
            using (var context = new DomainContext(_dbOptions))
            {
                context.Courses.AddRange(expectedData);
                context.SaveChanges();
            }

            // Act
            var response = await _client.GetAsync("/api/Course");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var actualData = JsonConvert.DeserializeObject<List<Course>>(responseContent);
            Assert.Equal(expectedData.Count, actualData.Count);
            for (int i = 0; i < expectedData.Count; i++)
            {
                Assert.Equal(expectedData[i].ID, actualData[i].ID);
                Assert.Equal(expectedData[i].Name, actualData[i].Name);
            }
        }

        [Fact] 
        public async Task TestGetEndpointReturnsModules(){
            var expectedData = new List<Module>()
            {
                new Module { ID = 5, Name = "Module 25", CourseID = 1 },

            };
            using (var context = new DomainContext(_dbOptions))
            {
                context.Modules.AddRange(expectedData);
                context.SaveChanges();
            }

            // Act
            var response = await _client.GetAsync("/api/Course/1/Module/5");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var actualData = JsonConvert.DeserializeObject<List<Module>>(responseContent);
            Assert.Equal(expectedData.Count, actualData.Count);
            for (int i = 0; i < expectedData.Count; i++)
            {
                Assert.Equal(expectedData[i].ID, actualData[i].ID);
                Assert.Equal(expectedData[i].Name, actualData[i].Name);
                Assert.Equal(expectedData[i].CourseID, actualData[i].CourseID);
            }
        }

        [Fact]
        public async Task TestGetEndpointReturnsAssignments(){
            var expectedData = new List<Assignment>()
            {
                new Assignment { ID = 25, Name = "Assignment 25", Grade = 100, DueDate = DateTime.Now, ModuleID = 1},

            };
            using (var context = new DomainContext(_dbOptions))
            {
                context.Assignments.AddRange(expectedData);
                context.SaveChanges();
            }

            // Act
            var response = await _client.GetAsync("/api/Course/1/Module/1/Assignment/25");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var actualData = JsonConvert.DeserializeObject<List<Assignment>>(responseContent);
            Assert.Equal(expectedData.Count, actualData.Count);
            for (int i = 0; i < expectedData.Count; i++)
            {
                Assert.Equal(expectedData[i].ID, actualData[i].ID);
                Assert.Equal(expectedData[i].Name, actualData[i].Name);
                Assert.Equal(expectedData[i].Grade, actualData[i].Grade);
                Assert.Equal(expectedData[i].DueDate, actualData[i].DueDate);
                Assert.Equal(expectedData[i].ModuleID, actualData[i].ModuleID);
            }
        }

        [Fact]
                public async Task TestGetEndpointReturnsNoCourse(){
            var expectedData = new List<Course>()
            {
                new Course {},

            };
            using (var context = new DomainContext(_dbOptions))
            {
                context.Courses.AddRange(expectedData);
                context.SaveChanges();
            }

            // Act
            var response = await _client.GetAsync("/api/Course/25");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var actualData = JsonConvert.DeserializeObject<List<Course>>(responseContent);
            Assert.Equal(expectedData.Count, actualData.Count);
            for (int i = 0; i < expectedData.Count; i++)
            {
                Assert.Equal(expectedData[i].ID, actualData[i].ID);
                Assert.Equal(expectedData[i].Name, actualData[i].Name);
            }
        }

        


        public void Dispose()
        {
            _server.Dispose();
        }
    }
}
