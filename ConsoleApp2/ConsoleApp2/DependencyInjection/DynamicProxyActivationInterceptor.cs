using Castle.DynamicProxy;
using Lamar;
using Lamar.IoC;

namespace ConsoleApp2.DependencyInjection;

[LamarIgnore]
public class DynamicProxyActivationInterceptor : IActivationInterceptor
{
    private static ProxyGenerator _proxyGenerator = new ProxyGenerator();
    private readonly IInterceptor[] _interceptors;

    public DynamicProxyActivationInterceptor(params IInterceptor[] interceptors)
    {
        _interceptors = interceptors;
    }

    public object Intercept(Type serviceType, object instance, Scope scope) => _proxyGenerator.CreateInterfaceProxyWithTarget(serviceType, instance, _interceptors);
}
