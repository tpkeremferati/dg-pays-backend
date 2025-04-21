using AutoMapper;
using Dg.Pays.Application.Contracts.DTOs;
using Dg.Pays.Domain.Entities;

namespace Dg.Pays.Application.Mappers
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionDto>();
        }
    }
}
