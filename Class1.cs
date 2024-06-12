using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace AssetManagementSystemLibrary
{
    public class Asset
    {
        public int AssetID { get; set; }
        public string AssetName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal PurchaseCost { get; set; }
        public int CategoryID { get; set; }
        public int SubcategoryID { get; set; }
        public decimal SalvageValue { get; set; }
        public int UsefulLife { get; set; }
        public decimal DepreciationValue { get; set; }
    }

    public class Employee
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
    }

    public class AssetIssuedToEmployee
    {
        public int AssetID { get; set; }
        public string AssetName { get; set; }
        public string EmployeeName { get; set; }
        public DateTime IssueDate { get; set; }
    }

    public class AssetDepreciation
    {
        public int AssetID { get; set; }
        public string AssetName { get; set; }
        public decimal DepreciationValue { get; set; }
    }

    public class AssetMaintenance
    {
        public int AssetID { get; set; }
        public string AssetName { get; set; }
        public DateTime RenewalDate { get; set; }
    }

    public class AssetManagementLibrary
    {
        private readonly string connectionString = "Server=ARYAN\\SQLEXPRESS01;Database=AssetManagementSystem;Integrated Security=True;Encrypt=False;";


        private SqlConnection connection;

        public void ConnectToDatabase()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Connected to SQL Server");
        }

        public List<AssetIssuedToEmployee> GetIssuedAssets()
        {
            var issuedAssets = new List<AssetIssuedToEmployee>();

            string query = "SELECT a.asset_id, a.asset_name, e.employee_name, ia.issue_date " +
                           "FROM Issued_Assets ia " +
                           "JOIN ASSET a ON ia.asset_id = a.asset_id " +
                           "JOIN Employees e ON ia.employee_id = e.employee_id";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        issuedAssets.Add(new AssetIssuedToEmployee
                        {
                            AssetID = reader.GetInt32(0),
                            AssetName = reader.GetString(1),
                            EmployeeName = reader.GetString(2),
                            IssueDate = reader.GetDateTime(3)
                        });
                    }
                }
            }

            return issuedAssets;
        }

        public List<AssetDepreciation> GetDepreciatedAssets()
        {
            var depreciatedAssets = new List<AssetDepreciation>();

            string query = "SELECT asset_id, asset_name, depreciation_value FROM ASSET";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        depreciatedAssets.Add(new AssetDepreciation
                        {
                            AssetID = reader.GetInt32(0),
                            AssetName = reader.GetString(1),
                            DepreciationValue = reader.GetDecimal(2)
                        });
                    }
                }
            }

            return depreciatedAssets;
        }

        public List<AssetIssuedToEmployee> GetAssetsIssuedToPeople()
        {
            var assetsIssuedToPeople = new List<AssetIssuedToEmployee>();

            string query = "SELECT a.asset_id, a.asset_name, e.employee_name, ia.issue_date " +
                           "FROM Issued_Assets ia " +
                           "JOIN ASSET a ON ia.asset_id = a.asset_id " +
                           "JOIN Employees e ON ia.employee_id = e.employee_id";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        assetsIssuedToPeople.Add(new AssetIssuedToEmployee
                        {
                            AssetID = reader.GetInt32(0),
                            AssetName = reader.GetString(1),
                            EmployeeName = reader.GetString(2),
                            IssueDate = reader.GetDateTime(3)
                        });
                    }
                }
            }

            return assetsIssuedToPeople;
        }

        public List<AssetMaintenance> GetAssetsWithMaintenance()
        {
            var assetsWithMaintenance = new List<AssetMaintenance>();

            string query = "SELECT am.asset_id, a.asset_name, am.renewal_date " +
                           "FROM Asset_Maintenance am " +
                           "JOIN ASSET a ON am.asset_id = a.asset_id";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        assetsWithMaintenance.Add(new AssetMaintenance
                        {
                            AssetID = reader.GetInt32(0),
                            AssetName = reader.GetString(1),
                            RenewalDate = reader.GetDateTime(2)
                        });
                    }
                }
            }

            return assetsWithMaintenance;
        }

        public void Dispose()
        {
            if (connection != null)
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }
}
