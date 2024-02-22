// Copyright (c) Microsoft. All rights reserved.
//
// Licensed under the MIT license.

using Microsoft.Artifacts.Authentication;
using NuGetCredentialProvider.Util;
using System.Collections.Generic;
/* Unmerged change from project 'CredentialProvider.Microsoft (net461)'
Before:
using System;
using System.Collections.Generic;
After:
using System.Collections.Generic;
using System.Threading.Tasks;
*/

/* Unmerged change from project 'CredentialProvider.Microsoft (netcoreapp3.1)'
Before:
using System;
using System.Collections.Generic;
After:
using System.Collections.Generic;
using System.Threading.Tasks;
*/


namespace NuGetCredentialProvider.CredentialProviders.Vsts
{
	internal class MsalTokenProvidersFactory : ITokenProvidersFactory
	{
		private readonly ILogger logger;
		private MsalCacheHelper cache;

		public MsalTokenProvidersFactory(ILogger logger)
		{
			this.logger = logger;
		}

		public async Task<IEnumerable<ITokenProvider>> GetAsync(Uri authority)
		{
			if (cache == null && EnvUtil.MsalFileCacheEnabled())
			{
				cache = await MsalCache.GetMsalCacheHelperAsync(EnvUtil.GetMsalCacheLocation(), logger);
			}

			var app = AzureArtifacts.CreateDefaultBuilder(authority)
				.WithBroker(EnvUtil.MsalAllowBrokerEnabled(), logger)
				.WithHttpClientFactory(HttpClientFactory.Default)
				.WithLogging(
					(Microsoft.Identity.Client.LogLevel level, string message, bool containsPii) =>
					{
						// We ignore containsPii param because we are passing in enablePiiLogging below.
						logger.LogTrace("MSAL Log ({level}): {message}", level, message);
					},
					enablePiiLogging: EnvUtil.GetLogPIIEnabled()
				)
				.Build();

			cache?.RegisterCache(app.UserTokenCache);

			return MsalTokenProviders.Get(app, logger);
		}
	}
}
