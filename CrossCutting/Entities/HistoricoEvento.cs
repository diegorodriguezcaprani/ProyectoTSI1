﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class HistoricoEvento
    {
        public int Id { get; set; }
        public int EventoId { get; set; }
        public DateTime Fecha { get; set; }
        public String ValorCritico { get; set; }
    }
}
