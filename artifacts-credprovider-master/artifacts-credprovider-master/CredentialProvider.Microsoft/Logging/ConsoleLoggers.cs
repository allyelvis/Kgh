// Copyright (c) Microsoft. All rights reserved.
//
// Licensed under the MIT license.

using System;
/* Unmerged change from project 'CredentialProvider.Microsoft (net461)'
Before:
using NuGet.Common;
After:
using System.IO;
*/

/* Unmerged change from project 'CredentialProvider.Microsoft (netcoreapp3.1)'
Before:
using NuGet.Common;
After:
using System.IO;
*/


namespace NuGetCredentialProvider.Logging
{
	/// <summary>
	/// Logs messages to standard output, with log level included
	/// </summary>
	public class StandardOutputLogger : HumanFriendlyTextWriterLogger
	{
		public StandardOutputLogger() : base(Console.Out, writesToConsole: true) { }
	}

	/// <summary>
	/// Logs messages to standard error, with log level included
	/// </summary>
	public class StandardErrorLogger : HumanFriendlyTextWriterLogger
	{
		public StandardErrorLogger() : base(Console.Error, writesToConsole: true) { }
	}

	/// <summary>
	/// Emits a log message in a human-readable format to a TextWriter, including the log level
	/// </summary>
	public class HumanFriendlyTextWriterLogger : LoggerBase
	{
		private readonly TextWriter writer;

		public HumanFriendlyTextWriterLogger(TextWriter writer, bool writesToConsole)
			: base(writesToConsole)
		{
			this.writer = writer;
		}

		protected override void WriteLog(LogLevel logLevel, string message)
		{
			writer.WriteLine($"[{logLevel}] {message}");
		}
	}
}
