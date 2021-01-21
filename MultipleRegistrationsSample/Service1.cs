using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace MultipleRegistrationsSample
{
    internal class Service1 : IService //Or ISubService1 which will still be considered as an IService
    {
        private readonly ILogger<Service1> logger;

        public Service1(ILogger<Service1> logger)
        {
            this.logger = logger;
        }

        public Task RunServiceAsync()
        {
            logger.LogInformation("In RunServiceAsync");
            return Task.CompletedTask;
        }
    }
}