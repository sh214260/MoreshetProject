using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class ServicesConfig
    {
        public static void AddBLServices(this IServiceCollection collection)
        {
            collection.AddScoped(typeof(Repositories.Interfaces.IUserRepository), typeof(Repositories.UserRepository));
            collection.AddScoped(typeof(Repositories.Interfaces.ICategoryRepository), typeof(Repositories.CategoryRepository));
            //collection.AddScoped(typeof(Repositories.Interfaces.IItemForOrderRepository), typeof(Repositories.ItemForOrderRepository));
            collection.AddScoped(typeof(Repositories.Interfaces.IOrderRepository), typeof(Repositories.OrderRepository));
            collection.AddScoped(typeof(Repositories.Interfaces.IProductRpository), typeof(Repositories.ProductRepository));
            collection.AddScoped(typeof(Repositories.Interfaces.ICartProductRepository), typeof(Repositories.CartProductRepository));
            collection.AddScoped(typeof(Repositories.Interfaces.ICartRepository), typeof(Repositories.CartRepository));


            collection.AddScoped(typeof(Services.Interfaces.IUserService), typeof(Services.UserService));
            collection.AddScoped(typeof(Services.Interfaces.ICategoryService), typeof(Services.CategoryService));
            //collection.AddScoped(typeof(Services.Interfaces.IItemForOrderService), typeof(Services.ItemForOrderService));
            collection.AddScoped(typeof(Services.Interfaces.IOrderService), typeof(Services.OrderService));
            collection.AddScoped(typeof(Services.Interfaces.IProductService), typeof(Services.ProductService));
            collection.AddScoped(typeof(Services.Interfaces.ICartProduct), typeof(Services.CartProduct));
            collection.AddScoped(typeof(Services.Interfaces.ICartService), typeof(Services.CartService));


            collection.AddDbContext<Repositories.Models.FullStackMoreshetdbContext>();
            var mapping = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapping.CreateMapper();
            collection.AddSingleton(mapper);
        }
        
    }
}
