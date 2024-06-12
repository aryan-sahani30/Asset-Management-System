using System;
using System.Collections.Generic;
using AssetManagementSystemLibrary;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            AssetManagementLibrary assetManagementSystem = new AssetManagementLibrary();

            try
            {
                assetManagementSystem.ConnectToDatabase();

                List<AssetIssuedToEmployee> issuedAssets = assetManagementSystem.GetIssuedAssets();
                List<AssetDepreciation> depreciatedAssets = assetManagementSystem.GetDepreciatedAssets();
                List<AssetIssuedToEmployee> assetsIssuedToPeople = assetManagementSystem.GetAssetsIssuedToPeople();
                List<AssetMaintenance> assetsWithMaintenance = assetManagementSystem.GetAssetsWithMaintenance();

                Console.WriteLine("Assets Issued to Employees:");
                foreach (var asset in issuedAssets)
                {
                    Console.WriteLine($"Asset ID: {asset.AssetID}, Name: {asset.AssetName}, Issued To: {asset.EmployeeName}, Issue Date: {asset.IssueDate}");
                }

                Console.WriteLine("\nDepreciation Values:");
                foreach (var asset in depreciatedAssets)
                {
                    Console.WriteLine($"Asset ID: {asset.AssetID}, Name: {asset.AssetName}, Depreciation Value: {asset.DepreciationValue:C}");
                }

                Console.WriteLine("\nAssets Issued to People:");
                foreach (var asset in assetsIssuedToPeople)
                {
                    Console.WriteLine($"Asset ID: {asset.AssetID}, Name: {asset.AssetName}, Issued To: {asset.EmployeeName}, Issue Date: {asset.IssueDate}");
                }

                Console.WriteLine("\nAssets with Maintenance Renewal Date:");
                foreach (var asset in assetsWithMaintenance)
                {
                    Console.WriteLine($"Asset ID: {asset.AssetID}, Name: {asset.AssetName}, Maintenance Renewal Date: {asset.RenewalDate.ToShortDateString()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
