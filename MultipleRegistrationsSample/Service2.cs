using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace MultipleRegistrationsSample
{
    internal class Service2 : IService //Or ISubService2 which will still be considered as an IService
    {
        private readonly ILogger<Service2> logger;

        public Service2(ILogger<Service2> logger)
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