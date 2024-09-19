using AutoMapper;
using MediatR;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Features.Products.Queries.GetProductsByCriteria;
using ProductManagement.Core.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProductManagement.Application.Queries.GetProductsByCriteria
{
    public class GetProductsByCriteriaQueryHandler : IRequestHandler<GetProductsByCriteriaQuery, List<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductsByCriteriaQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> Handle(GetProductsByCriteriaQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetByCriteriaAsync(request.Name, request.MinPrice, request.MaxPrice);
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
