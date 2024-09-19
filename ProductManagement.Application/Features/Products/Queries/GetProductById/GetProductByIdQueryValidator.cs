﻿using FluentValidation;

namespace ProductManagement.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Product ID must be greater than zero.");
        }
    }
}
