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
* `async { }` - computation expression

## the gist

* `async { }`
* `let!`
* `Task<T> -> Async<T>`

## reading

* [Async programming in F#](https://docs.microsoft.com/en-us/dotnet/fsharp/tutorials/asynchronous-and-concurrent-programming/async)
* [The F# Asynchronous Programming Model](https://www.microsoft.com/en-us/research/wp-content/uploads/2016/02/async-padl-revised-v2.pdf)
* [Asynchronous programming](https://fsharpforfunandprofit.com/posts/concurrency-async-and-parallel/)
* [Async in C# and F# Asynchronous gotchas in C#](http://tomasp.net/blog/csharp-async-gotchas.aspx/)
* [F# Async Guide](https://medium.com/@eulerfx/f-async-guide-eb3c8a2d180a)