using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient.Memcached;
using System;
using System.Threading.Tasks;
using TransferAPI.Src.Interfaces;
using TransferAPI.Src.Models;

namespace TransferAPI.Src.Services.Implements
{
    public class AuthenticationServices : IAuthentication
    {
        #region Atributos

        private ICliente _context;

        #endregion

        #region Construtores

        public AuthenticationServices(ICliente context)
        {
            _context = context;
        }

        #endregion

        #region Métodos

        public async Task NoDuplicateCPFPIX(Cliente cliente)
        {
            var aux = await _context.GetClienteByCPFAsync(cliente.CPF);

            if (aux != null) throw new Exception("CPF já existente no sistema");

            var auxP = await _context.GetClienteByPIXAsync(cliente.Pix);

            if (auxP != null) throw new Exception("Chave PIX já existente");

            await _context.NewClienteAsync(cliente);
        }

        #endregion
    }
}
