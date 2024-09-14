using CommonExtensions.Models;

namespace CacheService.Interfaces
{
    public interface IMetaData
    {
     List<ConnectionStringModel> GetConnections();
        ConnectionStringModel GetConnectionById(int id);
    }
}
