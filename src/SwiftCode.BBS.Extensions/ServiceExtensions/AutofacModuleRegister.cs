using Autofac;
using SwiftCode.BBS.IRepositories;
using SwiftCode.BBS.IRepositories.Base;
using SwiftCode.BBS.IServices;
using SwiftCode.BBS.IServices.Base;
using SwiftCode.BBS.Repositories.Base;
using SwiftCode.BBS.Services.Base;
using System.Reflection;

namespace SwiftCode.BBS.Extensions.ServiceExtensions;

public class AutofacModuleRegister : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        //var assemblysServices = Assembly.Load("SwiftCode.BBS.Services");
        //builder.RegisterAssemblyTypes(assemblysServices).AsImplementedInterfaces();

        //var assemblysRepositories = Assembly.Load("SwiftCode.BBS.Repositories");
        //builder.RegisterAssemblyTypes(assemblysRepositories).AsImplementedInterfaces();

        builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerDependency();
        builder.RegisterGeneric(typeof(BaseServices<>)).As(typeof(IBaseServices<>)).InstancePerDependency();


        var basePath = AppContext.BaseDirectory;

        var servicesDllFile = Path.Combine(basePath, "SwiftCode.BBS.Services.dll");
        var respositoryDllFile = Path.Combine(basePath, "SwiftCode.BBS.Repositories.dll");
        if (!File.Exists(servicesDllFile) && File.Exists(respositoryDllFile))
        {
            var msg = $"文件不存在";
            throw new FileNotFoundException(msg);
        }

        var assemblysServices = Assembly.LoadFrom(servicesDllFile);
        builder.RegisterAssemblyTypes(assemblysServices).AsImplementedInterfaces();

        var assemblysRepositories = Assembly.LoadFrom(respositoryDllFile);
        builder.RegisterAssemblyTypes(assemblysRepositories).AsImplementedInterfaces();
    }
}
