using Features.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Features.Tests._02_Fixtures
{

    [CollectionDefinition(nameof(ClienteCollection))]
    public class ClienteCollection : ICollectionFixture<ClienteFixture>
    {

    }

    public class ClienteFixture : IDisposable
    {
        public Cliente GerarClienteValido()
        {
            var cliente = new Cliente(
                Guid.NewGuid(),
                "Lorem",
                "Ipsum",
                DateTime.Now.AddYears(-30),
                DateTime.Now,
                "a@a.com",
                true);

            return cliente;
        }

        public Cliente GerarClienteInvalido()
        {
            var cliente = new Cliente(
                Guid.NewGuid(),
                "",
                "",
                DateTime.Now,
                DateTime.Now,
                "a@a.com",
                true);

            return cliente;
        }

        public void Dispose()
        {

        }
    }
}
