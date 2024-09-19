using Autofac;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ProductManagement.API.Filters;
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

            // FluentValidation validator'larını kaydedin
            builder.RegisterAssemblyTypes(typeof(CreateProductCommandValidator).Assembly)
                   .Where(type => type.IsClosedTypeOf(typeof(IValidator<>)))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
            // MediatR handler'larını kaydedin
            builder.RegisterAssemblyTypes(typeof(CreateProductCommand).Assembly)
                   .AsClosedTypesOf(typeof(IRequestHandler<,>))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            // GlobalExceptionFilter'ı kaydedin
            builder.RegisterType<GlobalExceptionFilter>()
                   .AsSelf()
                   .InstancePerLifetimeScope();


            // AutoMapper profillerini kaydedin
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                // Mapping profillerini ekleyin
                cfg.AddProfile<ProductMappingProfile>();
            })).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                // AutoMapper için IMapper servisini oluştur
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            }).As<IMapper>().InstancePerLifetimeScope();

            // Logger (Serilog) kaydedin
            builder.RegisterInstance(Log.Logger)
                   .As<ILogger>()
                   .SingleInstance();

            // DbContext kaydedin
            builder.RegisterType<ProductManagementDbContext>()
                   .AsSelf()
                   .InstancePerLifetimeScope();
        }
    }
}
