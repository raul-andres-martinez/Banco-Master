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

        [HttpPost]
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


        #endregion
    }
}
