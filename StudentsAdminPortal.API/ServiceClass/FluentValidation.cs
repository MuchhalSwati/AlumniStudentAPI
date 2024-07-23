﻿

using FluentValidation;
using StudentsAdminPortal.API.Models;
using System;

namespace StudentsAdminPortal.API.ServiceClass
{
    public class FluentValidation:AbstractValidator<StudentUpdate>
    {
        public FluentValidation()
        {
            try
            {
                
                RuleFor(stud => stud.FirstName).NotEmpty().WithMessage("FirstName is required");
                RuleFor(stud => stud.FirstName).MinimumLength(3).WithMessage("FirstName minimum length is 4 characters");
                RuleFor(stud => stud.DepartmentId).NotEmpty().WithMessage("DepartmentId is required");
                RuleFor(stud => stud.StartDate).NotEmpty().WithMessage("StartDate is required");
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}