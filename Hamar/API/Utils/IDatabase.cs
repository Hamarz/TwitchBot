using MySql.Data.MySqlClient;

namespace Hamar.API.Utils
{
    public interface IDatabase
    {
        MySqlDataReader Query(string query, params MySqlParameter[] param);
        void NonQuery(string query, params MySqlParameter[] param);
        string GetStatus();
    }
}
