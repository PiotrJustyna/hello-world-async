# readme

Asynchronous F# programming sandbox.

## notes

* orthogonal concepts:
  * concurrency
  * parallelism
  * asynchrony - not at the same time
* `Async<T>` type represents a composable asynchronous operations
* `Async` module contains functions which:
  * schedule
  * compose
  * transform
  
  asynchronous operations.
  
  Examples:
  
  * [`Async.RunSynchronously`](https://docs.microsoft.com/en-us/dotnet/fsharp/tutorials/async#asyncrunsynchronously)
  * [`Async.Start`](https://docs.microsoft.com/en-us/dotnet/fsharp/tutorials/async#asyncstart)
  * [`Async.StartChild`](https://docs.microsoft.com/en-us/dotnet/fsharp/tutorials/async#asyncstartchild)
  * [`Async.Catch`]()

## the gist

* `async { }` - computation expression/asynchronous expression. All expressions of such form are of type `Async<T>` for some `T`. [*source*](https://www.microsoft.com/en-us/research/wp-content/uploads/2016/02/async-padl-revised-v2.pdf)
* `expr := async { aexpr }` - complete syntax for asynchronous expressions.
* selected `aexpr` examples:
  * `do!` - execute async (`Async<unit>`).
  * `let!` - execute and bind async (`Async<T>`).
  * `return! expr` - tailcall to async
  * `return expr` - return result of async expression
* `Task<T> -> Async<T>` - interop translation of C# `Task<T>` to F# `Async<T>`.
* asynchronous function - normal function or method, but returning an asynchronous computation. It is common for asynchronous functions to have their entire bodies enclosed in `async { }`.
* cancellation:
  * C# `CancellationTokenSource` and `CancellationToken` are both supported.
  * cancellation tokens are implicitly propagated through the execution of an asynchronous operation.
  * Cancellation tokens are provided at the entry point to the execution of an asynchronous computation, e.g.:
    * `Async.RunSynchronously`
    * `Async.StartImmediate`
    * `Async.Start`
    
    reference: https://stackoverflow.com/questions/15284209/async-start-vs-async-startchild
* exceptions:
  * [x] basic exceptions
  * [x] nested exceptions
  * [x] nested cancellations.

    

## reading

* [x] [Async programming in F#](https://docs.microsoft.com/en-us/dotnet/fsharp/tutorials/asynchronous-and-concurrent-programming/async)
* [x] [The F# Asynchronous Programming Model](https://www.microsoft.com/en-us/research/wp-content/uploads/2016/02/async-padl-revised-v2.pdf)
* [x] [Async in C# and F# Asynchronous gotchas in C#](http://tomasp.net/blog/csharp-async-gotchas.aspx/)
* [x] [F# Async Guide](https://medium.com/@eulerfx/f-async-guide-eb3c8a2d180a)