using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Modelo
{
    public class RepositoryUoW : EFRepository.RepositoryUoW, IDisposable, IUnitOfWork
    {
        public RepositoryUoW(
           bool autoDetectChangesEnabled = false,
           bool proxyCreationEnabled = false) :
           base(new VentasEntities(), autoDetectChangesEnabled, proxyCreationEnabled)
        {

        }
    }
}
