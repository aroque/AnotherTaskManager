using System;
namespace AnotherTaskManager.Api.ServiceModels
{
    public class ValidatorApiResponse
    {
        public bool Valid { get; set; }

        public string ParameterName { get; set; }

        public string ErrorMessage { get; set; }
    }
}
