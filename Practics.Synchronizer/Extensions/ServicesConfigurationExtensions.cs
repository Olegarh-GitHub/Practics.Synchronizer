using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Practics.Synchronizer.BLL.Services;
using Practics.Synchronizer.BLL.Services.ConvertServices;
using Practics.Synchronizer.Core.Interfaces;
using Practics.Synchronizer.Core.Models;
using Practics.Synchronizer.DAL.Models;
using Practics.Synchronizer.DAL.Repository.Dapper;
using Practics.Synchronizer.DAL.Repository.EntityFramework;

namespace Practics.Synchronizer.Extensions
{
    public static class ServicesConfigurationExtensions
    {
        public static void AddGenericServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(RawEntityService<>));
            services.AddTransient(typeof(ImportedEntityService<>));
            services.AddTransient(typeof(ImportedEntityUpdateService<,>));

            services.AddTransient<IRepository<AwardStatus, int>, EntityFrameworkGenericRepository<AwardStatus>>();
            services.AddTransient<IRepository<Department, int>, EntityFrameworkGenericRepository<Department>>();
            services.AddTransient<IRepository<Person, int>, EntityFrameworkGenericRepository<Person>>();
            services.AddTransient<IRepository<PersonContacts, int>, EntityFrameworkGenericRepository<PersonContacts>>();
            services.AddTransient<IRepository<Worker, int>, EntityFrameworkGenericRepository<Worker>>();

            services.AddTransient<IRawEntityToImportedEntityConvertService<AwardStatusRaw, AwardStatus>, RawEntityToImportedEntityConvertService<AwardStatusRaw, AwardStatus>>();
            services.AddTransient<IRawEntityToImportedEntityConvertService<DepartmentRaw, Department>, RawEntityToImportedEntityConvertService<DepartmentRaw, Department>>();
            services.AddTransient<IRawEntityToImportedEntityConvertService<PersonRaw, Person>, RawEntityToImportedEntityConvertService<PersonRaw, Person>>();
            services.AddTransient<IRawEntityToImportedEntityConvertService<PersonContactsRaw, PersonContacts>, PersonContactsRawToPersonContactsConvertService>();
            services.AddTransient<IRawEntityToImportedEntityConvertService<WorkerRaw, Worker>, WorkerRawToWorkerConvertService>();

            services.AddTransient<IRepository<AwardStatusRaw, string>, DapperGenericRepository<AwardStatusRaw>>();
            services.AddTransient<IRepository<DepartmentRaw, string>, DapperGenericRepository<DepartmentRaw>>();
            services.AddTransient<IRepository<PersonRaw, string>, DapperGenericRepository<PersonRaw>>();
            services.AddTransient<IRepository<PersonContactsRaw, string>, DapperGenericRepository<PersonContactsRaw>>();
            services.AddTransient<IRepository<WorkerRaw, string>, DapperGenericRepository<WorkerRaw>>();
        }
    }
}