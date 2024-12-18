using System.Data;
using Npgsql;

namespace WebMMP
{
    public class Database : IDisposable
    {
        public NpgsqlConnection table;
        public Database(string connectionString)
        {
            table = new NpgsqlConnection(connectionString);
            table.Open();
            if (table.FullState == ConnectionState.Broken || table.FullState == ConnectionState.Closed)
            {
                throw new Exception("Table not opened");
            }
        }

        public void Dispose()
        {
            table.Close();
        }
    }
}
