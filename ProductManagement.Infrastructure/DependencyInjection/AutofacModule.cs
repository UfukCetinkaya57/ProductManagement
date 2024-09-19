using Autofac;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ProductManagement.Application.Features.Products.Commands.CreateProduct;
using ProductManagement.Application.MappingProfiles;
using ProductManagement.Application.Services;
using ProductManagement.Core.Interfaces;
using ProductManagement.Infrastructure.Services;
using ProductManagement.Persistence.Contexts;
using ProductManagement.Persistence.Repositories;
using Serilog;
using ILogger = Serilog.ILogger;

namespace ProductManagement.Infrastructure.DependencyInjection
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Repository'leri Autofac ile kaydedin
            builder.RegisterType<ProductRepository>()
                   .As<IProductRepository>()
                   .InstancePerLifetimeScope();

            // Servisleri Autofac ile kaydedin
            builder.RegisterType<JwtService>()
                   .As<IJwtService>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<UserService>()
                   .As<IUserService>()
                   .InstancePerLifetimeScope();

    

        }
    }
}
