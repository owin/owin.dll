﻿using System;
using System.Collections.Generic;
﻿using System.Threading;
﻿using System.Threading.Tasks;

namespace Owin
{
    public delegate void AppDelegate(
        IDictionary<string, object> env,
        ResultDelegate result,
        Action<Exception> fault);

    public delegate void ResultDelegate(
        string status,
        IDictionary<string, string[]> headers,
        BodyDelegate body);

    public delegate void BodyDelegate(
        Func<ArraySegment<byte>, Action, bool> write,
        Action<Exception> end,
        CancellationToken cancellationToken);

    public delegate Task<Tuple<string /* status */, IDictionary<String, string[]> /* headers */, BodyDelegate /* body */>>
        AppTaskDelegate(IDictionary<string, object> env);

    public interface IAppBuilder
    {
        IAppBuilder Use<TApp>(Func<TApp, TApp> middleware);
        TApp Build<TApp>(Action<IAppBuilder> fork);

        IAppBuilder AddAdapters<TApp1, TApp2>(Func<TApp1, TApp2> adapter1, Func<TApp2, TApp1> adapter2);
        IDictionary<string, object> Context { get; }
    }
}
