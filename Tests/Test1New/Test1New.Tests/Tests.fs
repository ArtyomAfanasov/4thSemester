namespace Test1New.Tests

open NUnit.Framework
open FsUnit

[<TestFixture>]
type ProgramTestClass () =

    [<Test>]
    member this.``some `` () =
        Assert.IsTrue(true)