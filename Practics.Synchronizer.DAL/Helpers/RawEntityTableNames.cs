using System;
using System.Collections.Generic;
using Practics.Synchronizer.DAL.Models;

namespace Practics.Synchronizer.DAL.Helpers
{
    public static class RawEntityTableNames
    {
        private static readonly Dictionary<Type, string> _tableNames = new()
        {
            { typeof(DepartmentRaw), "[biview].[dbo].[Practics_Departments]" },
            { typeof(PersonContactsRaw), "[biview].[dbo].[Practics_PersonsContacts]" },
            { typeof(PersonRaw), "[biview].[dbo].[Practics_Persons]" },
            { typeof(WorkerRaw), "[biview].[dbo].[Practics_Workers]" },
            { typeof(AwardStatusRaw), "[biview].[dbo].[Practics_AwardStatuses]" },
        };
        
        public static string GetTable(Type rawType)
        {
            if (!_tableNames.ContainsKey(rawType))
                throw new KeyNotFoundException();
            
            return _tableNames[rawType];
        }
    }
}