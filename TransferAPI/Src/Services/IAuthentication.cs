using MySql.Data.MySqlClient.Memcached;
using System.Threading.Tasks;
using TransferAPI.Src.Models;

namespace TransferAPI.Src.Services
{
    public interface IAuthentication
    {
        Task NoDuplicateCPFPIX(Cliente cliente);
    }
}
