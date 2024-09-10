using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using StudentsAdminPortal.API.Models;
using System;
using System.Text.Json;

namespace StudentsAdminPortal.API.FluentValidation
{
    public class FluentValidation : CustomValidator<StudentUpdate>
    {
        public FluentValidation()
        {

            RuleFor(stud => stud.FirstName).NotEmpty().WithMessage("FirstName is required");
            RuleFor(stud => stud.FirstName).MinimumLength(3).WithMessage("FirstName minimum length is 4 characters");
            RuleFor(stud => stud.DepartmentId).NotEmpty().WithMessage("DepartmentId is required");
            RuleFor(stud => stud.StartDate).NotEmpty().WithMessage("StartDate is required");
        }
    }
}
