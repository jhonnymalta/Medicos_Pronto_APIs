﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP.Core.Integration
{
    public abstract class IntegrationEvent : Event
    {
    }

    public class UsuarioRegistradoIntegrationEvent : IntegrationEvent {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        public UsuarioRegistradoIntegrationEvent(Guid id, string nome, string email, string cpf)
        {
            Id = id;   
            Nome = nome;
            Email = email;
            Cpf = cpf;
        }
    }
}
 