using Autofac;
using MSDosNote.Domain.Interfaces;
using MSDosNote.Infrastructure.Data;
using System.Diagnostics.CodeAnalysis;

namespace MSDosNote.App.Consolos
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        private static IContainer CompositionRoot()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>();
            builder.RegisterType<NoteRepository>().As<INoteRepository>();
            return builder.Build();
        }

        static void Main(string[] args)
        {
            try
            {
                CompositionRoot().Resolve<Application>().Run(args);
            }
            catch (System.Exception e)
            {
                global::System.Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}
