﻿using System;
using System.Collections.Generic;
﻿using System.Threading;
﻿using System.Threading.Tasks;

namespace Owin
{
    public static class OwinConstants
    {
        public const string Version = "owin.Version";
        public const string RequestScheme = "owin.RequestScheme";
        public const string RequestMethod = "owin.RequestMethod";
        public const string RequestPathBase = "owin.RequestPathBase";
        public const string RequestPath = "owin.RequestPath";
        public const string RequestQueryString = "owin.RequestQueryString";
        public const string RequestHeaders = "owin.RequestHeaders";
        public const string RequestBody = "owin.RequestBody";
    }

    public delegate void AppDelegate(
        IDictionary<string, object> env,
        ResultDelegate result,
        Action<Exception> fault);

    public delegate void ResultDelegate(
        string status,
        IDictionary<string, IEnumerable<string>> headers,
        BodyDelegate body);

    public delegate void BodyDelegate(
        Func<ArraySegment<byte>, bool> write,
        Func<Action, bool> flush,
        Action<Exception> end,
        CancellationToken cancellationToken);

    public delegate Task<Tuple<string /* status */, IDictionary<String, IEnumerable<string>> /* headers */, BodyDelegate /* body */>>
        AppTaskDelegate(IDictionary<string, object> env);

    public interface IAppBuilder
    {
        IAppBuilder Use<TApp>(Func<TApp, TApp> middleware);
        TApp Build<TApp>(Action<IAppBuilder> fork);
    }
}
