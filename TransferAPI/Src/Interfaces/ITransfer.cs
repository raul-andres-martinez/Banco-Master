using System.Collections.Generic;
using System.Threading.Tasks;
using TransferAPI.Src.Models;

namespace TransferAPI.Src.Interfaces
{
    /// <summary>
    /// <para>Resumo: Responsavel por representar ação de criar uma transfêrencia pix entre
    /// clientes</para>
    /// <para>Criado por: Raul</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 01/11/2022</para>
    /// </summary>
    public interface ITransfer
    {
        Task NewTransferAsync(Transfer transfer);
        Task<List<Transfer>> ExtractListAsync();
    }
}
