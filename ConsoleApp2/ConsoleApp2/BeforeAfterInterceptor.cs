using Castle.DynamicProxy;
using Lamar;

namespace ConsoleApp2;

[LamarIgnore]
public class BeforeAfterInterceptor : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        if (invocation == null)
            throw new ArgumentNullException(nameof(invocation));

        Console.WriteLine("Before");

        try
        {
            invocation.Proceed();
        }
        finally
        {
            Console.WriteLine("After");
        }
    }
}
