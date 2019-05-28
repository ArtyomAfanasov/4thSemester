namespace LastTest.Tests

open NUnit.Framework
open FsUnit

[<TestFixture>]
type ProgramTestClass () =

    [<Test>]
    member this.`` 1 ``() =
        1 |> should equal 1