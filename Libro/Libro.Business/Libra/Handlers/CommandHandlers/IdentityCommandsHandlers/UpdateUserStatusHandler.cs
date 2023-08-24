﻿using Libro.Business.Handlers.CommandHandlers.IdentityCommands;
using Libro.Business.Libra.Commands.IdentityCommands;
using Libro.Business.Managers;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libro.Business.Libra.Handlers.CommandHandlers.IdentityCommandsHandlers
{
    public class UpdateUserStatusHandler : IRequestHandler<UpdateUserStatusCommand, string>
    {
        public IdentityManager _manager;
        public ILogger<UpdateUserStatusHandler> _logger;

        public UpdateUserStatusHandler(ILogger<UpdateUserStatusHandler> logger, IdentityManager manager = null)
        {
            _logger = logger;
            _manager = manager;
        }

        public async Task<string> Handle(UpdateUserStatusCommand request, CancellationToken cancellationToken)
        {
            return await _manager.UpdateUserStatus(request.Id);
        }
    }
}