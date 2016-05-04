using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CsvHelper;

namespace GeneralKnowledge.Test.App.Tests
{
    /// <summary>
    /// CSV processing test
    /// </summary>
    /// 
    public class CsvProcessingTest : ITest
    {

        

        public void Run()
        {
            // TODO: 
            // Create a domain model via POCO classes to store the data available in the CSV file below
            // Objects to be present in the domain model: Asset, Country and Mime type
            // Process the file in the most robust way possible
            // The use of 3rd party plugins is permitted

            var csvFile = Resources.AssetImport;
            
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("CSV Processing Test");
            Console.WriteLine();
            // Solution using an external library to read CSV file called CSVHelper

            
            var importedAssetListWithExternalLibraries = ImportAssetsFromCsvUsingExternalLibraries(csvFile);
            if (importedAssetListWithExternalLibraries == null || !importedAssetListWithExternalLibraries.Any())
            {
                Console.WriteLine("The CSV file doesn't contain any record");
                return;
            }
            
            Console.WriteLine(@"Using External Library CSVHelper....(only first 6 records..)");
            Console.WriteLine();
            PrintResults(importedAssetListWithExternalLibraries);


            Console.WriteLine();
            Console.WriteLine();

            
            // Solution using only internal libraries to read CSV 
            var importedAssetListWithInteralLibraries = ImportAssetsFromCsvUsingInternalLibraries(csvFile);
            if (importedAssetListWithInteralLibraries == null || !importedAssetListWithInteralLibraries.Any())
            {
                Console.WriteLine("The CSV file doesn't contain any record");
                return;
            }


            Console.WriteLine(@"Using only internal Libraries....(only first 6 records..");
            Console.WriteLine();
            PrintResults(importedAssetListWithInteralLibraries);




            Console.WriteLine("----------------------------------------");
     
        }


        private List<Asset> ImportAssetsFromCsvUsingExternalLibraries(string csvFile)
        {
            if (string.IsNullOrEmpty(csvFile))
            {
                return null;
            }
            var list = new List<Asset>();

            using (var sr = new StringReader(csvFile))
            {
                var reader = new CsvReader(sr);

                //CSVReader will now read the whole file into an enumerable
                IEnumerable<Asset> records = reader.GetRecords<Asset>();

                //First 5 records in CSV file will be printed to the Output Window
                foreach (Asset record in records.Take(5))
                {
                    list.Add(record);
                }
            }

            return list;
        }


        private List<Asset> ImportAssetsFromCsvUsingInternalLibraries(string csvFile)
        {
            if (string.IsNullOrEmpty(csvFile))
            {
                return null;
            }

            var list = new List<Asset>();
            using (TextReader sr = new StringReader(csvFile))
            {
                string line;
                var lineCounter = 1;
                while ((line = sr.ReadLine()) != null)
                {
                    if (lineCounter == 1)
                    {
                        lineCounter++;
                        continue;
                    }

                    IList<string> split = Regex.Split(line, ",");
                    var newAsset = new Asset
                    {
                        AssetId = split[0],
                        FileName = split[1],
                        MimeType = split[2],
                        CreatedBy = split[3],
                        Email = split[4],
                        Country = split[5],
                        Description = split[6]

                    };

                    list.Add(newAsset);

                }
            }

            return list;
        }

        private void PrintResults(List<Asset> assetList)
        {
            //print only first 6 records...
            foreach (var item in assetList.Take(6))
            {
                Console.WriteLine(item.AssetId + " " + item.FileName + " " + item.MimeType + " " + item.CreatedBy + " " +
                                  item.Email + " " + item.Country + " " + item.Description);
                Console.WriteLine();
            }
        }



    }


    internal class Asset
    {
        //asset id,file_name,mime_type,created_by,email,country,description

        public string AssetId { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public string CreatedBy { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }


    }


}
