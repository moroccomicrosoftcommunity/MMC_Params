﻿using MediatR;
using ParamsService.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamsService.Application.Features.Theme.Commands
{
    public record ThemeCreateCmd(string? Name):IRequest<ThemeGetDto>;
}
