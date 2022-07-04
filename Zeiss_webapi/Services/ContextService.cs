using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Zeiss_webapi.Providers;

namespace Zeiss_webapi.Services {
    public class ContextService {
        /// <summary>
        /// 配置各种服务
        /// </summary>
        /// <returns></returns>
        public static IServiceProvider ServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();
 
            services.AddDbContext<MyDbContext>(options => options.UseSqlite("DataSource=app.db;Cache=Shared"));
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
 
        /// <summary>
        /// 获取上下文
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static MyDbContext GetContext(IServiceProvider services)
        {
            var sqliteContext = services.GetService<MyDbContext>();
            return sqliteContext;
        }
 
        /// <summary>
        /// 获取上下文
        /// </summary>
        public static MyDbContext GetContext()
        {
            var services = ServiceProvider();
            var sqliteContext = services.GetService<MyDbContext>();
            return sqliteContext;
        }
    }
}
