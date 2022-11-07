using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransferAPI.Src.Context;
using TransferAPI.Src.Models;
namespace TransferAPI.Src.Interfaces.Implements
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por implementar ITransfer</para>
    /// <para>Criado por: Raul</para>
    /// <para>Versão: 1.0</para>
    /// </summary>
    public class TransferImplement : ITransfer
    {
        #region Atributos

        private readonly TransferContext _context;
        private readonly ICustomer _customer;

        #endregion

        #region Construtores

        public TransferImplement(TransferContext context, ICustomer customer)
        {
            _context = context;
            _customer = customer;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// <para>Resumo: Método assíncrono para carregar lista de extrato. </para>
        /// </summary>
        public async Task<List<Transfer>> ExtractListAsync()
        {
            return await _context.Transfers.ToListAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para uma nova transferências </para>
        /// </summary>
        /// <return>CustomerModel</return>
        public async Task NewTransferAsync(Transfer transfer)
        {
            if (!OriginPixExist(transfer.ChavePixOrigem.Pix)) throw new Exception("Chave PIX não cadastrada");

            if (!DestinationPixExist(transfer.ChavePixDestino.Pix)) throw new Exception("Chave PIX não cadastrada");

            if(transfer.ChavePixOrigem.Pix == transfer.ChavePixDestino.Pix) throw new Exception("As chaves PIX não podem ser iguais");

            if (transfer.Valor <= 0) throw new Exception("O valor da transferência deve ser maior que 0");

            await _context.Transfers.AddAsync(
                new Transfer
                {
                    ChavePixOrigem = transfer.ChavePixOrigem,
                    ChavePixDestino = transfer.ChavePixDestino,
                    Valor = transfer.Valor,

                });         
            await UpdateOriginBalanceAsync(transfer.ChavePixOrigem.Pix, transfer.Valor);
            await UpdateDestinationBalanceAsync(transfer.ChavePixDestino.Pix, transfer.Valor);
            await _context.SaveChangesAsync();

            //função auxiliar
            bool OriginPixExist(string pix)
            {
                var aux = _context.Customers.FirstOrDefault(p => p.Pix == transfer.ChavePixOrigem.Pix);
                return aux != null;
            }
            bool DestinationPixExist(string pix)
            {
                var aux = _context.Customers.FirstOrDefault(p => p.Pix == transfer.ChavePixDestino.Pix);
                return aux != null;
            }
        }

        //função auxiliar
        public async Task UpdateOriginBalanceAsync(string pixKey, float valor)
        {
            var pix = _context.Customers.FirstOrDefault(p => p.Pix == pixKey);

            if (pix != null)
            {
                if(pix.Saldo >= valor)
                {
                    pix.Saldo -= valor;
                    _context.Customers.Update(pix);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Saldo indisponível");
                }
            }
        }

        public async Task UpdateDestinationBalanceAsync(string pixKey, float valor)
        {
            var pix = _context.Customers.FirstOrDefault(p => p.Pix == pixKey);

            if (pix != null)
            {
                pix.Saldo += valor;
                _context.Customers.Update(pix);
                await _context.SaveChangesAsync();
            }
        }

        #endregion



    }
}

