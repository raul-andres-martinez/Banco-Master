using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransferAPI.Src.Interfaces;
using TransferAPI.Src.Models;
using TransferAPI.Src.Services;

namespace TransferAPI.Src.Controllers
{
    [ApiController]
    [Route("api/Cliente")]
    [Produces("application/json")]
    public class ClienteController : ControllerBase
    {
        #region Atributos

        private readonly ICliente _repo;
        private readonly IAuthentication _services;

        #endregion

        #region Construtores

        public ClienteController(ICliente repo, IAuthentication services)
        {
            _repo = repo;
            _services = services;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Cadastrar cliente
        /// </summary>
        /// <param name="cliente">Contrutor para cadastrar cliente</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        /// POST /api/Cliente/CadastrarCliente
        /// {
        /// "Nome": "Fulano",
        /// "CPF": "123.456.789-10 (Escrever com "." e "-")",
        /// "Pix": "123abc"
        /// }
        ///
        /// </remarks>
        /// <response code="201">Retorna cliente criado</response>
        /// <response code="401">CPF ou PIX já cadastrado</response>

        [HttpPost("CadastrarCliente")]
        public async Task<ActionResult> NewClienteAsync([FromBody] Cliente cliente)
        {
            try
            {
                await _services.NoDuplicateCPFPIX(cliente);

                return Created($"api/Clientes/{cliente.Nome}", cliente);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }

        }


        /// <summary>
        /// Consultar todos clientes
        /// </summary>
        /// <param>Consultar todos clientes</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna todos clientes</response>
        /// <response code="204">Sem clientes cadastrados</response>
        [HttpGet]
        public async Task<ActionResult> GetAllClientesAsync()
        {
            var list = await _repo.GetAllClientesAsync();
            if (list.Count < 1) return NoContent();
            return Ok(list);
        }

        /// <summary>
        /// Consultar cliente pelo CPF
        /// </summary>
        /// <param name="cpf">CPF do cliente</param>
        /// <return>Cliente</return>
        /// <response code="200">Retorna cliente</response>
        /// <response code="204">CPF não encontrado</response>
        [HttpGet("ConsultarCliente/{cpf}")]
        public async Task<ActionResult> GetClienteByCPFAsync([FromRoute] string cpf)
        {
            var cliente = await _repo.GetClienteByCPFAsync(cpf);

            if (cliente == null) return NotFound(new { Message = "CPF não encontrado no sistema" });

            return Ok(cliente);
        }

        #endregion

    }
}
