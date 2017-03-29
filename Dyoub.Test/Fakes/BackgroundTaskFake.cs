using System;
using System.Threading;
using Dyoub.App.Infrastructure;

namespace Dyoub.Test.Fakes
{
    public class BackgroundTaskFake : BackgroundTask
    {
        public override void Execute(Action<CancellationToken> action)
        {
            action(new CancellationToken());
        }
    }
}
