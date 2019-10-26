﻿using System.Threading;
using System.Threading.Tasks;
using otor.msixhero.lib.BusinessLayer.Infrastructure;
using otor.msixhero.lib.BusinessLayer.State;

namespace otor.msixhero.lib.BusinessLayer.Reducers
{
    public abstract class BaseReducer<T> : IReducer<T> where T : IApplicationState
    {
        public abstract Task ReduceAsync(IApplicationStateManager<T> state, CancellationToken cancellationToken);
    }
}
