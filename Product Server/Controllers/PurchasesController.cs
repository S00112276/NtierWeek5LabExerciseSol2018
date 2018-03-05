using AutoMapper;
using Common_Objects;
using Product_Server.Models.DAL;
using Product_Server.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Product_Server.Controllers
{
    [Authorize(Roles="Purchases Manager")]
    [RoutePrefix("api/Purchases")]
    public class PurchasesController : ApiController
    {
        
        private SupplierProductRepository context;
        //private MapperConfiguration supConfig, prodConfig;

        public PurchasesController()
        {
            context = new SupplierProductRepository(new ProductDbContext());
            //prodConfig = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>());
            //supConfig = new MapperConfiguration(cfg => cfg.CreateMap<Supplier, SupplierDTO>());
        }
        // For injection
        public PurchasesController(SupplierProductRepository ctx)
        {
            context = ctx;
        }

        [HttpGet]
        [Route("Suppliers")]
        public async Task<IList<Supplier>> GetSuppliers()
        {
           return await (context as ISupplierRepository).getEntities();
        }

        [HttpGet]
        [Route("Products")]
        public async Task<IList<Product>> GetProducts()
        {
            return await (context as IProductRepository).getEntities();
        }

        [HttpGet]
        [Route("ReorderList")]
        public async Task<IList<Product>> GetReorderList()
        {
            return await (context as IProductRepository).GetReorderList();
        }

        [HttpPost]
        [Route("add/SupplierProduct")]
        public async Task<Product> postProductSupplier(ProductDTO newProduct)
        {
            // Here we get a Product DTO and create a new product based on the DTO 
            // with associated supplier 
            // We call the Post Entity method to add and update the db context
            // We return the resulting inserted object which ha all the fields filled in 
            // as a result of the Insertion
            Product inserted = await (context as IProductRepository).PostEntity(
                new Product { Description=newProduct.Description,
                                 associatedSupplier = new Supplier
                                 {
                                      Address = newProduct.Supplier.Address,
                                       Name = newProduct.Supplier.Name
                                 },
                                 Price = newProduct.Price,
                                  Quantity = newProduct.Quantity,
                                   ReorderLevel = newProduct.ReorderLevel
                });
            return inserted;
        }

        [HttpDelete]
        [Route("delete/Supplier/{sid:int}")]
        public async Task<Supplier> DeleteSupplier(int sid)
        {
            return await (context as ISupplierRepository).delete(sid);
        }
    }
}
