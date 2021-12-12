using System.Linq.Expressions;

namespace WinFormsClient.Presentation.Common;

public interface IContainer
{
    void Register<TService, TImplementation>() where TImplementation : TService;
    void Register<TService>();
    void RegisterInstance<T>(T instance);
    TService Resolve<TService>();
    bool IsRegistered<TService>();
    void Register<TService, TArgument>(Expression<Func<TArgument, TService>> factory);
}