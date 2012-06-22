﻿using System;
using System.Collections.Generic;
﻿using System.Threading;
﻿using System.Threading.Tasks;

namespace Owin
{
    public struct CallParameters
    {
        public IDictionary<string, object> Environment;
        public IDictionary<string, string[]> Headers;
        public BodyDelegate Body;
        public CancellationToken CallDisposed;
    }

    public struct ResultParameters
    {
        public int Status;
        public IDictionary<string, string[]> Headers;
        public BodyDelegate Body;
        public IDictionary<string, object> Properties;
    }

    public delegate void AppDelegate(CallParameters call, Action<ResultParameters, Exception> callback);

    public delegate Task<ResultParameters> AppTaskDelegate(CallParameters call);

    public delegate void BodyDelegate(
        Func<ArraySegment<byte>, Action<Exception>, bool> write,
        Action<Exception> end,
        CancellationToken cancel);


    public interface IAppBuilder
    {
        IAppBuilder Use<TApp>(Func<TApp, TApp> middleware);
        TApp Build<TApp>(Action<IAppBuilder> pipeline);

        IAppBuilder AddAdapters<TApp1, TApp2>(Func<TApp1, TApp2> adapter1, Func<TApp2, TApp1> adapter2);
        IDictionary<string, object> Properties { get; }
    }
}
