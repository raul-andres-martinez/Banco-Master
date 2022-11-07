using MySql.Data.MySqlClient.Memcached;
using System.Threading.Tasks;
using TransferAPI.Src.Models;

namespace TransferAPI.Src.Services
{
    public interface IAuthentication
    {
        /// <summary>
        /// <para>Resumo: Responsavel por representar se um CPF e chave PIX são unicos de um
        /// clientes</para>
        /// <para>Criado por: Raul</para>
        /// <para>Versão: 1.0</para>
        /// <para>Data: 03/11/2022</para>
        /// </summary>
        Task NoDuplicateCPFPIX(Customer customer);
    }
}
