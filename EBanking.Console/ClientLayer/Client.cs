using EBanking.Console.Brokers;
using EBanking.Console.Model;

namespace EBanking.Console.ClientLayer
{
    internal class Client
    {
        private IBroker? broker;
        public void SetBroker(IBroker broker)
        {
            this.broker = broker;
        }
        public async Task Create()
        {
            if (broker == null) return;
            await broker.CreateEntityFromInput();
        }
        public async Task Delete()
        {
            if (broker == null) return;
            await broker.DeleteEntityFromInput();
        }
        public async Task Update()
        {
            if (broker == null) return;
            await broker.UpdateEntityFromInput();
        }
        public async Task GetOne()
        {
            if (broker == null) return;
            await broker.GetEntityFromInput();
        }
        public async Task GetAll()
        {
            if (broker == null) return;
            await broker.GetEntitiesFromInput();
        }
    }
}
