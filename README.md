# What is it?
OWIN defines a standard interface between .NET web servers and web applications. The goal of the OWIN interface is to decouple server and application, encourage the development of simple modules for .NET web development, and, by being an open standard, stimulate the open source ecosystem of .NET web development tools.

## Read the specification
[owin.org](http://owin.org/)

## Note on owin NuGet Package

The OWIN community [voted to sunset the `owin.dll`](https://groups.google.com/forum/#!topic/net-http-abstractions/YDbMZqGFVHA) as it contains only the `IAppBuilder` interface which is not specified in any form in the current OWIN specification. We recommend you not use the [owin NuGet package](https://www.nuget.org/packages/owin) for any new OWIN related libraries. If you currently use a project that relies on the owin package, you do not need to do anything. The package will remain available in its current state for the foreseeable future.

