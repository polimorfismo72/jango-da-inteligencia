﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DevJANGO.Business.Models
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid(); 
        }

        public Guid Id { get; set; }
    }
}
