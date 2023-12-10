using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using quanlihosonhansu.Connection;

namespace quanlihosonhansu.Connection
{
    internal class Connection
    {
        private static string 
        stringConnection = @"Data Source=LAPTOP-0S8UO1DE\MAYAO;Initial Catalog=qlinhansu;Persist Security Info=True;";
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(stringConnection); 
        } 
    }
}
