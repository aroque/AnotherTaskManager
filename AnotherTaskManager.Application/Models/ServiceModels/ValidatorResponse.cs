using System;
namespace AnotherTaskManager.Api.ServiceModels
{
    public class ValidatorResponse
    {
        public bool Valid { get; set; }

        public string ParameterName { get; set; }

        public string ErrorMessage { get; set; }
    }
}
