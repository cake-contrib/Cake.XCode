using Cake.Core.IO;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.CocoaPods
{
    /// <summary>
    /// Cocoapods aliases.
    /// </summary>
    [CakeAliasCategory("CocoaPods")]
    public static class CocoaPodAliases
    {
        /// <summary>
        /// Runs pod install for the given project directory
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="projectDirectory">The project directory.</param>
        [CakeMethodAlias]
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
        [CakeMethodAlias]
        public static void CocoaPodInstall (this ICakeContext context, DirectoryPath projectDirectory, CocoaPodInstallSettings settings)
        {
            var r = new CocoaPodRunner (context.FileSystem, context.Environment, context.ProcessRunner, context.Globber);
            r.Install (context, projectDirectory, settings);
        }

        /// <summary>
        /// Runs pod update for the given project directory, updating all pods
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="projectDirectory">The project directory.</param>
        [CakeMethodAlias]
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
        [CakeMethodAlias]
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
        [CakeMethodAlias]
        public static void CocoaPodUpdate (this ICakeContext context, DirectoryPath projectDirectory, string[] podNames, CocoaPodUpdateSettings settings)
        {
            var r = new CocoaPodRunner (context.FileSystem, context.Environment, context.ProcessRunner, context.Globber);
            r.Update (context, projectDirectory, podNames, settings);
        }

        /// <summary>
        /// Returns the version of CocoaPods
        /// </summary>
        /// <returns>The pod version.</returns>
        /// <param name="context">The context.</param>
        [CakeMethodAlias]
        public static System.Version CocoaPodVersion (this ICakeContext context)
        {
            return CocoaPodVersion (context, new CocoaPodSettings ());
        }

        /// <summary>
        /// Returns the version of CocoaPods
        /// </summary>
        /// <returns>The pod version.</returns>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        [CakeMethodAlias]
        public static System.Version CocoaPodVersion (this ICakeContext context, CocoaPodSettings settings)
        {
            var r = new CocoaPodRunner (context.FileSystem, context.Environment, context.ProcessRunner, context.Globber);
            return r.GetVersion (settings);
        }

        /// <summary>
        /// Updates the CocoaPods repo
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        [CakeMethodAlias]
        public static void CocoaPodRepoUpdate (this ICakeContext context)
        {
            var r = new CocoaPodRunner (context.FileSystem, context.Environment, context.ProcessRunner, context.Globber);
            r.RepoUpdate (new CocoaPodSettings ());
        }

        /// <summary>
        /// Updates the CocoaPods repo
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        [CakeMethodAlias]
        public static void CocoaPodRepoUpdate (this ICakeContext context, CocoaPodSettings settings)
        {
            var r = new CocoaPodRunner (context.FileSystem, context.Environment, context.ProcessRunner, context.Globber);
            r.RepoUpdate (settings);
        }
    }
}
