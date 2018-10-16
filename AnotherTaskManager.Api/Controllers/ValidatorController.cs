using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AnotherTaskManager.App.Models.DataModels;
using AnotherTaskManager.App.Models.Repositories;
using AnotherTaskManager.Api.ServiceModels;

namespace AnotherTaskManager.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ValidatorController : ControllerBase
    {
        TaskRepository taskRepository;

        public ValidatorController(ATMContext context){
            taskRepository = new TaskRepository(context);
        }

        // GET api/validate
        [HttpGet]
        public ActionResult<ValidatorApiResponse> Get(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new Exception("Missing parameter Name");

            bool isValid = !taskRepository.CheckIfNameExists(name);

            System.Threading.Thread.Sleep(new Random().Next(5000, 7000));

            return Ok(new ValidatorApiResponse
            {
                Valid = isValid,
                ParameterName = nameof(name),
                ErrorMessage = isValid ? string.Empty : "The name already exists"
            });
        }


    }
}
