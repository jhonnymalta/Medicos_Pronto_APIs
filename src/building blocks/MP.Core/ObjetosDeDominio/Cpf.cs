using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP.Core.ObjetosDeDominio
{
    public class Cpf
    {
        public const int CpfMaxLength = 11;
        public string Numero { get; private set; }

        protected Cpf(){ }

        public Cpf(string numero)
        {
            Numero = numero;
        }

        public static bool Validar(string cpf)
        {
            if (cpf.Length > 11)
                return false;

            while (cpf.Length != 11)
                cpf = '0' + cpf;

            var igual = true;
            for (int i = 1; i < 11 && igual; i++)
            {
                if (cpf[i] != cpf[0])
                    igual = false;
            }
            return true;
        }

    }
}
