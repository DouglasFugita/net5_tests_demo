using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Basico.Tests
{
    public class AssertingCollectionTests
    {
        [Fact]
        public void Funcionario_Habilidades_NaoDevePossuirHabilidadesVazias()
        {
            // Arrange - Act
            var funcionario = FuncionarioFactory.Criar("Lorem", 10000);

            //Assert
            Assert.All(funcionario.Habilidades, habilidades => Assert.False(string.IsNullOrWhiteSpace(habilidades)));
        }

        [Fact]
        public void Funcionario_Habilidades_JuniorDevePossuirHabilidadeBasica()
        {
            // Arrange - Act
            var funcionario = FuncionarioFactory.Criar("Lorem", 10000);

            //Assert
            Assert.Contains("OOP", funcionario.Habilidades);
        }

        [Fact]
        public void Funcionario_Habilidades_JuniorNaoDevePossuirHabilidadeAvancadas()
        {
            // Arrange - Act
            var funcionario = FuncionarioFactory.Criar("Lorem", 1000);

            //Assert
            Assert.DoesNotContain("Microservices", funcionario.Habilidades);
        }

        [Fact]
        public void Funcionario_Habilidades_SeniorDevePossuirTodasHabilidades()
        {
            // Arrange - Act
            var funcionario = FuncionarioFactory.Criar("Lorem", 10000);

            var habilidades = new[]
            {
                "Logica de Programacao",
                "OOP",
                "Testes",
                "Microservices"
            };

            //Assert
            Assert.Equal(habilidades, funcionario.Habilidades);
        }

    }
}
