using Domain.Repository;
using Infrastructure.Setup;

namespace Infrastructure.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly DatabaseContext _databaseContext;

        public ClientRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
    }
}
