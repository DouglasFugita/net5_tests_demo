using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Features.Tests._08_Skip
{
    public class TesteNaoPassandoPorMotivoEspecifico
    {
        [Fact(DisplayName = "Novo Cliente 2.0")]
        [Trait("Categoria","Escapando dos Testes")]
        public void Teste_NaoEstaPassando_VersaoNovaNaoCompativel()
        {
            Assert.True(true);
        }
    }
}
