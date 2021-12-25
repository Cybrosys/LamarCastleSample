using Lamar.IoC;

namespace ConsoleApp2.DependencyInjection;

public interface IActivationInterceptor
{
    object Intercept(Type serviceType, object instance, Scope scope);
}
