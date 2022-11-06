using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBanking.Console.Brokers
{
    internal interface IBroker
    {
        Task CreateEntityFromInput();
        Task DeleteEntityFromInput();
        Task UpdateEntityFromInput();
        Task GetEntityFromInput();
        Task GetEntitiesFromInput();
    }
}
