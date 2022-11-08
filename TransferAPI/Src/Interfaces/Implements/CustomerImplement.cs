using Microsoft.EntityFrameworkCore;
using System;
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
    /// <para>Resumo: Classe responsavel por implementar ICustomer</para>
    /// <para>Criado por: Raul</para>
    /// <para>Versão: 1.0</para>
    /// </summary>
    public class CustomerImplement : ICustomer
    {
        #region Atributos

        private readonly TransferContext _context;

        #endregion

        #region Construtores

        public CustomerImplement(TransferContext context)
        {
            _context = context;
        }

        #endregion

        /// <summary>
        /// <para>Resumo: Método assíncrono para adicionar um novo cliente</para>
        /// </summary>
        /// <param name="customer">Construtor para cadastrar cliente</param>       
        public async Task NewCustomerAsync(Customer customer)
        {
            if (customer.Saldo < 0) throw new Exception("O saldo não pode ser negativo");
            await _context.Customers.AddAsync(
                new Customer
                {
                    Nome = customer.Nome,
                    CPF = customer.CPF,
                    Pix = customer.Pix,
                    Saldo = customer.Saldo,
                });
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para consultar todos clientes</para>
        /// </summary>
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para consultar um cliente pelo
        ///CPF</para>
        /// </summary>
        /// <param name="cpf">CPF do usuario</param>
        /// <return>CustomerModel</return>
        public async Task<Customer> GetCustomerByCPFAsync(string cpf)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.CPF == cpf);
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para consultar um cliente pela Chave
        ///PIX</para>
        /// </summary>
        /// <param name="pix">CPF do usuario</param>
        /// <return>CustomerModel</return>
        public async Task<Customer> GetCustomerByPIXAsync(string pix)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Pix == pix);
        }
    }
}
