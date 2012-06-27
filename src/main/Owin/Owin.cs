﻿using System;
using System.Collections.Generic;
﻿using System.IO;
﻿using System.Threading;
﻿using System.Threading.Tasks;

namespace Owin
{
    using AppAction = Action< // Call
        IDictionary<string, object>, // Environment
        IDictionary<string, string[]>, // Headers
        Stream, // Body
        CancellationToken, // CallCancelled
        Task<Tuple< //Result
            IDictionary<string, object>, // Properties
            int, // Status
            IDictionary<string, string[]>, // Headers
            Action< // CopyTo
                Stream, // Body
                CancellationToken, // CopyToCancelled
                Task>>>>; // Done

    public struct CallParameters
    {
        public IDictionary<string, object> Environment;
        public IDictionary<string, string[]> Headers;
        public Stream Body;
    }

    public struct ResultParameters
    {
        public IDictionary<string, object> Properties;
        public int Status;
        public IDictionary<string, string[]> Headers;
        public BodyDelegate Body;
    }

    public delegate Task<ResultParameters> AppDelegate(CallParameters call, CancellationToken cancel);

    public delegate Task BodyDelegate(Stream output, CancellationToken cancel);

    public interface IAppBuilder
    {
        IAppBuilder Use<TApp>(Func<TApp, TApp> middleware);
        TApp Build<TApp>(Action<IAppBuilder> pipeline);

        IAppBuilder AddAdapters<TApp1, TApp2>(Func<TApp1, TApp2> adapter1, Func<TApp2, TApp1> adapter2);
        IDictionary<string, object> Properties { get; }
    }
}
