using Autofac;

namespace TME.Interfaces
{
    public interface IDependencyContainer
    {
        public IContainer CurrentContainer { get; set; }
    }
}
