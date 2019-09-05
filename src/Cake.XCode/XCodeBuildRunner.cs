using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Utilities;

namespace Cake.XCode
{
    /// <summary>
    /// XCode sdk info.
    /// </summary>
    public class XCodeSdk
    {
        /// <summary>
        /// Gets or sets the display name of the SDK.
        /// </summary>
        /// <value>The display name.</value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the SDK value to use in XCodeBuild.
        /// </summary>
        /// <value>The sdk value.</value>
        public string SdkValue { get; set; }
    }

    /// <summary>
    /// XCode settings.
    /// </summary>
    public class XCodeSettings
    {
        /// <summary>
        /// Gets or sets the xcodebuild path.
        /// </summary>
        /// <value>The tool path.</value>
        public FilePath ToolPath { get; set; }
    }

    /// <summary>
    /// XCode build settings.
    /// </summary>
    public class XCodeBuildSettings : XCodeSettings
    {
        /// <summary>
        /// Provide additional status output
        /// </summary>
        public bool Verbose { get; set; }

        /// <summary>
        /// Check if any First Launch tasks need to be performed
        /// </summary>
        public bool CheckFirstLaunchStatus { get; set; }

        /// <summary>
        /// Builds the given Project NAME
        /// </summary>
        /// <value>The name of the project.</value>
        public string Project { get; set; }

        /// <summary>
        /// Builds the given Target NAME
        /// </summary>
        /// <value>The name of the target.</value>
        public string Target { get; set; }

        /// <summary>
        /// Builds all the targets
        /// </summary>
        public bool AllTargets { get; set; }

        /// <summary>
        /// Builds the given Workspace
        /// </summary>
        /// <value>The  workspace.</value>
        public FilePath Workspace { get; set; }

        /// <summary>
        /// Builds the given Scheme NAME
        /// </summary>
        /// <value>The name of the scheme.</value>
        public string Scheme { get; set; }

        /// <summary>
        /// Use the build configuration NAME for building each target
        /// </summary>
        /// <value>The name of the configuration.</value>
        public string Configuration { get; set; }

        /// <summary>
        /// Apply the build settings defined in the file as overrides
        /// </summary>
        /// <value>The build settings file.</value>
        public FilePath XcConfig { get; set; }

        /// <summary>
        /// Build each target for the given architecture; this will override architectures defined in the project
        /// </summary>
        /// <value>The architecture to build.</value>
        public string Arch { get; set; }

        /// <summary>
        /// The SDK to use as the base SDK when building the project
        /// </summary>
        /// <value>The value of the sdk.</value>
        public string Sdk { get; set; }

        /// <summary>
        /// Use the given toolchain
        /// </summary>
        /// <value>The name or identifier of the toolchain.</value>
        public string Toolchain { get; set; }

        /// <summary>
        /// Use the destination described by key value pairs
        /// </summary>
        /// <value>The destination key value pairs.</value>
        public Dictionary<string, string> Destination { get; set; }

        /// <summary>
        /// The timeout in seconds to wait for while searching for the destination device
        /// </summary>
        /// <value>The timeout in seconds.</value>
        public int? DestinationTimeout { get; set; }

        /// <summary>
        /// Build independent targets in parallel
        /// </summary>
        public bool ParallelizeTargets { get; set; }

        /// <summary>
        /// Specify the maximum number of concurrent build operations
        /// </summary>
        /// <value>The maximum number of concurrent jobs.</value>
        public int? Jobs { get; set; }

        /// <summary>
        /// Do everything except actually running the commands
        /// </summary>
        public bool DryRun { get; set; }

        /// <summary>
        /// Specifies the directory where a result bundle describing what occurred will be placed
        /// </summary>
        /// <value>The result bundle path.</value>
        public DirectoryPath ResultBundlePath { get; set; }

        /// <summary>
        /// Specifies the directory where build products and other derived data will go
        /// </summary>
        /// <value>The derived data path.</value>
        public DirectoryPath DerivedDataPath { get; set; }

        /// <summary>
        /// Specifies the directory where any created archives will be placed, or the archive that should be exported
        /// </summary>
        /// <value>The archive path.</value>
        public DirectoryPath ArchivePath { get; set; }

        /// <summary>
        /// Specifies that an archive should be exported
        /// </summary>
        public bool ExportArchive { get; set; }

        /// <summary>
        /// Specifies the format that the archive should be exported as (e.g. ipa, pkg, app)
        /// </summary>
        /// <value>The export format.</value>
        public ExportFormatType? ExportFormat { get; set; }

        /// <summary>
        /// Specifies the destination for the product exported from an archive
        /// </summary>
        /// <value>The export path.</value>
        public DirectoryPath ExportPath { get; set; }

        /// <summary>
        /// Specifies the location of the plist file used as options in the export process.
        /// </summary>
        /// <value>The path to plist.</value>
        public DirectoryPath ExportOptionsPlist { get; set; }

