using Product_Server.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product_Server.Models.DAL
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        Task<IList<Product>> SupplierProducts();
    }
}
