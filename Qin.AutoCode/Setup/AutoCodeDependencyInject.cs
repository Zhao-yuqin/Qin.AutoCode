using Microsoft.Extensions.DependencyInjection;
using Qin.AutoCode.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Qin.AutoCode.Setup
{
    public static class AutoCodeDependencyInject
    {
        public static void AutoCodeInject(this IServiceCollection services)
        {
            services.AddSingleton<IAutoCodeProvider, AutoCodeProvider>();
            services.AddSingleton<IAutoCodeBuilder, AutoCodeBuilder>();
        }
    }
}