        /// <summary>
        /// Specifies the provisioning profile that should be used when re-signing the exported archive; if possible, implies a signing identity
        /// </summary>
        /// <value>The export provisioning profile name.</value>
        public string ExportProvisioningProfile { get; set; }

        /// <summary>
        /// Specifies the codesigning identity that should be used to re-sign the exported archive
        /// </summary>
        /// <value>The export signing identity name.</value>
        public string ExportSigningIdentity { get; set; }

        /// <summary>
        /// specifies the installer signing identity that should be used during export; this may be inferable from the ExportSigningIdentity
        /// </summary>
        /// <value>The export installer identity.</value>
        public string ExportInstallerIdentity { get; set; }

        /// <summary>
        /// Specifies that the signing identity used to create the archive should be used to sign the exported product
        /// original signing identity.
        /// </summary>
        public bool ExportWithOriginalSigningIdentity { get; set; }

        /// <summary>
        /// Specifies that scheme actions that cannot be performed should be skipped instead of causing a failure
        /// </summary>
        public bool SkipUnavailableActions { get; set; }

        /// <summary>
        /// Exports completed and outstanding project localizations
        /// </summary>
        public bool ExportLocalizations { get; set; }

        /// <summary>
        /// Specifies a path to XLIFF localization files
        /// </summary>
        /// <value>The localization path.</value>
        public DirectoryPath LocalizationPath { get; set; }

        /// <summary>
        /// Specifies optional ISO 639-1 target languages included in a localization export 
        /// </summary>
        /// <value>The export language.</value>
        public string ExportLanguage { get; set; }

