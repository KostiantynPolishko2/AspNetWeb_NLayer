﻿using AspNetWeb_NLayer.BLL.DTO;
using AspNetWeb_NLayer.BLL.Interfaces;
using AspNetWeb_NLayer.DAL.Interfaces;
using AspNetWeb_NLayer.BLL.Infrastructure;
using AutoMapper;
using AspNetWeb_NLayer.DAL.Entities;
using AspNetWeb_NLayer.BLL.BussinesModels;
using AspNetWeb_NLayer.DAL.Repositories;

namespace AspNetWeb_NLayer.BLL.Services
{
    public class ProductService :IProductService
    {
        public IUnitOfWork db { get; }

        public ProductService(IUnitOfWork uow) 
        { 
            db = uow;
        }
        
        public ProductItemDto getProductDto(string? name) => new ProductItemDto(getProductItem(name));

        public ProductItem getProductItem(string? name)
        {
            var productItem = db.productItems.getItem(name);
            if (productItem == null) throw new ProductItemException("absent productitem in db", name??"none");

            return productItem;
        }
        
        public IEnumerable<ProductItemDto> getAllProductsDto() 
        {
            var products = db.productItems.getAllItems();
            if (products is null) throw new ProductItemException("absent table", "products");

            IMapper mapper = new MapperConfiguration( c => c.CreateMap<ProductItem, ProductItemDto>()).CreateMapper();
            return mapper.Map<IEnumerable<ProductItem>, IEnumerable<ProductItemDto>>(products);
        }

        public ProductOrderDto getProductOrderDto(string? name, ClientTimeProperty cltTimeProps, ClientPayProperty cltPayProps)
        {
            var productItem = getProductItem(name);

            IMapper mapper = new MapperConfiguration(c => c.CreateMap<ProductItem, DTO.ProductOrderDto>()).CreateMapper();
            var productOrder = mapper.Map<ProductItem, DTO.ProductOrderDto>(productItem);

            cltTimeProps.EngineerType = productItem.typeEngeeniring;
            productOrder.timeStudy = new EducationTime(cltTimeProps).getTimeEducation(productItem.durationMonth);
            productOrder.sumPay = new EducationPayment(cltPayProps).getSumPayment(productItem.price);

            return productOrder;
        }

        public void Dispose()
        {
            db?.Dispose();
        }
    }
}
