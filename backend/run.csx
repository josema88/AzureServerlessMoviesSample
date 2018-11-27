#r "Microsoft.Azure.DocumentDB.Core"
#r "Newtonsoft.Json"
#load "Movies.csx"

using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;


private static string endpointUrl = Environment.GetEnvironmentVariable("cosmosDBAccountEndpoint"); 
private static string authorizationKey = Environment.GetEnvironmentVariable("cosmosDBAccountKey"); 
private static DocumentClient client = new DocumentClient(new Uri(endpointUrl), authorizationKey);

public static async Task<IActionResult> Run(HttpRequest req, ILogger log)
{
    log.LogInformation("C# HTTP trigger function processed a request.");

    FeedOptions queryOptions = new FeedOptions { MaxItemCount = 100 };
    // Now execute the same query via direct SQL
        IQueryable<Movies> movieQueryInSql = client.CreateDocumentQuery<Movies>(
                UriFactory.CreateDocumentCollectionUri("moviesDb", "movies"),
                "SELECT * FROM movies",
                queryOptions);

        Console.WriteLine("Running direct SQL query...");
        if(movieQueryInSql == null)
            return new BadRequestObjectResult("No se encontró ningun objeto");
        var json = JsonConvert.SerializeObject(movieQueryInSql.ToArray());
    return new OkObjectResult(json);
}