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
                        .AddBookClient()
                        .ConfigureHttpClient(client => client.BaseAddress = new Uri("http://localhost:4200/graphql"));

            IServiceProvider services = serviceCollection.BuildServiceProvider();

            IBookClient client = services.GetRequiredService<IBookClient>();

            Console.WriteLine("exeucting graphql command on the client side");

            var result = await client.GetBook.ExecuteAsync();
            Console.WriteLine(result.Data.Book.Title);
        }
    }
}
