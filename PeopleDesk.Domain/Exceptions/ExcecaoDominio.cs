using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleDesk.Domain.Exceptions
{
    public class ExcecaoDominio : Exception
    {
        public ExcecaoDominio(string mensagem) : base(mensagem)
        {
            
        }
    }
}
