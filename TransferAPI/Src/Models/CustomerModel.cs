using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransferAPI.Src.Models
{
    /// <summary>
    /// <para>Resumo: Classe responsavel pela tabela Clientes no banco de dados, adotei como documento padrão o CPF, por ser único para cada cidadão brasileiro.</para>
    /// <para>Criado por: Raul</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 01/11/2022</para>
    /// </summary>

    [Table("TB_CLIENTES")]
    public class Customer
    {
        #region Atributos

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [RegularExpression("^\\d{3}\\.\\d{3}\\.\\d{3}-\\d{2}$")]
        public string CPF { get; set; }

        [Required]
        [JsonIgnore]
        [InverseProperty("ChavePixOrigem")]
        public string Pix { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public float Saldo { get; set; }

        #endregion Atributos
    }
}
