#r "Microsoft.Azure.DocumentDB.Core"

using Microsoft.Azure.Documents;
using System.Collections.Generic;
using System;

public static void Run(IReadOnlyList<Document> input, ILogger log)
{
    if (input != null && input.Count > 0)
    {
        log.LogInformation("Documents modified " + input.Count);
	    log.LogInformation("First document Id " + input[0].Id);
    }
}