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
	/// <summary>
	/// Handles a <see cref="SetCredentialsRequest"/>
	/// </summary>
	internal class SetCredentialsRequestHandler : RequestHandlerBase<SetCredentialsRequest, SetCredentialsResponse>
	{
		private static readonly SetCredentialsResponse SuccessResponse = new SetCredentialsResponse(MessageResponseCode.Success);

		public SetCredentialsRequestHandler(ILogger logger)
			: base(logger)
		{
		}

		public override Task<SetCredentialsResponse> HandleRequestAsync(SetCredentialsRequest request)
		{
			// There's currently no way to handle proxies, so nothing we can do here
			return Task.FromResult(SuccessResponse);
		}
	}
}