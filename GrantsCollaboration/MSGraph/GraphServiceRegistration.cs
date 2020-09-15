using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GrantsCollaboration.MSGraph
{
    public static class GraphServiceRegistration
    {
        public static void AddGraphService(this IServiceCollection services, IConfiguration configuration) 
            => services.Configure<GraphSettings>(configuration);
    }
}