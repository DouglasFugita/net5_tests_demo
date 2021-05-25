using NStore.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStore.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }

        private List<Event> _notificacoes;
        public IReadOnlyCollection<Event> Notificacoes => _notificacoes?.AsReadOnly();

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public void AdicionarEvento(Event evento)
        {
            _notificacoes = _notificacoes ?? new List<Event>();
            _notificacoes.Add(evento);
        }

        public void RemoverEvento(Event evento)
        {
            _notificacoes?.Remove(evento);
        }

        public void LimparEventos()
        {
            _notificacoes?.Clear();
        }

    }
}
