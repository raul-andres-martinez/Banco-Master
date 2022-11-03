using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TransferAPI.Src.Models
{
    [Table("TB_TRANSFER")]
    public class Transfer
    {
        #region Atributos

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("FK_ORIGEMPIX")]
        public Cliente ChavePixOrigem { get; set; }

        [Required]
        public float Valor { get; set; }

        [Required]
        [ForeignKey("FK_DESTINOPIX")]
        public Cliente ChavePixDestino { get; set; }

        #endregion

    }
}
