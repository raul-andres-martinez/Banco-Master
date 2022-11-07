using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using TransferAPI.Src.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using TransferAPI.Src.Models;

namespace TransferAPI.Src.Controllers
{
    [ApiController]
    [Route("api/Transferencia")]
    [Produces("application/json")]
    public class TransferController : ControllerBase
    {
        #region Atributos

        private readonly ITransfer _repo;

        #endregion

        #region Construtores

        public TransferController(ITransfer repo)
        {
            _repo = repo;
        }

        #endregion

        #region Métodos


        /// <summary>
        /// Nova transferência
        /// </summary>
        /// <param name="transfer">Contrutor para nova transferência</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Transferencia/TransferirPix
        ///     {
        ///         "chavePixOrigem": {
        ///             "Nome": "Fulano",
        ///             "CPF": "123.456.789-10", 
        ///             "Pix": "123abc"
        ///         },
        ///         "valor": 60.00,
        ///         "chavePixDestino": {
        ///             "Nome": "Raul",
        ///             "CPF": "987.654.321-10",
        ///             "Pix": "abc123"
        ///         }
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retorna transferência realizada</response>
        /// <response code="400">Erro na requisição</response>
        [HttpPost("TransferirPix")]
        public async Task<ActionResult> NewTransferAsync([FromBody] Transfer transfer)
        {
            try
            {
                await _repo.NewTransferAsync(transfer);
                return Created($"api/Transferencias", transfer);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Carregar histórico de transferências
        /// </summary>
        /// <returns> ActionResult </returns>
        /// <response code="201"> Retorna transferências </response>
        /// <response code="204"> Sem transferências realizadas </response>

        [HttpGet]
        public async Task<ActionResult> ExtractListAsync()
        {
            var list = await _repo.ExtractListAsync();
            if (list.Count < 1) return NoContent();
            return Ok(list);
        }


        #endregion
    }
}
