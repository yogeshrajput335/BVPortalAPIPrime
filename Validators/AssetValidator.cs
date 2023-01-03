using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BVPortalApi.DTO;
using BVPortalApi.Models;
using FluentValidation;

namespace BVPortalAPIPrime.Validators
{
    public class AssetValidator : AbstractValidator<AssetDTO>
    {
        public AssetValidator()
        {
            RuleFor(model => model.Name).NotEmpty();
            RuleFor(model => model.ModelNumber).NotEmpty();
        }
    }
}