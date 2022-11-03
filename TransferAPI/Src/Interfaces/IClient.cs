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
    public interface ICliente
    {
        Task NewClienteAsync(Cliente cliente);
        Task<List<Cliente>> GetAllClientesAsync();
        Task<Cliente> GetClienteByCPFAsync(string cpf);

        Task<Cliente> GetClienteByPIXAsync(string pix);
    }
}

