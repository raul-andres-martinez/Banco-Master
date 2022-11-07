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
    public class CustomerController : ControllerBase
    {
        #region Atributos

        private readonly ICustomer _repo;
        private readonly IAuthentication _services;

        #endregion

        #region Construtores

        public CustomerController(ICustomer repo, IAuthentication services)
        {
            _repo = repo;
            _services = services;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Cadastrar cliente
        /// </summary>
        /// <param name="customer">Contrutor para cadastrar cliente. Para testar todas funções, criar 2 clientes</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Cliente/CadastrarCliente
        ///     {
        ///         "Nome": "Fulano",
        ///         "CPF": "123.456.789-10"  (Escrever com "." e "-"),
        ///         "Pix": "123abc",
        ///         "Saldo": 120.00
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retorna cliente criado</response>
        /// <response code="401">CPF ou PIX já cadastrado</response>

        [HttpPost("CadastrarCliente")]
        public async Task<ActionResult> NewCustomerAsync([FromBody] Customer customer)
        {
            try
            {
                await _services.NoDuplicateCPFPIX(customer);

                return Created($"api/Clientes/{customer.CPF}", customer);
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
        public async Task<ActionResult> GetAllCustomersAsync()
        {
            var list = await _repo.GetAllCustomersAsync();
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
        [HttpGet("ConsultarCliente/CPF/{cpf}")]
        public async Task<ActionResult> GetCustomerByCPFAsync([FromRoute] string cpf)
        {
            var customer = await _repo.GetCustomerByCPFAsync(cpf);

            if (customer == null) return NotFound(new { Message = "CPF não encontrado no sistema" });

            return Ok(customer);
        }

        /// <summary>
        /// Consultar cliente pela chave PIX
        /// </summary>
        /// <param name="pix">PIX do cliente</param>
        /// <return>Cliente</return>
        /// <response code="200">Retorna cliente</response>
        /// <response code="204">PIX não encontrado</response>
        [HttpGet("ConsultarCliente/PIX/{pix}")]
        public async Task<ActionResult> GetCustomerByPIXAsync([FromRoute] string pix)
        {
            var customer = await _repo.GetCustomerByPIXAsync(pix);

            if (customer == null) return NotFound(new { Message = "PIX não encontrado no sistema" });

            return Ok(customer);
        }

        #endregion

    }
}
