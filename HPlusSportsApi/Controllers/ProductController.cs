using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HPlusSportsApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HPlusSportsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        Services.IDocumentDBService docService;
        Services.IBlobService blobService;

        public ProductController(Services.IDocumentDBService docDbService, Services.IBlobService blobStorageService)
        {
            docService = docDbService;
            blobService = blobStorageService;
        }

        // GET api/product
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            List<ProductBase> products = await docService.GetProductsAsync();

            return new JsonResult(products);
        }



        // GET api/product/5
        [HttpGet("{id}")]
        public async Task<JsonResult> Get(string id)
        {
            var product = await docService.GetProductAsync(id);

            return new JsonResult(product);
        }


        [HttpPost]
        [Route("/api/[controller]/Nutritional")]
        public async Task<JsonResult> AddNutritional(NutritionalProduct product)
        {
            var newProduct = await docService.AddProductAsync<NutritionalProduct>(product);
            return new JsonResult(newProduct);
        }

        [HttpPost]
        [Route("/api/[controller]/Clothing")]
        public async Task<JsonResult> AddClothing(ClothingProduct product)
        {
            var newProduct = await docService.AddProductAsync<ClothingProduct>(product);
            return new JsonResult(newProduct);
        }

        [HttpPost]
        [Route("/api/[controller]/Image/{id}")]
        public async Task<IActionResult> AddImage(IFormFile imageFile)
        {
            string id = (string)RouteData.Values["id"];

            if (!Request.HasFormContentType)
                throw new UnsupportedMediaTypeException(
                    "You must post using multipart form data",
                    new System.Net.Http.Headers.MediaTypeHeaderValue(Request.ContentType));

            var filename = $"{id}.jpg";

            //BLOB Service: Get blob to write
            var blobRef = await blobService.GetBlobForWriteAsync(filename);

            try
            {
                using (var blobStream = blobRef.BlobStream)
                {
                    await imageFile.CopyToAsync(blobStream);
                }

                //update cosmos db with image link
                await docService.AddImageToProductAsync(id, blobRef.BlobUri);
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return Ok();
        }

    }
}
