using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Basico.Tests
{
    public class AssertingNumbersTests
    {
        [Fact]
        public void Calculadora_Somar_DeveSerIgual()
        {
            //Arrange
            var calculadora = new Calculadora();

            // Act
            var result = calculadora.Somar(1, 2);

            //Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void Calculadora_Somar_NaoDeveSerIgual()
        {
            //Arrange
            var calculadora = new Calculadora();

            // Act
            var result = calculadora.Somar(1.1312312, 2.231221
                );

            //Assert
            Assert.NotEqual(3.3, result, precision:1);
        }

        //08:40 M03V06
    }
}
