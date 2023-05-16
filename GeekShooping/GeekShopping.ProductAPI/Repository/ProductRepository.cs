﻿using AutoMapper;
using GeekShopping.ProductAPI.Data.Dto;
using GeekShopping.ProductAPI.Model;
using GeekShopping.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MySqlContext _context;
        private IMapper _mapper;

        public ProductRepository(MySqlContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> FindAll()
        {
            List<Product> products = await _context.Products.ToListAsync();

            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<ProductDto> FindById(long id)
        {
            Product products = await _context.Products.Where( p=> p.Id == id).FirstOrDefaultAsync();

            return _mapper.Map<ProductDto>(products);
        }

        public async Task<ProductDto> Create(ProductDto productDto)
        {
            Product product = _mapper.Map<Product>(productDto);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);

        }
        public async Task<ProductDto> Update(ProductDto productDto)
        {
            Product product = _mapper.Map<Product>(productDto);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        }
        public async Task<bool> DeleteById(long id)
        {
            try
            {
                Product products = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();

                if(products != null) 
                    return false;
                _context.Products.Remove(products);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }
    }
}
