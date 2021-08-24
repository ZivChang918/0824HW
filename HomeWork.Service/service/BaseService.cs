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
        public FAQDBContext db = new FAQDBContext();

        public void Dispose() { GC.SuppressFinalize(true); }
    }
}
