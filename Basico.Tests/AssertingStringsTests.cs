using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Basico.Tests
{
    public class AssertingStringsTests
    {
        [Fact]
        public void StringTools_UnirNomes_RetornarNomeCompleto()
        {
            //Arrange
            var stringTools = new StringTools();

            //Act
            var resultado = stringTools.Unir("Lorem", "Ipsum");

            //Assert
            Assert.Equal("Lorem Ipsum", resultado);
        }

        [Fact]
        public void StringTools_UnirNomes_IgnorarCase()
        {
            //Arrange
            var stringTools = new StringTools();

            //Act
            var resultado = stringTools.Unir("Lorem", "Ipsum");

            //Assert
            Assert.Equal("LOREM IPSUM", resultado, ignoreCase: true);
        }

        [Fact]
        public void StringsTools_UnirNomes_DeveConterTrecho()
        {
            //Arrange
            var stringTools = new StringTools();

            //Act
            var resultado = stringTools.Unir("Lorem", "Ipsum");

            //Assert
            Assert.Contains("rem Ip", resultado);
        }

        [Fact]
        public void StringsTools_UnirNomes_DeveComecarCom()
        {
            //Arrange
            var stringTools = new StringTools();

            //Act
            var resultado = stringTools.Unir("Lorem", "Ipsum");

            //Assert
            Assert.StartsWith("Lor", resultado);
        }

        [Fact]
        public void StringsTools_UnirNomes_DeveTerminarCom()
        {
            //Arrange
            var stringTools = new StringTools();

            //Act
            var resultado = stringTools.Unir("Lorem", "Ipsum");

            //Assert
            Assert.EndsWith("sum", resultado);
        }

        [Fact]
        public void StringsTools_UnirNomes_ValidarRegex()
        {
            //Arrange
            var stringTools = new StringTools();

            //Act
            var resultado = stringTools.Unir("Lorem", "Ipsum");

            //Assert
            Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", resultado);
        }
    }
}
