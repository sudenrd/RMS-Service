using Application.Abstractions.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class Registration
    {
        public static void AddPersistenceRegistration(this IServiceCollection services)
        {
            services.AddScoped<IFC_Repository, FC_Repository>();
        }
    }
}
