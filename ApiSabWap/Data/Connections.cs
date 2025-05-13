using Microsoft.Data.SqlClient;

namespace ApiSabWap.Data
{
    public class Connections
    {
        public SqlConnection sqlConnection;

        public Connections()
        {
            sqlConnection = new SqlConnection(
            new SqlConnectionStringBuilder()
            {
                DataSource = "10.255.255.34",
                InitialCatalog = "SabBMC",
                UserID = "user_sab_app_wap",
                Password = "DC550FA304EA45679720414D",
                Encrypt = false,
                TrustServerCertificate = true
            }.ConnectionString);
        }
    }
}
