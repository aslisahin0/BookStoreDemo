using AutoMapper;
using BookStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Services
{
    public class Service : IDisposable
    {
        public IUnitOfWork _unitOfWork;
        
        public Service(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Dispose()
        {
            if (_unitOfWork != null)
            {
                _unitOfWork.Dispose();
            }
        }
    }

}
