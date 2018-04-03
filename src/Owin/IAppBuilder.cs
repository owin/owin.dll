// Licensed to Monkey Square, Inc. under one or more contributor 
// license agreements.  See the NOTICE file distributed with 
// this work or additional information regarding copyright 
// ownership.  Monkey Square, Inc. licenses this file to you 
// under the Apache License, Version 2.0 (the "License"); you 
// may not use this file except in compliance with the License.
// You may obtain a copy of the License at 
// 
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
// KIND, either express or implied.  See the License for the
// specific language governing permissions and limitations
// under the License.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Owin
{
    /// <summary>
    /// This interface may be passed to web site's Startup code. It enables the
    /// site author to add middleware to an OWIN pipeline, typically ending with
    /// the OWIN adapter for the web framework their site is built on.
    /// </summary>
    public interface IAppBuilder
    {
        /// <summary>
        /// Contains arbitrary properties which may be added, examined, and modified by
        /// components during the startup sequence. 
        /// </summary>
        IDictionary<string, object> Properties { get; }

        /// <summary>
        /// Adds a middleware node to the OWIN function pipeline. The middleware are
        /// invoked in the order they are added: the first middleware passed to Use will
        /// be the outermost function, and the last middleware passed to Use will be the
        /// innermost.
        /// </summary>
        /// <param name="middleware">
        /// The middleware parameter determines which behavior is being chained into the
        /// pipeline. 
        /// 
        /// If the middleware given to Use is a Delegate, then it will be invoked with the "next app" in 
        /// the chain as the first parameter. If the delegate takes more than the single argument, 
        /// then the additional values must be provided to Use in the args array.
        /// 
        /// If the middleware given to Use is a Type, then the public constructor will be 
        /// invoked with the "next app" in the chain as the first parameter. The resulting object
        /// must have a public Invoke method. If the object has constructors which take more than
        /// the single "next app" argument, then additional values may be provided in the args array.
        /// </param>
        /// <param name="args">
        /// Any additional args passed to Use will be passed as additional values, following the "next app"
        /// parameter, when the OWIN call pipeline is build.
        /// 
        /// They are passed as additional parameters if the middleware parameter is a Delegate, or as additional
        /// constructor arguments if the middle parameter is a Type.
        /// </param>
        /// <returns>
        /// The IAppBuilder itself is returned. This enables you to chain your use statements together.
        /// </returns>
        IAppBuilder Use(object middleware, params object[] args);

        /// <summary>
        /// Build is called at the point when all of the middleware should be chained
        /// together. This is typically done by the hosting component which created the app builder,
        /// and does not need to be called by the startup method if the IAppBuilder is passed in.
        /// </summary>
        /// <param name="returnType">
        /// The Type argument indicates which calling convention should be returned, and
        /// is typically typeof(Func&lt;IDictionary&lt;string, object&gt;, Task&gt;) for the OWIN
        /// calling convention.
        /// </param>
        /// <returns>
        /// Returns an instance of the pipeline's entry point. This object may be safely cast to the
        /// type which was provided
        /// </returns>
        object Build(Type returnType);

        /// <summary>
        /// The New method creates a new instance of an IAppBuilder. This is needed to create
        /// a tree structure in your processing, rather than a linear pipeline. The new instance share the
        /// same Properties, but will be created with a new, empty middleware list.
        /// 
        /// To create a tangent pipeline you would first call New, followed by several calls to Use on 
        /// the new builder, ending with a call to Build on the new builder. The return value from Build
        /// will be the entry-point to your tangent pipeline. This entry-point may now be added to the
        /// main pipeline as an argument to a switching middleware, which will either call the tangent
        /// pipeline or the "next app", based on something in the request.
        /// 
        /// That said - all of that work is typically hidden by a middleware like Map, which will do that
        /// for you.
        /// </summary>
        /// <returns>The new instance of the IAppBuilder implementation</returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "New", Justification = "By design")]
        IAppBuilder New();
    }
}