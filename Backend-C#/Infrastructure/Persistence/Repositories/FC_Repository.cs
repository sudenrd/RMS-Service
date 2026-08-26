using Application.Abstractions.Interfaces.Repositories;
using Domain.Entities;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class FC_Repository : Repository_Base<FC>, IFC_Repository
    {
        public FC_Repository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<FC>> GetAllAsync()
        {
            return await _context.FC.ToListAsync();
        }

        public List<FC> getByTime(DateTime start, DateTime end)
        {
            return _context.FC.Where(x => x.Timestamp >= start && x.Timestamp <= end).ToList();
        }
    }
}
