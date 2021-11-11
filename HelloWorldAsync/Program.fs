open System.Net
open System.Threading
open FSharp.Data

// 2021-11-10 PJ:
// --------------
// The was taken from this paper: https://www.microsoft.com/en-us/research/wp-content/uploads/2016/02/async-padl-revised-v2.pdf
// However, I refactored it slightly as some dependencies got decommissioned/archived since the article publication date.
// To keep things simple, I replaced all the string-retrieving lines with one line leveraging the FSharp.Data nuget.
// More here: http://fsprojects.github.io/FSharp.Data/library/Http.html
let getWebPage (url: string) =
    async {
        let! response = Http.AsyncRequestString(url)

        printfn $"content length: %d{response.Length} characters"
    }

// 2021-11-10 PJ:
// --------------
// The work gets cancelled immediately.
let scenario1 () =
    let capability = new CancellationTokenSource()

    // 2021-11-10 PJ:
    // --------------
    // There is an error in the paper. Async.Start cannot accept Action<unit []> (result of Async.Parallel).
    // It can only take one asynchronous expression of type Async<unit>.
    Async.Start(getWebPage "http://www.google.com", capability.Token)

    // 2021-11-10 PJ:
    // --------------
    // Cancel immediately. We know the work has not finished yet.
    // It would probably take at least a few ms to finish.
    capability.Cancel()

    printfn "scenario1 finished"

// 2021-11-10 PJ:
// --------------
// The work does not get cancelled.
let scenario2 () =
    let capability = new CancellationTokenSource()

    Async.Start(getWebPage "http://www.google.com", capability.Token)

    // 2021-11-10 PJ:
    // --------------
    // Dummy wait to make sure the request finishes.
    Async.RunSynchronously(async { do! Async.Sleep(5000) })

    printfn "scenario2 finished"

// 2021-11-10 PJ:
// --------------
// The work does get cancelled, but only after 50ms.
// The purpose of this is to demonstrate that even when we have the dummy 5000ms wait,
// meaning: we have the time to wait for the external call,
// the underlying "getWebPage" operation gets cancelled successfully anyway.
// I would encourage readers to experiment with longer CancellationTokenSource
// timespans: 3s, 4s, etc. (anything below 5s - the dummy wait).
let scenario3 () =
    let capability =
        new CancellationTokenSource(System.TimeSpan.FromMilliseconds(50.0))

    Async.Start(getWebPage "http://www.google.com", capability.Token)

    // 2021-11-10 PJ:
    // --------------
    // Dummy wait to make sure the request finishes.
    Async.RunSynchronously(async { do! Async.Sleep(5000) })

    printfn "scenario3 finished"

// 2021-11-10 PJ:
// --------------
// This demonstrates:
// * implicit propagation of the cancellation token (50ms will always cancel the request)
// * exception catching in async context
let scenario4 () =
    let capability =
        new CancellationTokenSource(System.TimeSpan.FromMilliseconds(200.0))

    let throwingJob =
        async {
            try
                // 2021-11-10 PJ:
                // --------------
                // This will trigger an exception:
                do! getWebPage "abc"
                // This will not:
                // do! getWebPage "http://www.google.com"
            with
            | _ -> printfn "getWebPageContentLength failed"
        }

    Async.Start(throwingJob, capability.Token)

    // 2021-11-10 PJ:
    // --------------
    // Dummy wait to make sure the request finishes.
    Async.RunSynchronously(async { do! Async.Sleep(5000) })

    printfn "scenario4 finished"

[<EntryPoint>]
let main argv =
    scenario4 ()
    0