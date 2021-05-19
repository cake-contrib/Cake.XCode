using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cake.CocoaPods
{
    /// <summary>
    /// CocoaPod settings.
    /// </summary>
    public class CocoaPodSettings : ToolSettings
    {
    }

    /// <summary>
    /// CocoaPod install settings.
    /// </summary>
    public class CocoaPodInstallSettings : CocoaPodSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether or not to integrate the pods into an xcode project.
        /// </summary>
        /// <value><c>true</c> to not integrate into a project; otherwise, <c>false</c>.</value>
        [Obsolete ("Not available in CocoaPods >= 1.0")]
        public bool NoIntegrate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to clean .
        /// </summary>
        /// <value><c>true</c> if no cleaning should occur; otherwise, <c>false</c>.</value>
        [Obsolete ("Not available in CocoaPods >= 1.0")]
        public bool NoClean { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to update the repo.
        /// </summary>
        /// <value><c>true</c> if repo should not be updated; otherwise, <c>false</c>.</value>
        [Obsolete ("Not available in CocoaPods >= 1.0")]
        public bool NoRepoUpdate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the output should be silent.
        /// </summary>
        /// <value><c>true</c> if output should be silent; otherwise, <c>false</c>.</value>
        public bool Silent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the output should be verbose.
        /// </summary>
        /// <value><c>true</c> if output should be verbose; otherwise, <c>false</c>.</value>
        public bool Verbose { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the -noansi flag should be set
        /// </summary>
        /// <value><c>true</c> if -noansi flag should be set; otherwise, <c>false</c>.</value>
        public bool NoAnsi { get; set; }
    }

    /// <summary>
    /// CocoaPod update settings.
    /// </summary>
    public class CocoaPodUpdateSettings : CocoaPodSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether or not to integrate the pods into an xcode project.
        /// </summary>
        /// <value><c>true</c> to not integrate into a project; otherwise, <c>false</c>.</value>
        [Obsolete ("Not available in CocoaPods >= 1.0")]
        public bool NoIntegrate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to clean .
        /// </summary>
        /// <value><c>true</c> if no cleaning should occur; otherwise, <c>false</c>.</value>
        [Obsolete ("Not available in CocoaPods >= 1.0")]
        public bool NoClean { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to update the repo.
        /// </summary>
        /// <value><c>true</c> if repo should not be updated; otherwise, <c>false</c>.</value>
        public bool NoRepoUpdate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the output should be silent.
        /// </summary>
        /// <value><c>true</c> if output should be silent; otherwise, <c>false</c>.</value>
        public bool Silent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the output should be verbose.
        /// </summary>
        /// <value><c>true</c> if output should be verbose; otherwise, <c>false</c>.</value>
        public bool Verbose { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the -noansi flag should be set
        /// </summary>
        /// <value><c>true</c> if -noansi flag should be set; otherwise, <c>false</c>.</value>
        public bool NoAnsi { get; set; }
    }

    internal class CocoaPodRunner : Tool<CocoaPodSettings>
    {
        readonly ICakeEnvironment _cakeEnvironment;

        public CocoaPodRunner (IFileSystem fileSystem, ICakeEnvironment cakeEnvironment, IProcessRunner processRunner, IToolLocator toolLocator)
            : base (fileSystem, cakeEnvironment, processRunner, toolLocator)
        {
            _cakeEnvironment = cakeEnvironment;
        }

        protected override string GetToolName ()
        {
            return "CocoaPods";
        }

        protected override IEnumerable<string> GetToolExecutableNames ()
        {
            return new [] { "pod" };
        }

        protected override IEnumerable<FilePath> GetAlternativeToolPaths (CocoaPodSettings settings)
        {
            return new [] {
                new FilePath ("/usr/local/bin/pod"),
                new FilePath ("/usr/bin/pod")
            };
        }

        public void Install (ICakeContext context, DirectoryPath projectDirectory, CocoaPodInstallSettings settings)
        {
            if (settings == null)
                settings = new CocoaPodInstallSettings ();

            var version = GetVersion (settings);

            var builder = new ProcessArgumentBuilder ();

            builder.Append ("install");

            if (version < new Version (1, 0)) {
                if (settings.NoClean)
                    builder.Append ("--no-clean");

                if (settings.NoIntegrate)
                    builder.Append ("--no-integrate");

                if (settings.NoRepoUpdate)
                    builder.Append ("--no-repo-update");
            } else {

                if (settings.NoClean)
                    Warn (context, "--no-clean is not a valid option for CocoaPods >= 1.0");
                if (settings.NoIntegrate)
                    Warn (context, "--no-integrate is not a valid option for CocoaPods >= 1.0"
                                     + Environment.NewLine
                                    + "Use `install! 'cocoapods', :integrate_targets => false` in your Podfile instead");
                if (settings.NoRepoUpdate)
                    Warn (context, "--no-repo-update is not a valid option for CocoaPods >= 1.0");
            }

            if (settings.Silent)
                builder.Append ("--silent");

            if (settings.Verbose)
                builder.Append ("--verbose");

            if (settings.NoAnsi)
                builder.Append ("--no-ansi");

            if (projectDirectory != null)
                builder.Append ("--project-directory=" + projectDirectory.MakeAbsolute (_cakeEnvironment).FullPath.Quote ());

            Run (settings, builder);
        }

        public void Update (ICakeContext context, DirectoryPath projectDirectory, string[] podNames, CocoaPodUpdateSettings settings)
        {
            if (settings == null)
                settings = new CocoaPodUpdateSettings ();

            var version = GetVersion (settings);

            var builder = new ProcessArgumentBuilder ();

            builder.Append ("update");

            if (podNames != null && podNames.Length > 0) {
                foreach (var pn in podNames)
                    builder.Append (pn);
            }

            if (version < new Version (1, 0)) {
                if (settings.NoClean)
                    builder.Append ("--no-clean");

                if (settings.NoIntegrate)
                    builder.Append ("--no-integrate");
            } else {

                if (settings.NoClean)
                    Warn (context, "--no-clean is not a valid option for CocoaPods >= 1.0");
                if (settings.NoIntegrate)
                    Warn (context, "--no-integrate is not a valid option for CocoaPods >= 1.0"
                                     + Environment.NewLine
                                    + "Use `install! 'cocoapods', :integrate_targets => false` in your Podfile instead");
            }

            if (settings.NoRepoUpdate)
                builder.Append ("--no-repo-update");

            if (settings.Silent)
                builder.Append ("--silent");

            if (settings.Verbose)
                builder.Append ("--verbose");

            if (settings.NoAnsi)
                builder.Append ("--no-ansi");

            if (projectDirectory != null)
                builder.Append ("--project-directory=" + projectDirectory.MakeAbsolute (_cakeEnvironment).FullPath.Quote ());

            if (settings.ToolPath == null)
                Run (settings, builder);
            else
                Run (settings, builder);
        }

        public Version GetVersion (CocoaPodSettings settings)
        {
            var s = settings ?? new CocoaPodSettings ();

            var builder = new ProcessArgumentBuilder ();

            builder.Append ("--version");
            var process = RunProcess (s, builder, new ProcessSettings {
                RedirectStandardOutput = true
            });


            process.WaitForExit ();

            var text = process.GetStandardOutput ().ToList ();

            foreach (var line in text) {
                Version tmpVersion;
                if (Version.TryParse (line.Trim (), out tmpVersion))
                    return tmpVersion;
            }

            return null;
        }

        public void RepoUpdate (CocoaPodSettings settings)
        {
            var builder = new ProcessArgumentBuilder ();
            builder.Append ("repo");
            builder.Append ("update");

            var process = RunProcess (settings, builder);

            process.WaitForExit ();
        }

        void Warn (ICakeContext context, string text, params object[] args)
        {
            context.Log.Write (Core.Diagnostics.Verbosity.Normal, Core.Diagnostics.LogLevel.Warning, text, args);
        }
    }
}

