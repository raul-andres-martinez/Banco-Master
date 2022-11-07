using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TransferAPI.Src.Models
{
    [Table("TB_TRANSFER")]
    public class Transfer
    {
        /// <summary>
        /// <para>Resumo: Classe responsavel pela tabela de Transferência no banco de dados.</para>
        /// <para>Criado por: Raul</para>
        /// <para>Versão: 1.0</para>
        /// <para>Data: 02/11/2022</para>
        /// </summary>
        #region Atributos

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("ChavePixOrigemId")]
        public Customer ChavePixOrigem { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public float Valor { get; set; }

        [Required]
        public Customer ChavePixDestino { get; set; }

        #endregion

    }
}
