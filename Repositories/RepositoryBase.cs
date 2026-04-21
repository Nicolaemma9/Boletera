// esta clase sera mi conexxion a base de datos

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boletera.Repositories
{
public abstract class RepositoryBase
    {
        private readonly string _connectionString;
        public RepositoryBase()
        {
            _connectionString = "Server = DESKTOP-MRJ338U\\VSGESTION;"
                + "Database = DataBaseProyectoUno;"
                + "Integrated Security = true";
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
