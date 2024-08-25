﻿using Prods.Models;

namespace Prods.Repository.IRepository
{
    public interface IProductRepository :IRepository<Product>
    {
       void Update(Product product);
    }
}
