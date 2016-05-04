using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebExperience.Domain.DataAccess;
using WebExperience.Domain.Entities;


namespace WebExperience.Controllers
{
    public class AssetController : ApiController
    {

        // TODO:
        // Create an API controller via REST to perform all CRUD operations on the asset objects created as part of the CSV processing test
        // Visualize the assets in a paged overview showing the title and created on field
        // Clicking an asset should navigate the user to a detail page showing all properties
        // Any data repository is permitted
        // Use a client MVVM framework
          private readonly IAssetRepository _repository;

        public AssetController(IAssetRepository repo)
        {
            _repository = repo;
        }
    
        [HttpGet()]
        public IHttpActionResult Get()
        {
            IHttpActionResult ret = null;

            var list = _repository.Assets;
            
            if (list.Count > 0)
            {
                ret = Ok(list);
            }
            else
            {
                ret = NotFound();
            }

            return ret;
        }
      


        // GET api/<controller>/5
        [HttpGet()]
        public IHttpActionResult Get(string id)
        {
            IHttpActionResult ret;

            var prod = _repository.GetAsset(id);
            
            if (prod == null)
            {
                ret = NotFound();
            }
            else
            {
                ret = Ok(prod);
            }

            return ret;
        }


        // POST api/<controller>
        [HttpPost()]
        public IHttpActionResult Post(Asset asset)
        {
            IHttpActionResult ret = null;

            var result = _repository.AddAsset(asset);
            
            if (result!=null)
            {
                ret = Created<Asset>(Request.RequestUri +
                                          result.AssetId,
                                          asset);
            }
            else
            {
                ret = NotFound();
            }

            return ret;
        }
     

        [HttpPut()]
        public IHttpActionResult Put(string id, Asset asset)
        {
            IHttpActionResult ret = null;

            if (_repository.UpdateAsset(asset))
            {
                ret = Ok(asset);
            }
            else
            {
                ret = NotFound();
            }

            return ret;
        }


        // DELETE api/<controller>/5
        [HttpDelete()]
        public IHttpActionResult Delete(string id)
        {
            IHttpActionResult ret = null;

            if (_repository.DeleteAsset(id))
            {
                ret = Ok(true);
            }
            else
            {
                ret = NotFound();
            }

            return ret;
        }
    

    }


}
