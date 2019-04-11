namespace Test1.Tests

open FsUnit
open NUnit.Framework
//open logic

[<TestFixture>]
type TestClass () =

    [<Test>]
    member this.`` new test ``() =       
        1 |> should equal 1