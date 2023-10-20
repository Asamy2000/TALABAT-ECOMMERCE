using E_commerce.Core;
using E_commerce.Core.IRepositories;
using E_commerce.Core.IServices;
using E_commerce.Errors;
using E_commerce.Helper;
using E_commerce.Repos;
using E_commerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services )
        {

            services.AddSingleton(typeof(IBasketRepo), typeof(BasketRepo));
            //register IGenericRepo
            //services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
          
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IOrderServices, OrderService>();
            services.AddScoped(typeof(IPaymentService), typeof(PaymentService));
            services.AddScoped<IResponseCacheService, ResponseCacheService>();
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actionContext) =>
                {

                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0).SelectMany(p => p.Value.Errors)
                                                           .Select(E => E.ErrorMessage).ToArray();

                    var validationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(validationErrorResponse);
                };
            });

            return services;
        }
    }
}
