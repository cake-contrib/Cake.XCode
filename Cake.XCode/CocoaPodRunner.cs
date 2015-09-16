using System;
using Cake.Core.IO;
using Cake.Core.Utilities;
using Cake.Core;
using System.Collections.Generic;

namespace Cake.CocoaPods
{
    /// <summary>
    /// CocoaPod settings.
    /// </summary>
    public class CocoaPodSettings
    {
        /// <summary>
        /// Gets or sets the path to pod executable.
        /// </summary>
        /// <value>The tool path.</value>
        public FilePath ToolPath { get; set; }
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
        public bool NoIntegrate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to clean .
        /// </summary>
        /// <value><c>true</c> if no cleaning should occur; otherwise, <c>false</c>.</value>
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

    /// <summary>
    /// CocoaPod update settings.
    /// </summary>
    public class CocoaPodUpdateSettings : CocoaPodSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether or not to integrate the pods into an xcode project.
        /// </summary>
        /// <value><c>true</c> to not integrate into a project; otherwise, <c>false</c>.</value>
        public bool NoIntegrate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to clean .
        /// </summary>
        /// <value><c>true</c> if no cleaning should occur; otherwise, <c>false</c>.</value>
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

        public CocoaPodRunner (IFileSystem fileSystem, ICakeEnvironment cakeEnvironment, IProcessRunner processRunner, IGlobber globber)
            : base (fileSystem, cakeEnvironment, processRunner, globber)
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

        public void Install (DirectoryPath projectDirectory, CocoaPodInstallSettings settings)
        {
            if (settings == null)
                settings = new CocoaPodInstallSettings ();
            
            var builder = new ProcessArgumentBuilder ();

            builder.Append ("install");

            if (settings.NoClean)
                builder.Append ("--no-clean");

            if (settings.NoIntegrate)
                builder.Append ("--no-integrate");

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

            Run (settings, builder, settings.ToolPath);
        }

        public void Update (DirectoryPath projectDirectory, string[] podNames, CocoaPodUpdateSettings settings)
        {
            var builder = new ProcessArgumentBuilder ();

            builder.Append ("update");

            if (podNames != null && podNames.Length > 0) {
                foreach (var pn in podNames)
                    builder.Append (pn);
            }

            if (settings.NoClean)
                builder.Append ("--no-clean");

            if (settings.NoIntegrate)
                builder.Append ("--no-integrate");

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
                Run (settings, builder, settings.ToolPath);
        }
    }
}

