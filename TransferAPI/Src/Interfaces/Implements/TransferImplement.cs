using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Linq;
using System.Threading.Tasks;
using TransferAPI.Src.Context;
using TransferAPI.Src.Models;
namespace TransferAPI.Src.Interfaces.Implements
{
    public class TransferImplement : ITransfer
    {
        #region Atributos

        private readonly TransferContext _context;

        #endregion

        #region Construtores

        public TransferImplement(TransferContext context)
        {
            _context = context;
        }

        public async Task NewTransferAsync(Transfer transfer)
        {
            if (!OriginPixExist(transfer.ChavePixOrigem.Pix)) throw new Exception("Chave PIX não cadastrada");

            if (!DestinationPixExist(transfer.ChavePixDestino.Pix)) throw new Exception("Chave PIX não cadastrada");

            if (transfer.ChavePixOrigem.Pix == transfer.ChavePixDestino.Pix) throw new Exception("As chaves PIX não podem ser iguais!");

            await _context.Transfers.AddAsync(
                new Transfer
                {
                    ChavePixOrigem = _context.Clientes.FirstOrDefault(p => p.Pix == transfer.ChavePixOrigem.Pix),
                    ChavePixDestino = _context.Clientes.FirstOrDefault(p => p.Pix == transfer.ChavePixDestino.Pix),
                    Valor = transfer.Valor,

                });
            await _context.SaveChangesAsync();

            //função auxiliar
            bool OriginPixExist(string pix)
            {
                var aux = _context.Clientes.FirstOrDefault(p => p.Pix == transfer.ChavePixOrigem.Pix);
                return aux != null;
            }
            bool DestinationPixExist(string pix)
            {
                var aux = _context.Clientes.FirstOrDefault(p => p.Pix == transfer.ChavePixDestino.Pix);
                return aux != null;
            }
        }



        #endregion
    }
    }

