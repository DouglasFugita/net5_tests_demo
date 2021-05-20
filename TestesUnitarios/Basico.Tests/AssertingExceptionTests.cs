using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Basico.Tests
{
    public class AssertingExceptionTests
    {
        [Fact]
        public void Calculadora_Dividir_DeveRetornarErroDivisaoPorZero()
        {
            //Arrange
            var calculadora = new Calculadora();

            //Act - Assert
            Assert.Throws<DivideByZeroException>(() => calculadora.Dividir(10, 0));
        }

        [Fact]
        public void Funcionario_Salario_DeveRetornarErroSalarioInferiorPermitido()
        {
            // AAA
            var exception = Assert.Throws<Exception>(() => FuncionarioFactory.Criar("Lorem", 250));

            Assert.Equal("Salario inferior ao permitido", exception.Message);
        }
    }
}
