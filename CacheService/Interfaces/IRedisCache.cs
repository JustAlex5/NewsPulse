using CacheService.Models;

namespace CacheService.Interfaces
{
    public interface IRedisCache
    {
        public  Task InsertConnectionStringsAsync(List<ConnectionStringModel> connectionStrings);
        public  Task FlushAllDatabasesAsync();
        public  Task<ConnectionStringModel> GetConnectionStringAsync(int id);
        public  Task<List<ConnectionStringModel>> GetAllConnectionStringsAsync();



    }
}
