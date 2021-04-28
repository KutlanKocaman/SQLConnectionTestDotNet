using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLConnectionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            while (1 == 1)
            {
                TestSQLConnection();
            }
        }

        public static void TestSQLConnection()
        {
            Console.WriteLine("Would you like to use the connection string in the config file (c) or the connection builder (b)?");
            var connectionModeKey = Console.ReadKey();
            Console.WriteLine();

            string sqlConnectionString = "";

            if (connectionModeKey.Key == ConsoleKey.C)
            {
                sqlConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }
            else if (connectionModeKey.Key == ConsoleKey.B)
            {
                SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();

                Console.WriteLine("Provider the instance name then press Enter");
                sqlConnectionStringBuilder.DataSource = Console.ReadLine();

                Console.WriteLine("Provider the database name to connect to then press Enter");
                sqlConnectionStringBuilder.InitialCatalog = Console.ReadLine();

                Console.WriteLine("Would you like to use Windows (w) or SQL Authentication (s)?");
                var sqlAuthenticationKey = Console.ReadKey();
                Console.WriteLine();

                if (sqlAuthenticationKey.Key == ConsoleKey.W)
                    sqlConnectionStringBuilder.IntegratedSecurity = true;
                else if (sqlAuthenticationKey.Key == ConsoleKey.S)
                {
                    sqlConnectionStringBuilder.IntegratedSecurity = false;

                    Console.WriteLine("Enter the username to use to connect then press Enter");
                    sqlConnectionStringBuilder.UserID = Console.ReadLine();

                    Console.WriteLine("Enter the password to use to connect then press Enter");
                    sqlConnectionStringBuilder.Password = Console.ReadLine();
                }

                sqlConnectionString = sqlConnectionStringBuilder.ToString();
            }

            Console.WriteLine("The connection string is:");
            Console.WriteLine(sqlConnectionString);
            Console.WriteLine("Attempting to connect...");

            if (sqlConnectionString != null && sqlConnectionString.Trim() != "")
            {
                try
                {
                    using (var sqlConnection = new SqlConnection(sqlConnectionString))
                    {
                        sqlConnection.Open();
                        Console.WriteLine("Connected");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            else
            {
                Console.WriteLine("The connection string was empty so a connection was not attempted.");
            }
        }
    }
}
