// Copyright (c) Microsoft. All rights reserved.
//
// Licensed under the MIT license.


/* Unmerged change from project 'CredentialProvider.Microsoft (net461)'
Before:
using System.Threading.Tasks;
using NuGet.Protocol.Plugins;
using NuGetCredentialProvider.Logging;
After:
using NuGet.Protocol.Plugins;
using NuGetCredentialProvider.Logging;
using System.Threading.Tasks;
*/

/* Unmerged change from project 'CredentialProvider.Microsoft (netcoreapp3.1)'
Before:
using System.Threading.Tasks;
using NuGet.Protocol.Plugins;
using NuGetCredentialProvider.Logging;
After:
using NuGet.Protocol.Plugins;
using NuGetCredentialProvider.Logging;
using System.Threading.Tasks;
*/
using NuGetCredentialProvider.Logging;
using System.Threading.Tasks;

namespace NuGetCredentialProvider.RequestHandlers
{
	internal class SetLogLevelRequestHandler : RequestHandlerBase<SetLogLevelRequest, SetLogLevelResponse>
	{
		private static readonly SetLogLevelResponse SuccessResponse = new SetLogLevelResponse(MessageResponseCode.Success);

		public SetLogLevelRequestHandler(ILogger logger)
			: base(logger)
		{
		}

		public override Task<SetLogLevelResponse> HandleRequestAsync(SetLogLevelRequest request)
		{
			Logger.SetLogLevel(request.LogLevel);
			return Task.FromResult(SuccessResponse);
		}
	}
}
