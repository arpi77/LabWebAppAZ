using HPlusSportsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPlusSportsApi.Services
{
    public interface ITableService
    {
        Task<List<OrderHistoryItem>> GetOrderHistoryAsync();
    }
}
