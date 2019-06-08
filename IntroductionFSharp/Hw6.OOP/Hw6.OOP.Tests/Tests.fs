namespace Hw6.OOP.Tests

open FsUnit
open NUnit.Framework
open Program

[<TestFixture>]
type TestClass () =

    [<Test>]
    member this.``2`` () =
        Assert.IsTrue(true)
