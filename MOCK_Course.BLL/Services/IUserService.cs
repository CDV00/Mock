﻿using System.Threading.Tasks;
using Course.BLL.Requests;
using Course.BLL.Responses;

namespace Course.BLL.Services
{
    public interface IUserService 
    {
        public Task<Response<UserResponse>> Register(RegisterRequest registerRequest);
        Task<Response<LoginResponse>> Login(LoginRequest loginRequest);
    }
}
