using Microsoft.EntityFrameworkCore;
using TransferAPI.Src.Models;

namespace TransferAPI.Src.Context
{
    public class TransferContext : DbContext
    {
        #region Atributos

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Transfer> Transfers { get; set; }

        #endregion

        #region Construtores

        public TransferContext(DbContextOptions<TransferContext> opt) : base(opt)
        {

        }

        #endregion
    }
}
