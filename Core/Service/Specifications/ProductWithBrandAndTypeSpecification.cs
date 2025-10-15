using DomainLayer.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    internal class ProductWithBrandAndTypeSpecification:BaseSpecification<Product,int> 
    {
        //Get All
        //1- BrandId = null && TypeId = null =>   true                 && true
        //2- BrandId = null && TypeId = value =>  true                 && P.TypeId == typeId
        //3- BrandId = value && TypeId = null =>  P.BrandId == brandId && true
        //4- BrandId = value && TypeId = value => P.BrandId == brandId && P.Typeid == typeId
        public ProductWithBrandAndTypeSpecification(ProductQueryParams queryParams)
            : base(P => (!queryParams.brandId.HasValue || P.BrandId == queryParams.brandId)
            && (!queryParams.typeId.HasValue || P.TypeId == queryParams.typeId)
            && (string.IsNullOrEmpty(queryParams.SearchValue) || P.Name.ToLower().Contains(queryParams.SearchValue.ToLower())))
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P=>P.ProductType);
            switch (queryParams.sortingOptions)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(P => P.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(P => P.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(P => P.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(P => P.Price);
                    break;
                default:
                    break;
            }
            ApplyPagination(queryParams.PageSize, queryParams.pageIndex);
        }
        //Get By Id
        public ProductWithBrandAndTypeSpecification(int id):base(P=>P.Id==id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}
