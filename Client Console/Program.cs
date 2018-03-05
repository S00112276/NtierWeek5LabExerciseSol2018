using Common_Objects;
using Product_client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientApiLib.baseWebAddress = "http://localhost:49614/";
            if(ClientApiLib.login("fflynstone", "Flint$12345"))
            {
                List<ProductDTO> reorderList = ClientApiLib.getOrorderList();
                ProductDTO newprod = new ProductDTO
                {
                    Description = "glass hammers",
                    Price = 200f,
                    Quantity = 2,
                    ReorderLevel = 5,
                    Supplier = new SupplierDTO { Name = "Supplier 1", Address = "1 Sup Road" }
                };

                ProductDTO ProductInserted =  ClientApiLib.addSupplierProduct(newprod);
                if(ProductInserted != null)
                {
                    Console.WriteLine(ProductInserted.ToString());
                }

                SupplierDTO deleted = ClientApiLib.delete(ProductInserted.SupplierID);
                if(deleted != null)
                {
                    Console.WriteLine(deleted.ToString() + " Was deleted ");
                }

                foreach (var item in reorderList)
                {
                    Console.WriteLine("{0} {1} {2}", item.Description, item.ReorderLevel, item.Quantity);
                }
                Console.ReadKey();
            }
        }
    }
}
