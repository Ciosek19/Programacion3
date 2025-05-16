using System.Data.SqlClient;

namespace DataAccess.Persistencia
{
    public class PConexion
    {
        private string connectionString = "Server=C01\\SQLEXPRESS;Database=Almacen;User Id=Rodrigo;Password=rodrigo"; //Cambiar el connection string
        protected SqlConnection GetConnection() 
        {
            SqlConnection context = new SqlConnection(connectionString);
            return context;
        }
    }
}
