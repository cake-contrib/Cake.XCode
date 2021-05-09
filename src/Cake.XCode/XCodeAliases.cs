using System.Collections.Generic;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.XCode
{
    /// <summary>
    /// XCode aliases.
    /// </summary>
    [CakeAliasCategory("XCode")]
    public static class XCodeAliases
    {
        /// <summary>
        /// Gets the various SDK's installed and available on the machine
        /// </summary>
        /// <returns>The xcode sdks.</returns>
        /// <param name="context">The context.</param>
        [CakeMethodAlias]
        public static IEnumerable<XCodeSdk> XCodeSdks (this ICakeContext context)
        {
            return XCodeSdks (context, new XCodeSettings ());
        }

        /// <summary>
        /// Gets the various SDK's installed and available on the machine
        /// </summary>
        /// <returns>The xcode sdks.</returns>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        [CakeMethodAlias]
        public static IEnumerable<XCodeSdk> XCodeSdks (this ICakeContext context, XCodeSettings settings)
        {
            var r = new XCodeBuildRunner (context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            return r.ShowSdks (settings);
        }

        /// <summary>
        /// Runs xcodebuild with the given settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        [CakeMethodAlias]
        public static void XCodeBuild (this ICakeContext context, XCodeBuildSettings settings)
        {
            var r = new XCodeBuildRunner (context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            r.Build (settings);
        }
    }
}

