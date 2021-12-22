using System;
using Practics.Synchronizer.Core.Models.Base;

namespace Practics.Synchronizer.Core.Models.Events
{
    public record CreatedEvent<T>(T Payload) : Event(DateTime.Now) where T : class;
}