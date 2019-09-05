using System;
using System.Collections.Generic;
using Cake.CocoaPods;
using Cake.Common.IO;
using Cake.Core.IO;
using Cake.XCode.Tests.Fakes;
using Xunit;

namespace Cake.XCode.Tests
{
    public class XCodeTests : IDisposable
    {
        FakeCakeContext context;

        public XCodeTests ()
        {
            context = new FakeCakeContext ();
            context.CakeContext.Environment.WorkingDirectory = System.IO.Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "..", "..");

            //context.CakeContext.CleanDirectories("./TestProjects/**/bin");
            //context.CakeContext.CleanDirectories("./TestProjects/**/obj");

            if (context.CakeContext.DirectoryExists ("./TestProjects/tmp"))
                context.CakeContext.DeleteDirectory ("./TestProjects/tmp", new DeleteDirectorySettings { Recursive = true });

            context.CakeContext.CreateDirectory ("./TestProjects/tmp");
        }
        
        public void Dispose ()
        {
            context.DumpLogs ();
        }

        [Fact]
        public void PodVersion ()
        {
            var version = context.CakeContext.CocoaPodVersion ();

            Assert.NotNull (version);
        }

        [Fact]
        public void PodInstall ()
        {
            var podfile = new List<string> {
                "platform :ios, '7.0'",
                "install! 'cocoapods', :integrate_targets => false",
                "target 'Xamarin' do",
                "  use_frameworks!",
                "end",
                "pod 'GoogleAnalytics', '3.13'"
            };

            var version = context.CakeContext.CocoaPodVersion ();

            if (version < new Version (1, 0))
                podfile.RemoveRange (0, 4);
            
            var f = new FilePath ("./TestProjects/tmp/Podfile")
                .MakeAbsolute (context.CakeContext.Environment).FullPath;
            System.IO.File.WriteAllLines (f, podfile.ToArray ());

            context.CakeContext.CocoaPodInstall ("./TestProjects/tmp/", new CocoaPodInstallSettings {
                NoIntegrate = true
            });

            Assert.True (context.CakeContext.FileExists ("./TestProjects/tmp/Podfile.lock"));
            Assert.True (context.CakeContext.FileExists ("./TestProjects/tmp/Pods/GoogleAnalytics/Libraries/libGoogleAnalytics.a"));
        }

        [Fact]
        public void PodUpdate ()
        {
            var podfile = new List<string> {
                "platform :ios, '7.0'",
                "install! 'cocoapods', :integrate_targets => false",
                "target 'Xamarin' do",
                "  use_frameworks!",
                "end",
                "pod 'GoogleAnalytics', '~> 3.12'"
            };

            var version = context.CakeContext.CocoaPodVersion ();

            if (version < new Version (1, 0))
                podfile.RemoveRange (0, 4);

            var f = new FilePath ("./TestProjects/tmp/Podfile")
                .MakeAbsolute (context.CakeContext.Environment).FullPath;
            System.IO.File.WriteAllLines (f, podfile.ToArray ());

            context.CakeContext.CocoaPodInstall ("./TestProjects/tmp/", new CocoaPodInstallSettings {
                NoIntegrate = true
            });

            Assert.True (context.CakeContext.FileExists ("./TestProjects/tmp/Podfile.lock"));
            Assert.True (context.CakeContext.FileExists ("./TestProjects/tmp/Pods/GoogleAnalytics/Libraries/libGoogleAnalytics.a"));

            context.CakeContext.CocoaPodUpdate ("./TestProjects/tmp/", new CocoaPodUpdateSettings {
                NoIntegrate = true
            });       

            var pfl = new FilePath ("./TestProjects/tmp/Podfile.lock");
            var text = System.IO.File.ReadAllText (pfl.MakeAbsolute (context.CakeContext.Environment).FullPath);

            Assert.False (text.Contains ("- GoogleAnalytics (3.12.0)"));
        }

        [Fact]
        public void XCodeSDKs ()
        {
            var sdks = context.CakeContext.XCodeSdks ();

            Assert.NotEmpty (sdks);
        }

        [Fact]
        public void XCodeBuild ()
        {
            context.CakeContext.CopyDirectory ("./TestProjects/XCodeProject/", "./TestProjects/tmp/");

            context.CakeContext.CocoaPodInstall ("./TestProjects/tmp/");

            context.CakeContext.XCodeBuild (new XCodeBuildSettings {
                Workspace = "./TestProjects/tmp/FieldExporter.xcworkspace",
                Scheme = "FieldExporter",
                Sdk = "iphonesimulator",
                Arch = "i386",
                Configuration = "Release",
                DerivedDataPath = "./TestProjects/tmp/build",
                Clean = true,
                BuildSettings = new Dictionary<string, string>
                {
                    { "ENABLE_BITCODE", "YES" },
                    { "BITCODE_GENERATION_MODE", "bitcode" },
                }
            });

            Assert.True (context.CakeContext.DirectoryExists ("./TestProjects/tmp/build/Build/"));
        }
    }
}

