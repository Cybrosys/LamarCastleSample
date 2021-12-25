using Lamar;
using Lamar.IoC;
using Lamar.IoC.Frames;
using Lamar.IoC.Instances;
using LamarCodeGeneration.Model;

namespace ConsoleApp2.DependencyInjection;

public class InterceptingInstance : Instance
{
    private readonly Instance _inner;
    private readonly IActivationInterceptor _interceptor;

    public InterceptingInstance(Instance inner, IActivationInterceptor interceptor)
        : base(inner.ServiceType, inner.ImplementationType, inner.Lifetime)
    {
        _inner = inner;
        _interceptor = interceptor;
        Name = _inner.Name;
    }

    protected override IEnumerable<Instance> createPlan(ServiceGraph services)
    {
        _inner.CreatePlan(services);
        foreach (var message in _inner.ErrorMessages)
            ErrorMessages.Add(message);
        return base.createPlan(services);
    }

    public override Variable CreateVariable(BuildMode mode, ResolverVariables variables, bool isRoot) => new GetInstanceFrame(this).Variable;

    public override Func<Scope, object> ToResolver(Scope topScope) => s => Resolve(s);

    public override object Resolve(Scope scope)
    {
        var instance = _inner.Resolve(scope);
        var resolved = ResolvedInternal(instance, scope);
        return resolved;
    }

    public override object QuickResolve(Scope scope)
    {
        var instance = _inner.QuickResolve(scope);
        var resolved = ResolvedInternal(instance, scope);
        return resolved;
    }

    private object ResolvedInternal(object instance, Scope scope)
    {
        return _interceptor.Intercept(ServiceType, instance, scope);
    }

    public override string ToString() => $"Intercepting instance for {_inner.ImplementationType.Name} for service {_inner.ServiceType.Name}";
}
