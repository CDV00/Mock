﻿using System.Threading.Tasks;
using Course.BLL.DTO;
using Course.BLL.Requests;
using System;
using Course.BLL.Responses;
using Course.BLL.Share.RequestFeatures;
using Course.DAL.DTOs;
using Entities.Responses;

namespace Course.BLL.Services.Abstraction
{
    public interface IAssignmentService
    {
        //Task<PagedList<AssignmentDTO>> GetAll(AssignmentParameters assignmentParameters);
        Task<ApiBaseResponse> GetAllAssignment(AssignmentParameters parameter);
        //Task<Response<AssignmentDTO>> Add(AssignmentForCreateRequest assignmentForCreateRequest);
    }
}
