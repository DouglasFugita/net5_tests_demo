using NStore.Core.DomainObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStore.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
