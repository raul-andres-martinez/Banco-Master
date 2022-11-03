using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TransferAPI.Src.Context;
using TransferAPI.Src.Interfaces;
using TransferAPI.Src.Models;

namespace TransferAPI.Src.Implements
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por implementar ICliente</para>
    /// <para>Criado por: Raul</para>
    /// <para>Versão: 1.0</para>
    /// </summary>
    public class ClienteImplement : ICliente
    {
        #region Atributos

        private readonly TransferContext _context;

        #endregion

        #region Construtores

        public ClienteImplement(TransferContext context)
        {
            _context = context;
        }

        #endregion

        /// <summary>
        /// <para>Resumo: Método assíncrono para adicionar um novo cliente</para>
        /// </summary>
        /// <param name="cliente">Construtor para cadastrar cliente</param>       
        public async Task NewClienteAsync(Cliente cliente)
        {
            await _context.Clientes.AddAsync(
                new Cliente
                {
                    Nome = cliente.Nome,
                    CPF = cliente.CPF,
                    Pix = cliente.Pix,
                });
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para consultar todos clientes</para>
        /// </summary>
        public async Task<List<Cliente>> GetAllClientesAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para consultar um cliente pelo
        ///CPF</para>
        /// </summary>
        /// <param name="cpf">CPF do usuario</param>
        /// <return>UserModel</return>
        public async Task<Cliente> GetClienteByCPFAsync(string cpf)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.CPF == cpf);
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para consultar um cliente pela Chave
        ///PIX</para>
        /// </summary>
        /// <param name="pix">CPF do usuario</param>
        /// <return>UserModel</return>
        public async Task<Cliente> GetClienteByPIXAsync(string pix)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Pix == pix);
        }
    }
}
