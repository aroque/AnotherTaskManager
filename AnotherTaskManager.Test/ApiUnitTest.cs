using System;
using AnotherTaskManager.Api.Controllers;
using AnotherTaskManager.Api.ServiceModels;
using AnotherTaskManager.App.Models.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AnotherTaskManager.Test
{
    public class ApiUnitTest
    {

        public ApiUnitTest()
        {
            InitContext();
        }

        ATMContext _atmContext;

        void InitContext()
        {
             var builder = new DbContextOptionsBuilder<ATMContext>()
                .UseInMemoryDatabase();

            var context = new ATMContext(builder.Options);
            context.Database.EnsureDeleted();
            context.Add(new Task
            {
                Id = 1,
                Name = "Teste",
                Status = 1,
                StatusChanges = 0,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            });
            int changed = context.SaveChanges();
            _atmContext = context;
        }

        [Fact]
        public void NameDoesNotExist()
        {
            ValidatorController controller = new ValidatorController(_atmContext);

            Console.WriteLine("Teste");

            ActionResult<ValidatorApiResponse> result = controller.Get("AnotherTeste");

            OkObjectResult resp = result.Result as OkObjectResult;

            ValidatorApiResponse apiResp = resp.Value as ValidatorApiResponse;

            Assert.True(apiResp.Valid);
        }

        [Fact]
        public void NameDoExist()
        {

            ValidatorController controller = new ValidatorController(_atmContext);

            ActionResult<ValidatorApiResponse> result = controller.Get("Teste");

            OkObjectResult resp = result.Result as OkObjectResult;

            ValidatorApiResponse apiResp = resp.Value as ValidatorApiResponse;

            Assert.False(apiResp.Valid);
        }

        [Fact]
        public void CallReturnsException()
        {
            ValidatorController controller = new ValidatorController(_atmContext);

            try
            {

                ActionResult<ValidatorApiResponse> result = controller.Get(null);
            }
            catch (Exception ex)
            {
                Assert.Equal("Missing parameter Name", ex.Message);
            }
        }
    }
}
