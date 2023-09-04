using Microsoft.Extensions.DependencyInjection;
using SwiftCode.BBS.Extensions.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftCode.BBS.Extensions.ServiceExtensions
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddAutoMapper(typeof(AutoMapperConfig));
        }
    }
}
