using Lamar;
using Lamar.IoC.Instances;

namespace ConsoleApp2.DependencyInjection;

[LamarIgnore]
public class ActivationInterceptorPolicy : IDecoratorPolicy
{
    private readonly IActivationInterceptor _interceptor;
    private readonly Func<Instance, bool> _wrapEval;

    public ActivationInterceptorPolicy(IActivationInterceptor interceptor, Func<Instance, bool> wrapEval)
    {
        _interceptor = interceptor;
        _wrapEval = wrapEval;
    }

    public bool TryWrap(Instance inner, out Instance wrapped)
    {
        if (_wrapEval(inner))
        {
            wrapped = new InterceptingInstance(inner, _interceptor);
            return true;
        }

        wrapped = null;
        return false;
    }
}
