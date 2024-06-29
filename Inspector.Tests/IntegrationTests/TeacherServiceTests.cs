using ClassLibrary.persistance;
using InspectorServices;
using INSPECTORV2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Tests.IntegrationTests
{
    public class TeacherServiceTests
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
        public async Task Save_ShouldAddTeacher()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new TeacherService(factory);
            var teacher = new Teacher { Id = 53, Name = "Teacher1", Phone = "123456789", Email = "Teacher@school.com", Age = 43, Address = "20street", Speialiation = "math" };

            // Act
            await service.Save(teacher);

            // Assert
            using var context = new LibraryContext(options);
            var savedTeacher = await context.Teachers.FirstOrDefaultAsync(a => a.Name == "Teacher1");
            Assert.NotNull(savedTeacher);
        }

        [Fact]
        public async Task Get_ShouldReturnTeacherById()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new TeacherService(factory);
            var teacher = new Teacher { Id = 53, Name = "Teacher1", Phone = "123456789", Email = "Teacher@school.com", Age = 43, Address = "20street", Speialiation = "math" };
            await service.Save(teacher);

            // Act
            var fetchedTeacher = await service.Get(teacher.Id);

            // Assert
            Assert.NotNull(fetchedTeacher);
            Assert.Equal(teacher.Name, fetchedTeacher.Name);
        }

        [Fact]
        public async Task GetList_ShouldReturnTeachersByName()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new TeacherService(factory);
            await service.Save(new Teacher { Id = 53, Name = "Teacher1", Phone = "123456789", Email = "Teacher@school.com", Age = 53, Address = "20street", Speialiation = "math" });
            await service.Save(new Teacher { Id = 24, Name = "Teacher2", Phone = "123456789", Email = "Teacher@school.com", Age = 24, Address = "20street", Speialiation = "math" });

            // Act
            var teachers = await service.GetList("Teacher");

            // Assert
            Assert.Equal(2, teachers.Count);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllTeachers()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new TeacherService(factory);
            await service.Save(new Teacher { Id = 53, Name = "Teacher1", Phone = "123456789", Email = "Teacher@school.com", Age = 43, Address = "20street", Speialiation = "math" });
            await service.Save(new Teacher { Id = 24, Name = "Teacher2", Phone = "123456789", Email = "Teacher@school.com", Age = 64, Address = "20street", Speialiation = "math" });

            // Act
            var teachers = await service.GetAll();

            // Assert
            Assert.Equal(2, teachers.Count);
        }

        [Fact]
        public async Task Delete_ShouldRemoveTeacher()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new TeacherService(factory);
            var teacher = new Teacher { Id = 53, Name = "Teacher1", Phone = "123456789", Email = "Teacher@school.com", Age = 43, Address = "20street", Speialiation = "math" };
            await service.Save(teacher);

            // Act
            await service.Delete(teacher);

            // Assert
            using var context = new LibraryContext(options);
            var deletedTeacher = await context.Teachers.FindAsync(teacher.Id);
            Assert.Null(deletedTeacher);
        }

        [Fact]
        public async Task Update_ShouldModifyTeacher()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var factory = GetDbContextFactoryAsync(options);
            var service = new TeacherService(factory);
            var teacher = new Teacher { Id = 53, Name = "Teacher1", Email = "teacher1@example.com", Phone = "1234567890", Age = 43, Address = "20street", Speialiation = "math" };
            await service.Save(teacher);

            // Act
            teacher.Name = "UpdatedTeacher";
            teacher.Email = "UpdatedTeacher@example.com";
            teacher.Phone = "0987654321";
            teacher.Address = "UpdatedAddress";
            teacher.Speialiation = "UpdatedSpeialiation";
            teacher.Age = 53;
            await service.Update(teacher);

            // Assert
            using var context = new LibraryContext(options);
            var updatedTeacher = await context.Teachers.FindAsync(teacher.Id);

            Assert.Equal("UpdatedTeacher", updatedTeacher.Name);
            Assert.Equal("UpdatedTeacher@example.com", updatedTeacher.Email);
            Assert.Equal("0987654321", updatedTeacher.Phone);
            Assert.Equal(53, updatedTeacher.Age);
            Assert.Equal("UpdatedAddress", updatedTeacher.Address);
            Assert.Equal("UpdatedSpeialiation", updatedTeacher.Speialiation);
        }
    }
}



