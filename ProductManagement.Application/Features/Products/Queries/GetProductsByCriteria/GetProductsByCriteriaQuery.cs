using MediatR;
using ProductManagement.Application.DTOs;
using System.Collections.Generic;

namespace ProductManagement.Application.Features.Products.Queries.GetProductsByCriteria
{
    public class GetProductsByCriteriaQuery : IRequest<List<ProductDto>>
    {
        public string? Name { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
