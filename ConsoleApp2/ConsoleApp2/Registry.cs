using ConsoleApp2.DependencyInjection;
using Lamar;

namespace ConsoleApp2;

internal class Registry : ServiceRegistry
{
    public Registry()
    {
        Policies.DecorateWith(new ActivationInterceptorPolicy(new DynamicProxyActivationInterceptor(new BeforeAfterInterceptor()), LogLevelInterceptorEval));

        Scan(x =>
        {
            x.TheCallingAssembly();
            x.ExcludeType<Registry>();
            x.WithDefaultConventions();
            x.SingleImplementationsOfInterface();
            x.RegisterConcreteTypesAgainstTheFirstInterface();
        });
    }

    private static bool LogLevelInterceptorEval(Lamar.IoC.Instances.Instance inner)
    {
        // If it's not an interface or the interface is not in the namespace we want then return false.
        if (!inner.ServiceType.IsInterface || !inner.ServiceType.Namespace?.StartsWith("ConsoleApp2.") != true)
            return false;

        // Everything checks out, return true.
        return true;
    }
}
