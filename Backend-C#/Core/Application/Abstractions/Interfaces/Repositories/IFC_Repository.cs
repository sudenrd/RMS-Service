using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Interfaces.Repositories
{
    public interface IFC_Repository : IRepository_Base <FC>
    {
        Task<List<FC>> GetAllAsync();
        List<FC> getByTime(DateTime start, DateTime end);
    }
}
