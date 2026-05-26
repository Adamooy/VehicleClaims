using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleClaims.Domain.Common
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }

        private readonly List<IDomainEvent> _domainEvents = [];
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected Entity(Guid id)
        {
            Id = id;
        }

        protected void RaiseDomainEvent(IDomainEvent domainEvent)
            => _domainEvents.Add(domainEvent);

        public void ClearDomainEvents()
            => _domainEvents.Clear();
    }
}
