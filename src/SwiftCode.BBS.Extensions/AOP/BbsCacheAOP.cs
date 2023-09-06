using Castle.DynamicProxy;
using SwiftCode.BSS.Common.MemoryCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftCode.BBS.Extensions.AOP;

public class BbsCacheAOP : IInterceptor
{
    private readonly ICachingProvider _cachingProvider;

    public BbsCacheAOP(ICachingProvider cachingProvider)
    {
        _cachingProvider = cachingProvider;
    }
    public void Intercept(IInvocation invocation)
    {
        var cacheKey = CustomCacheKey(invocation);
        var cacheValue = _cachingProvider.Get(cacheKey);
        if(cacheValue != null)
        {
            invocation.ReturnValue = cacheValue;
            return;
        }
        invocation.ReturnValue = cacheValue;
        if(!string.IsNullOrWhiteSpace(cacheKey))
        {     
            _cachingProvider.Set(cacheKey, cacheValue);
        }
    }

    private string CustomCacheKey(IInvocation invocation)
    {
        var typeName = invocation.TargetType.Name;
        var methodName = invocation.Method.Name;
        var methodArguments = invocation.Arguments.Select(GetArgumentValue).Take(3).ToList();

        string key = $"{typeName}:{methodName}:";
        foreach(var arg in methodArguments)
        {
            key +=$"{arg}";

        }
        return key.TrimEnd(':');
    }
    private string GetArgumentValue(object arg)
    {
        if (arg is int || arg is long || arg is string)
            return arg.ToString();

        if (arg is DateTime time)
            return time.ToString("yyyyMMddHHmmss");

        return string.Empty;
    }
}
