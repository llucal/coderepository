using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebExperience.Domain.Entities;

namespace WebExperience.Domain.DataAccess
{
    public interface IAssetRepository
    {

        List<Asset> Assets { get; }


        Asset GetAsset(string assetId);

        Asset AddAsset(Asset asset);

        bool DeleteAsset(string assetId);

        bool UpdateAsset(Asset asset);




    }
}
