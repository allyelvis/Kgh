﻿// Copyright (c) Microsoft. All rights reserved.
//
// Licensed under the MIT license.

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

namespace NuGetCredentialProvider.CredentialProviders
{
    /// <summary>
    /// Represents an interface for implementation of credential providers.
    /// </summary>
    internal interface ICredentialProvider : IDisposable
    {
        /// <summary>
        /// Determines whether this implementation can provide credentials for the specified <see cref="Uri"/>.
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> of the package feed.</param>
        /// <returns><code>true</code> if this implementation can provide credentials, otherwise <code>false</code>.</returns>
        Task<bool> CanProvideCredentialsAsync(Uri uri);

        /// <summary>
        /// Handles a <see cref="GetAuthenticationCredentialsRequest"/>.
        /// </summary>
        /// <param name="request">A <see cref="GetAuthenticationCredentialsRequest"/> object containing details about the request.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> used for signaling cancellation.</param>
        /// <returns>A <see cref="GetAuthenticationCredentialsResponse"/> object containg details about a response.</returns>
        Task<GetAuthenticationCredentialsResponse> HandleRequestAsync(GetAuthenticationCredentialsRequest request, CancellationToken cancellationToken);

        bool IsCachable { get; }
    }
}