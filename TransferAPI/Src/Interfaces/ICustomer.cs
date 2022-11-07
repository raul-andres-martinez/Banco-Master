using MySql.Data.MySqlClient.Memcached;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransferAPI.Src.Models;

namespace TransferAPI.Src.Interfaces
{
    /// <summary>
    /// <para>Resumo: Responsavel por representar ações de Criar e Consultar
    /// clientes</para>
    /// <para>Criado por: Raul</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 01/11/2022</para>
    /// </summary>
    public interface ICustomer
    {
        Task NewCustomerAsync(Customer customer);
        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByCPFAsync(string cpf);
        Task<Customer> GetCustomerByPIXAsync(string pix);
    }
}

