using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftCode.BBS.Extensions.AOP;

public class BbsLogAOP : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        var dataIntercept = $"{DateTime.Now:yyyyMMddHHmmss}当前执行方法:{invocation.Method.Name},参数:{string.Join(",",invocation.Arguments.Select(x => (x ?? string.Empty).ToString().ToArray()))}";

		try
		{
			invocation.Proceed();
		}
		catch (Exception ex)
		{
            dataIntercept += $"方法执行中出现异常:{ex.Message}";
		}

		dataIntercept += $"被拦截方法执行完毕，返回结果:{invocation.ReturnValue}";

		var path = Directory.GetCurrentDirectory() + @"\Log";
		if(!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}

    }
}
