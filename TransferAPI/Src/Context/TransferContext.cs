using Microsoft.EntityFrameworkCore;
using TransferAPI.Src.Models;

namespace TransferAPI.Src.Context
{
    /// <summary>
    /// <para>Resumo: Classe contexto, responsavel por carregar contexto e definir DbSets</para>
    /// <para>Criado por: Raul</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 01/11/2022</para>
    /// </summary>
    public class TransferContext : DbContext
    {
        #region Atributos

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Transfer> Transfers { get; set; }

        #endregion

        #region Construtores

        public TransferContext(DbContextOptions<TransferContext> opt) : base(opt)
        {

        }

        #endregion
    }
}
