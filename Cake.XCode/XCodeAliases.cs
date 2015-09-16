using System;
using Cake.Core.IO;
using Cake.Core;
using System.Collections.Generic;

namespace Cake.XCode
{
    public static class CocoaPodAliases
    {
        /// <summary>
        /// Runs pod install for the given project directory
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="projectDirectory">The project directory.</param>
        public static void CocoaPodInstall (this ICakeContext context, DirectoryPath projectDirectory)
        {
            CocoaPodInstall (context, projectDirectory, null);
        }

        /// <summary>
        /// Runs pod install for the given project directory
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="projectDirectory">The project directory.</param>
        /// <param name="settings">The settings.</param>
        public static void CocoaPodInstall (this ICakeContext context, DirectoryPath projectDirectory, CocoaPodInstallSettings settings) 
        {
            var r = new CocoaPodRunner (context.FileSystem, context.Environment, context.ProcessRunner, context.Globber);
            r.Install (projectDirectory, settings);
        }

        /// <summary>
        /// Runs pod update for the given project directory, updating all pods
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="projectDirectory">The project directory.</param>
        public static void CocoaPodUpdate (this ICakeContext context, DirectoryPath projectDirectory)
        {
            CocoaPodUpdate (context, projectDirectory, null, new CocoaPodUpdateSettings ());
        }

        /// <summary>
        /// Runs pod update for the given project directory, updating all pods
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="projectDirectory">The project directory.</param>
        /// <param name="settings">The settings.</param>
        public static void CocoaPodUpdate (this ICakeContext context, DirectoryPath projectDirectory, CocoaPodUpdateSettings settings)
        {
            CocoaPodUpdate (context, projectDirectory, null, settings);
        }

        /// <summary>
        /// Runs pod update for the given project directory, updating only the specified pods
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="projectDirectory">The project directory.</param>
        /// <param name="podNames">The specific pod names to update.</param>
        /// <param name="settings">The settings.</param>
        public static void CocoaPodUpdate (this ICakeContext context, DirectoryPath projectDirectory, string[] podNames, CocoaPodUpdateSettings settings)
        {
            var r = new CocoaPodRunner (context.FileSystem, context.Environment, context.ProcessRunner, context.Globber);
            r.Update (projectDirectory, podNames, settings);
        }
    }

    public static class XCodeAliases
    {
        /// <summary>
        /// Gets the various SDK's installed and available on the machine
        /// </summary>
        /// <returns>The xcode sdks.</returns>
        /// <param name="context">The context.</param>
        public static IEnumerable<XCodeSdk> XCodeSdks (this ICakeContext context)
        {
            return XCodeSdks (context, new XCodeSettings ());
        }

        /// <summary>
        /// Gets the various SDK's installed and available on the machine
        /// </summary>
        /// <returns>The xcode sdks.</returns>
        /// <param name="settings">The settings.</param>
        public static IEnumerable<XCodeSdk> XCodeSdks (this ICakeContext context, XCodeSettings settings)
        {
            var r = new XCodeBuildRunner (context.FileSystem, context.Environment, context.ProcessRunner, context.Globber);
            return r.ShowSdks (settings);
        }

        /// <summary>
        /// Runs xcodebuild with the given settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        public static void XCodeBuild (this ICakeContext context, XCodeBuildSettings settings)
        {
            var r = new XCodeBuildRunner (context.FileSystem, context.Environment, context.ProcessRunner, context.Globber);
            r.Build (settings);
        }
    }
}

