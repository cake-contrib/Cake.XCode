using System;
using Cake.CocoaPods;
using Cake.Common.IO;
using Cake.Core.IO;
using Cake.XCode.Tests.Fakes;
using NUnit.Framework;

namespace Cake.XCode.Tests
{
    [TestFixture, Category ("XCodeTests")]
    public class XCodeTests
    {
        FakeCakeContext context;

        [SetUp]
        public void Setup ()
        {
            context = new FakeCakeContext ();

            context.CakeContext.CleanDirectories ("./TestProjects/**/bin");
            context.CakeContext.CleanDirectories ("./TestProjects/**/obj");

            if (context.CakeContext.DirectoryExists ("./TestProjects/tmp"))
                context.CakeContext.DeleteDirectory ("./TestProjects/tmp", true);

            context.CakeContext.CreateDirectory ("./TestProjects/tmp");
        }

        [TearDown]
        public void Teardown ()
        {
            context.DumpLogs ();
        }

        [Test]
        public void PodInstall ()
        {            
            context.CakeContext.CopyFile ("./TestProjects/PodInstall/Podfile", "./TestProjects/tmp/Podfile");
            context.CakeContext.CocoaPodInstall ("./TestProjects/tmp/", new CocoaPodInstallSettings {
                NoIntegrate = true
            });

            Assert.IsTrue (context.CakeContext.FileExists ("./TestProjects/tmp/Podfile.lock"));
            Assert.IsTrue (context.CakeContext.DirectoryExists ("./TestProjects/tmp/Pods/GoogleAnalytics"));
        }

        [Test]
        public void PodUpdate ()
        {
            context.CakeContext.CopyFile ("./TestProjects/PodUpdate/Podfile", "./TestProjects/tmp/Podfile");
            context.CakeContext.CocoaPodInstall ("./TestProjects/tmp/", new CocoaPodInstallSettings {
                NoIntegrate = true
            });

            Assert.IsTrue (context.CakeContext.FileExists ("./TestProjects/tmp/Podfile.lock"));
            Assert.IsTrue (context.CakeContext.DirectoryExists ("./TestProjects/tmp/Pods/GoogleAnalytics"));

            context.CakeContext.CocoaPodUpdate ("./TestProjects/tmp/", new CocoaPodUpdateSettings {
                NoIntegrate = true
            });       

            var pfl = new FilePath ("./TestProjects/tmp/Podfile.lock");
            var text = System.IO.File.ReadAllText (pfl.MakeAbsolute (context.CakeContext.Environment).FullPath);

            Assert.IsFalse (text.Contains ("- GoogleAnalytics (3.12.0)"));
        }

        [Test]
        public void XCodeSDKs ()
        {
            var sdks = context.CakeContext.XCodeSdks ();

            Assert.IsNotEmpty (sdks);
        }

        [Test]
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
                Clean = true
            });

            Assert.IsTrue (context.CakeContext.DirectoryExists ("./TestProjects/tmp/build/Build/"));
        }
    }
}

