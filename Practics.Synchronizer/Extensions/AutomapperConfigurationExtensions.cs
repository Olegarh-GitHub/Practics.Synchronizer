using System;
using Microsoft.Extensions.DependencyInjection;
using Practics.Synchronizer.BLL.MappingProfiles;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Practics.Synchronizer.Extensions
{
    public static class AutomapperConfigurationExtensions
    {
        public static void AddAutoMapper(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper
            (
                typeof(RawEntityToImportedEntityProfile), 
                typeof(ImportedEntityUpdateProfile),
                typeof(ImportedEntityToDTOProfile)
            );
        }
    }
}