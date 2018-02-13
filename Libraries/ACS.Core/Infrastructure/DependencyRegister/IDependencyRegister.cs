using Autofac;

namespace ACS.Core.Infrastructure.DependencyRegister
{
    public interface IDependencyRegister
    {
        void Register(ContainerBuilder builder, ITypeFinder typeFinder);

        int Order { get; }
    }
}
