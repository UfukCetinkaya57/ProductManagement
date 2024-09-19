using MediatR;
using ProductManagement.Application.DTOs;
using System.Collections.Generic;

namespace ProductManagement.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<List<ProductDto>>
    {
    }
}