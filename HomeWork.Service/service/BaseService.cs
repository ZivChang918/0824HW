using HomeWork.Model.Models;
using HomeWork.Service.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace HomeWork.Service.service
{
    public class BaseService : IBaseService
    {
        public readonly FAQDBContext _context;

        public BaseService(FAQDBContext context)
        {
            _context = context;
        }

        public void Dispose() { GC.SuppressFinalize(true); }
    }
}
