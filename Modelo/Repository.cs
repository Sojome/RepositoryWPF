﻿using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFRepository;

namespace Modelo
{
    public class Repository : EFRepository.Repository,IDisposable,IRepository
    {
        public Repository(
           bool autoDetectChangesEnabled = false,
           bool proxyCreationEnabled = false) :
           base(new VentasEntities(), autoDetectChangesEnabled, proxyCreationEnabled)
        {

        }
               
    }
}
