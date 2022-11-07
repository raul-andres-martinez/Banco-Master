using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient.Memcached;
using System;
using System.Threading.Tasks;
using TransferAPI.Src.Interfaces;
using TransferAPI.Src.Models;

namespace TransferAPI.Src.Services.Implements
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por implementar a autenticação de um CPF ou PIX</para>
    /// <para>Criado por: Raul</para>
    /// </summary>

    public class AuthenticationServices : IAuthentication
    {
        #region Atributos

        private ICustomer _context;

        #endregion

        #region Construtores

        public AuthenticationServices(ICustomer context)
        {
            _context = context;
        }

        #endregion

        #region Métodos

        public async Task NoDuplicateCPFPIX(Customer customer)
        {
            var aux = await _context.GetCustomerByCPFAsync(customer.CPF);

            if (aux != null) throw new Exception("CPF já existente no sistema");

            var auxP = await _context.GetCustomerByPIXAsync(customer.Pix);

            if (auxP != null) throw new Exception("Chave PIX já existente");

            await _context.NewCustomerAsync(customer);
        }
        #endregion
    }
}
