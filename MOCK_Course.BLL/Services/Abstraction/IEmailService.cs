﻿using Course.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.BLL.Services.Abstraction
{
    public interface IEmailService
    {
        Task<BaseResponse> SendEmailAsync(string fromAddress, string toAddress, string subjects, string message);
    }
}
