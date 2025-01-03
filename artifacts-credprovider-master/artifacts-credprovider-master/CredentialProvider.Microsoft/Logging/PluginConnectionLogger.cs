﻿// Copyright (c) Microsoft. All rights reserved.
//
// Licensed under the MIT license.

/* Unmerged change from project 'CredentialProvider.Microsoft (net461)'
Before:
using NuGet.Common;
using NuGet.Protocol.Plugins;
After:
using System.Threading;
using System.Threading.Tasks;
*/

/* Unmerged change from project 'CredentialProvider.Microsoft (netcoreapp3.1)'
Before:
using NuGet.Common;
using NuGet.Protocol.Plugins;
After:
using System.Threading;
using System.Threading.Tasks;

/* Unmerged change from project 'CredentialProvider.Microsoft (net461)'
Before:
*/


namespace NuGetCredentialProvider.Logging
After:
*/


namespace NuGetCredentialProvider.Logging
*/

/* Unmerged change from project 'CredentialProvider.Microsoft (netcoreapp3.1)'
Before:
*/


namespace NuGetCredentialProvider.Logging
After:
*/


namespace NuGetCredentialProvider.Logging
*/
*/

namespace NuGetCredentialProvider.Logging
{
    internal class PluginConnectionLogger : LoggerBase
    {
        private readonly IConnection connection;

        internal PluginConnectionLogger(IConnection connection)
            : base(writesToConsole: true)
        {
            this.connection = connection;
        }

        protected override void WriteLog(LogLevel logLevel, string message)
        {
            // intentionally not awaiting here -- don't want to block forward progress just because we tried to log.
            connection.SendRequestAndReceiveResponseAsync<LogRequest, LogResponse>(
                    MessageMethod.Log,
                    new LogRequest(logLevel, $"    {message}"),
                    CancellationToken.None)
                // "observe" any exceptions to avoid unobserved exception escalation, which may terminate the process
                .ContinueWith(x => x.Exception, TaskContinuationOptions.OnlyOnFaulted);
        }
    }
}
