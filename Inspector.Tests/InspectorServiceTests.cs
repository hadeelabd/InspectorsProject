using ClassLibrary.persistance;
using InspectorServices;
using INSPECTORV2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace InspectorV2.Tests.IntegrationTests
{
    public class InspectorServiceTests
    {
        private DbContextOptions<LibraryContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        private IDbContextFactory<LibraryContext> GetDbContextFactoryAsync(DbContextOptions<LibraryContext> options)
        {
            var mockDbFactory = new Mock<IDbContextFactory<LibraryContext>>();
            mockDbFactory.Setup(f => f.CreateDbContext()).Returns(() => new LibraryContext(options));
            return mockDbFactory.Object;

        }

        [Fact]
        public async Task Save_ShouldAddInspector()
        {
            // Arrange????? ???????? 
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new InspectorService(factory);
            var inspector = new Inspector { Id = 3, Name = "Inspector1", Phone = "123456789", Email = "Inspector@company.com", Age = 40, Address = "20street", Speialiation = "math" };

            // Act
            await service.Save(inspector);

            // Assert
            using var context = new LibraryContext(options);
            var savedInspector = await context.Inspectors.FirstOrDefaultAsync(a => a.Name == "Inspector1");
            Assert.NotNull(savedInspector);
        }

        [Fact]
        public async Task Get_ShouldReturnInspectorById()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new InspectorService(factory);
            var inspector = new Inspector { Id=33, Name = "Inspector1", Phone = "123456789", Email = "Inspector@company.com", Age = 40, Address = "20street", Speialiation = "math" };
            await service.Save(inspector);

            // Act
            var fetchedInspector = await service.Get(inspector.Id);

            // Assert
            Assert.NotNull(fetchedInspector);
            Assert.Equal(inspector.Name, fetchedInspector.Name);
        }

            [Fact]
            public async Task GetList_ShouldReturnInspectorsByName()
            {
                // Arrange
                var options = CreateNewContextOptions();
                var factory = GetDbContextFactoryAsync(options);
                var service = new InspectorService(factory);
                await service.Save(new Inspector { Id = 33, Name = "Inspector1", Phone = "123456789", Email = "inspector@company.com", Age = 40, Address = "20street", Speialiation = "math" });
                await service.Save(new Inspector { Id = 33, Name = "Inspector2", Phone = "123456789", Email = "inspector@company.com", Age = 40, Address = "20street", Speialiation = "math" });

                // Act
                var inspectors = await service.GetList("Inspector");

                // Assert
                Assert.Equal(2, inspectors.Count);
            }

            [Fact]
            public async Task GetAll_ShouldReturnAllInspectors()
            {
                // Arrange
                var options = CreateNewContextOptions();
                var factory = GetDbContextFactoryAsync(options);
                var service = new InspectorService(factory);
                await service.Save(new Inspector {Id=33,Name= "Inspector1", Phone = "123456789", Email = "inspector@company.com", Age = 40, Address = "20street", Speialiation = "math" });
                await service.Save(new Inspector {Id=33 ,Name = "Inspector2", Phone = "123456789", Email = "inspector@company.com", Age = 40, Address = "20street", Speialiation = "math" });

                // Act
                var inspectors = await service.GetAll();

                // Assert
                Assert.Equal(2, inspectors.Count);
            }

            [Fact]
            public async Task Delete_ShouldRemoveInspector()
            {
                // Arrange
                var options = CreateNewContextOptions();
                var factory = GetDbContextFactoryAsync(options);
                var service = new InspectorService(factory);
                var inspector = new Inspector { Id=33,Name = "Inspector1", Phone = "123456789", Email = "Inspector@company.com", Age = 40, Address = "20street", Speialiation = "math" };
                await service.Save(inspector);

                // Act
                await service.Delete(inspector);

                // Assert
                using var context = new LibraryContext(options);
                var deletedInspector = await context.Inspectors.FindAsync(inspector.Id);
                Assert.Null(deletedInspector);
            }

            [Fact]
            public async Task Update_ShouldModifyInspector()
            {
                // Arrange
                var options = CreateNewContextOptions();
                var factory = GetDbContextFactoryAsync(options);
                var service = new InspectorService(factory);
                var inspector = new Inspector { Id=33,Name = "Inspector1", Email = "inspector1@example.com", Phone = "1234567890", Age = 40, Address = "20street", Speialiation = "math" };
                await service.Save(inspector);

            // Act
            inspector.Id = 33;
            inspector.Name = "Updated Inspector";
                inspector.Email = "updatedinspector@example.com";
                inspector.Phone = "0987654321";
                inspector.Age = 40;
                inspector.Address = "Updated Inspector";
                inspector.Speialiation = "Updated Inspector";
                await service.Update(inspector);

                // Assert
                using var context = new LibraryContext(options);
                var updatedinspector = await context.Inspectors.FindAsync(inspector.Id);
            
            Assert.Equal("UpdatedInspector", updatedinspector.Name);
                Assert.Equal("updatedInspector@example.com", updatedinspector.Email);
                Assert.Equal("0987654321", updatedinspector.Phone);

                Assert.Equal(40, updatedinspector.Age);
                Assert.Equal("updatedInspecor", updatedinspector.Address);
                Assert.Equal("UpdatedInspectorSpeialiation", updatedinspector.Speialiation);
            }


        }
    }