using System.Threading.Tasks;

namespace MultipleRegistrationsSample
{
    internal interface IService
    {
        Task RunServiceAsync();
    }

    //You could create empty dummy interfaces to separate them in registration and injection instead,
    // implementation will still be considered as an IService as well...
    internal interface ISubService1 : IService { }

    internal interface ISubService2 : IService { }
}