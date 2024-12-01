// Copyright (c) Microsoft. All rights reserved.
//
// Licensed under the MIT license.

namespace NuGetCredentialProvider.CredentialProviders.Vsts
{
    public interface IVstsSessionTokenClient
    {
        Task<string> CreateSessionTokenAsync(VstsTokenType tokenType, DateTime validTo, CancellationToken cancellationToken);
    }
}