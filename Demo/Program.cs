// See https://aka.ms/new-console-template for more information

using System;
using Microsoft.Extensions.DependencyInjection;
using Demo.GraphQL;
using StrawberryShake;

namespace Demo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            
            serviceCollection
                .AddConferenceClient()
                .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://workshop.chillicream.com/graphql"));

            IServiceProvider services = serviceCollection.BuildServiceProvider();

            IConferenceClient client = services.GetRequiredService<IConferenceClient>();
        
            var result = await client.GetSessions.ExecuteAsync();
            result.EnsureNoErrors();

            foreach (var session in result.Data.Sessions.Nodes)
            {
                Console.WriteLine(session.Title);
                Console.WriteLine(session.Abstract);
            }
        }
    }
}
