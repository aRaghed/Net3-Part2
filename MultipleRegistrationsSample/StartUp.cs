using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultipleRegistrationsSample
{
    internal class StartUp
    {
        private readonly ILogger<StartUp> logger;
        private readonly IEnumerable<IService> services;

        public StartUp(ILogger<StartUp> logger, IEnumerable<IService> services)
        {
            this.logger = logger;
            this.services = services;
        }

        public async Task RunAsync()
        {
            foreach (var service in services)
            {
                if (service is Service1)
                    logger.LogInformation("In RunAsync, IService implemented by Service1");
                else if (service is Service2)
                    logger.LogInformation("In RunAsync, IService implemented by Service2");
                else
                    logger.LogInformation("In RunAsync, unknown implementation of IService");

                await service.RunServiceAsync();
            }
        }
    }
}