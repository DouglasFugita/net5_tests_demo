using Features.Core;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Features.Tests
{
    public class CpfValidationTests
    {
        [Theory(DisplayName="CPF Validos")]
        [Trait("Categoria", "CPF Validation Tests")]
        [InlineData("15231766607")]
        [InlineData("78246847333")]
        [InlineData("64184957307")]
        [InlineData("21681764423")]
        public void Cpf_ValidarMultiplosNumero_TodosDevemSerValidos(string cpf)
        {
            // Assert
            var cpfValidation = new CpfValidation();

            // Act - Assert
            cpfValidation.EhValido(cpf).Should().BeTrue();
        }
    }
}