		/// <summary>
		/// Overrides specified build settings or adds User-Defined build settings
		/// </summary>
		/// <value>The build settings key value pairs.</value>
		public Dictionary<string, string> BuildSettings { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether project should be cleaned.
        /// </summary>
        /// <value><c>true</c> if project should be cleaned; otherwise, <c>false</c>.</value>
        public bool Clean { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether project should be archived instead of builded.
        /// </summary>
        /// <value><c>true</c> if project should be archive; otherwise, <c>false</c>.</value>
        public bool Archive { get; set; }

        /// <summary>
        /// Export format type.
        /// </summary>
        public enum ExportFormatType
        {
            /// <summary>
            /// A .app package
            /// </summary>
            App,
            /// <summary>
            /// A .pkg installer
            /// </summary>
            Pkg,
            /// <summary>
            /// An iPhone/iPad .ipa
            /// </summary>
            Ipa
        }
    }

    internal class XCodeBuildRunner : Tool<XCodeSettings>
    {
        readonly ICakeEnvironment _cakeEnvironment;

        public XCodeBuildRunner (IFileSystem fileSystem, ICakeEnvironment cakeEnvironment, IProcessRunner processRunner, IGlobber globber)
            : base (fileSystem, cakeEnvironment, processRunner, globber)
        {
            _cakeEnvironment = cakeEnvironment;
        }

        protected override string GetToolName ()
        {
            return "xcodebuild";
        }

        protected override IEnumerable<string> GetToolExecutableNames ()
        {
            return new [] { "xcodebuild" };
        }

        protected override IEnumerable<FilePath> GetAlternativeToolPaths (XCodeSettings settings)
        {
            return new [] { 
                new FilePath ("/usr/bin/xcodebuild"),
                new FilePath ("/usr/local/bin/xcodebuild"), 
                new FilePath ("/Applications/Xcode.app/Contents/Developer/usr/bin/xcodebuild")
            };
        }

        string GetQuotedAbsolute (DirectoryPath path)
        {
            return path.MakeAbsolute (_cakeEnvironment).FullPath.Quote ();
        }

        string GetQuotedAbsolute (FilePath path)
        {
            return path.MakeAbsolute (_cakeEnvironment).FullPath.Quote ();
        }

        public void Build (XCodeBuildSettings settings)
        {
            var builder = new ProcessArgumentBuilder ();

            if (settings.Verbose)
                builder.Append ("-verbose");

            if (settings.CheckFirstLaunchStatus)
                builder.Append ("-checkFirstLaunchStatus");

            if (!string.IsNullOrEmpty (settings.Project))
                builder.Append (string.Format ("-project \"{0}\"", settings.Project));

            if (!string.IsNullOrEmpty (settings.Target))
                builder.Append (string.Format ("-target \"{0}\"", settings.Target));

            if (settings.AllTargets)
                builder.Append ("-alltargets");
            
            if (settings.Workspace != null)
                builder.Append ("-workspace " + GetQuotedAbsolute (settings.Workspace));

            if (!string.IsNullOrEmpty (settings.Scheme))
                builder.Append (string.Format ("-scheme \"{0}\"", settings.Scheme));

            if (!string.IsNullOrEmpty (settings.Configuration))
                builder.Append (string.Format ("-configuration \"{0}\"", settings.Configuration));
                           
            if (settings.XcConfig != null)
                builder.Append ("-xcconfig " + GetQuotedAbsolute (settings.XcConfig));

            if (!string.IsNullOrEmpty (settings.Arch))
                builder.Append (string.Format ("-arch \"{0}\"", settings.Arch));

            if (!string.IsNullOrEmpty (settings.Sdk))
                builder.Append (string.Format ("-sdk \"{0}\"", settings.Sdk));
            
            if (!string.IsNullOrEmpty (settings.Toolchain))
                builder.Append (string.Format ("-toolchain \"{0}\"", settings.Toolchain));

            if (settings.Destination != null && settings.Destination.Count > 0) {
                builder.Append ("-destination " +
                string.Join (",", settings.Destination.Select (kvp => string.Format ("{0}={1}", kvp.Key, kvp.Value))));
             
                if (settings.DestinationTimeout.HasValue)
                    builder.Append ("-destination-timeout " + settings.DestinationTimeout.Value);
            }

            if (settings.ParallelizeTargets)
                builder.Append ("-parallelizeTargets");

            if (settings.Jobs.HasValue)
                builder.Append ("-jobs " + settings.Jobs.Value);

            if (settings.DryRun)
                builder.Append ("-dry-run");

            if (settings.ResultBundlePath != null)
                builder.Append ("-resultBundlePath " + GetQuotedAbsolute (settings.ResultBundlePath));
            
            if (settings.DerivedDataPath != null)
                builder.Append ("-derivedDataPath " + GetQuotedAbsolute (settings.DerivedDataPath));

            if (settings.ArchivePath != null)
                builder.Append ("-archivePath " + GetQuotedAbsolute (settings.ArchivePath));

            if (settings.ExportArchive)
                builder.Append ("-exportArchive");

            if (settings.ExportFormat.HasValue)
                builder.Append ("-exportFormat " + settings.ExportFormat.Value.ToString ().ToLowerInvariant ());

            if (settings.ExportPath != null)
                builder.Append ("-exportPath " + GetQuotedAbsolute (settings.ExportPath));

            if (settings.ExportOptionsPlist != null)
                builder.Append("-exportOptionsPlist " + GetQuotedAbsolute(settings.ExportOptionsPlist));

            if (!string.IsNullOrEmpty (settings.ExportProvisioningProfile))
                builder.Append ("-exportProvisioningProfile " + settings.ExportProvisioningProfile);

            if (!string.IsNullOrEmpty (settings.ExportSigningIdentity))
                builder.Append ("-exportSigningIdentity " + settings.ExportSigningIdentity);

            if (!string.IsNullOrEmpty (settings.ExportInstallerIdentity))
                builder.Append ("-exportInstallerIdentity " + settings.ExportInstallerIdentity);

            if (settings.ExportWithOriginalSigningIdentity)
                builder.Append ("-exportWithOriginalSigningIdentity");

            if (settings.SkipUnavailableActions)
                builder.Append ("-skipUnavailableActions");

            if (settings.ExportLocalizations)
                builder.Append ("-exportLocalizations");

            if (settings.LocalizationPath != null)
                builder.Append ("-localizationPath " + GetQuotedAbsolute (settings.LocalizationPath));
            
            if (!string.IsNullOrEmpty (settings.ExportLanguage))
                builder.Append ("-exportLanguage " + settings.ExportLanguage);

            if(settings.Archive)
                builder.Append("archive");
            else
                builder.Append ("build");

            if (settings.Clean)
                builder.Append ("clean");

			if (settings.BuildSettings != null && settings.BuildSettings.Count > 0)
				builder.Append(string.Join(" ", settings.BuildSettings.Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value))));

            Run (settings, builder, settings.ToolPath);
        }

        public IEnumerable<XCodeSdk> ShowSdks (XCodeSettings settings)
        {
            const string rxSdkInfo = @"\s+(?<name>.+)\s+-sdk\s+(?<value>.+)";

            var builder = new ProcessArgumentBuilder ();

            builder.Append ("-showsdks");

            var process = this.RunProcess (settings, builder, settings.ToolPath, new ProcessSettings {
                RedirectStandardOutput = true,
            });
            process.WaitForExit ();

            var rx = new Regex (rxSdkInfo, RegexOptions.Compiled | RegexOptions.Singleline);

            foreach (var line in process.GetStandardOutput ()) {
                var match = rx.Match (line);

                if (match != null && match.Success && match.Groups != null
                    && match.Groups ["name"] != null && match.Groups ["value"] != null) {

                    yield return new XCodeSdk {
                        DisplayName = match.Groups ["name"].Value,
                        SdkValue = match.Groups ["value"].Value
                    };
                }
            }
        }
    }
}

