using System;
using Cake.Core.IO;
using Cake.Core;
using Cake.Core.Tooling;
using Cake.Testing;

namespace Cake.XCode.Tests.Fakes
{
    public class FakeCakeContext
    {
        ICakeContext context;
        FakeLog log;
        DirectoryPath testsDir;

        public FakeCakeContext ()
        {
            testsDir = new DirectoryPath (
                System.IO.Path.GetDirectoryName (System.Reflection.Assembly.GetExecutingAssembly ().Location));

            var fileSystem = new FileSystem ();
            log = new FakeLog ();
            var environment = new CakeEnvironment(new CakePlatform(), new CakeRuntime(), log);
            var globber = new Globber(fileSystem, environment);
            var args = new FakeCakeArguments ();
            var processRunner = new ProcessRunner (environment, log);
            var registry = new WindowsRegistry ();
            var toolRepo = new ToolRepository (environment);
            var config = new FakeConfiguration();
            var toolResStrat = new ToolResolutionStrategy (fileSystem, environment, globber, config);
            var toolLocator = new ToolLocator (environment, toolRepo, toolResStrat);

            context = new CakeContext (fileSystem, environment, globber, log, args, processRunner, registry, toolLocator);
            context.Environment.WorkingDirectory = testsDir;
        }

        public DirectoryPath WorkingDirectory {
            get { return testsDir; }
        }
            
        public ICakeContext CakeContext {
            get { return context; }
        }

        public string GetLogs ()
        {
            return string.Join(Environment.NewLine, log.Messages);
        }

        public void DumpLogs ()
        {
            foreach (var m in log.Messages)
                Console.WriteLine (m);
        }
    }
}

