using System.Threading.Tasks;
using TransferAPI.Src.Models;

namespace TransferAPI.Src.Interfaces
{
    public interface ITransfer
    {
        Task NewTransferAsync(Transfer transfer);
    }
}
