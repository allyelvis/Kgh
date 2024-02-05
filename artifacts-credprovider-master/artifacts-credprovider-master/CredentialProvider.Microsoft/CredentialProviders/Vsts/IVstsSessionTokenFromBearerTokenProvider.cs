// Copyright (c) Microsoft. All rights reserved.
//
// Licensed under the MIT license.
using System.Threading;
/* Unmerged change from project 'CredentialProvider.Microsoft (net461)'
Before:
using NuGet.Protocol.Plugins;
After:
using System.Threading.Tasks;
*/

/* Unmerged change from project 'CredentialProvider.Microsoft (netcoreapp3.1)'
Before:
using NuGet.Protocol.Plugins;
After:
using System.Threading.Tasks;
*/


namespace NuGetCredentialProvider.CredentialProviders.Vsts
{
	public interface IAzureDevOpsSessionTokenFromBearerTokenProvider
	{
		Task<string> GetAzureDevOpsSessionTokenFromBearerToken(
			GetAuthenticationCredentialsRequest request,
			string bearerToken,
			bool bearerTokenObtainedInteractively,
			CancellationToken cancellationToken);
	}
}