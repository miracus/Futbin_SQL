using System.Data;
using System.Data.SqlClient;

namespace Futbin.SQL
{
    public static class Context
    {
        public static IDbConnection ConnectToSQL 
        {
            get
            {
                return new SqlConnection("Server=localhost\\SQLEXPRESS;Database=FutbinTest;Trusted_Connection=True;");
            }
        }
    }
}
