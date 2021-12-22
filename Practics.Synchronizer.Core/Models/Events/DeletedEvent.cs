using System;
using Practics.Synchronizer.Core.Models.Base;

namespace Practics.Synchronizer.Core.Models.Events
{
    public record DeletedEvent<T>(int Id) : Event(DateTime.Now) where T : class;
}