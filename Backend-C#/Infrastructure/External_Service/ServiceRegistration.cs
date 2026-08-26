using Application.Abstractions.Interfaces.Services;
using External_Service.Modbus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace External_Service
{
    public static class ServiceRegistration
    {
        public static void AddExternalRegistration(this IServiceCollection services)
        {
            services.AddSingleton<IModbusService, ModbusService>();
        }
    }
}
