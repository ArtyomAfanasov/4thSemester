namespace MyLazy.Tests

open NUnit.Framework
open FsUnit

[<TestFixture>]
type SingleThreadedLazyTestClass () =

    [<Test>]
    member this.``test`` () =
        ()
