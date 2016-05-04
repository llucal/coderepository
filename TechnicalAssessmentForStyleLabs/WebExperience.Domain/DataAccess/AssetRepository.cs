using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using WebExperience.Domain.Entities;

namespace WebExperience.Domain.DataAccess
{
    public class AssetRepository : IAssetRepository
    {
        private static List<Asset> _assets = GetAssets();


        public List<Asset> Assets
        {
            get { return _assets; }
        }


        public Asset GetAsset(string assetId)
        {
            return _assets.FirstOrDefault(a => a.AssetId.Equals(assetId));
        }

        public Asset AddAsset(Asset asset)
        {

            if (asset == null)
                return null;

            var existingAsset =
                _assets.FirstOrDefault(a => a.FileName.Equals(asset.FileName));

            if (existingAsset != null)
                return null;

            asset.AssetId = Guid.NewGuid().ToString();

            _assets.Add(asset);

            return asset;
        }

        public bool DeleteAsset(string assetId)
        {
            var existingAsset = _assets.FirstOrDefault(a => a.AssetId.Equals(assetId));

            if (existingAsset == null)
                return false;

            _assets.Remove(existingAsset);

            return true;
        }

        public bool UpdateAsset(Asset asset)
        {

            var existingAsset = _assets.FirstOrDefault(a => a.AssetId.Equals(asset.AssetId));


            if (existingAsset == null)
                return false;

            existingAsset.FileName = asset.FileName;
            existingAsset.Country = asset.Country;
            existingAsset.CreatedBy = asset.CreatedBy;
            existingAsset.Description = asset.Description;
            existingAsset.Email = asset.Email;
            existingAsset.MimeType = asset.MimeType;


            return true;
        }


        public static List<Asset> GetAssets()
        {
            var csvFile = Resources.Resources.AssetImport;
            var list = new List<Asset>();

            using (var sr = new StringReader(csvFile))
            {
                var reader = new CsvReader(sr);

                //CSVReader will now read the whole file into an enumerable
                IEnumerable<Asset> records = reader.GetRecords<Asset>();

                //First 5 records in CSV file will be printed to the Output Window
                foreach (Asset record in records.Take(8))
                {
                    list.Add(record);
                }
            }

            return list;
        }
    }
}
