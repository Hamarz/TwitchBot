using MySql.Data.MySqlClient;

namespace Hamar.Core
{
    public class MysqlDatabase : API.Utils.IDatabase
    {
        private MySqlConnection connection;
        private MySqlConnectionStringBuilder builder;

        public MysqlDatabase(string hostname, string username, string password, string database, uint port = 3306)
        {
            builder = new MySqlConnectionStringBuilder()
            {
                Server = hostname,
                UserID = username,
                Password = password,
                Database = database,
                Port = port
            };

            connection = new MySqlConnection(builder.GetConnectionString(true));

            Connect();
        }

        public void Connect()
        {
            try
            {
                connection.Open();
            }catch(MySqlException ex)
            {
                throw ex;
            }
        }

        public MySqlDataReader Query(string query, params MySqlParameter[] param)
        {
            var command = connection.CreateCommand();

            command.CommandText = query;

            foreach (var para in param)
                command.Parameters.Add(para);


            command.Prepare();

            return command.ExecuteReader();
        }

        public void NonQuery(string query, params MySqlParameter[] param)
        {
            var command = connection.CreateCommand();

            command.CommandText = query;

            foreach (var p in param)
                command.Parameters.Add(p);

            command.Prepare();

            command.ExecuteNonQuery();
        }

        public string GetStatus() { return connection.State.ToString(); }
    }
}
