﻿using System;
using System.Threading;
using System.Threading.Tasks;
using otor.msixhero.lib.BusinessLayer.Commands;
using otor.msixhero.lib.BusinessLayer.Infrastructure;
using otor.msixhero.lib.BusinessLayer.State;
using otor.msixhero.lib.Domain;
using otor.msixhero.ui.Services;

namespace otor.msixhero.lib.BusinessLayer.Reducers
{
    public abstract class SelfElevationReducer<T> : BaseReducer<T> where T : IApplicationState
    {
        protected SelfElevationReducer(BaseCommand command, IApplicationStateManager<T> stateManager) : base(command, stateManager)
        {
        }
    }

    public abstract class SelfElevationReducer<T, TOutput> : SelfElevationReducer<T>, IReducer<T, TOutput> where T : IApplicationState
    {
        protected SelfElevationReducer(BaseCommand<TOutput> command, IApplicationStateManager<T> stateManager) : base(command, stateManager)
        {
        }

        public abstract Task<TOutput> GetReduced(IInteractionService interactionService, CancellationToken cancellationToken = default);

        public override Task Reduce(IInteractionService interactionService, CancellationToken cancellationToken = default)
        {
            return this.GetReduced(interactionService, cancellationToken);
        }
    }
}