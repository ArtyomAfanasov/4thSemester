namespace Workflow.Tests

open NUnit.Framework
open FsUnit
open StringCalculations

[<TestFixture>]
type StringCalculationsTestsClass () =
    let calculate = new StringCalculationFlow()
    
    [<Test>]
    member this.``IntegerNumbersAsAStringShouldBeCalculated`` () =
        // act
        let result = calculate {
            let! x = "1"
            let! y = "2"
            let z = x + y
            return z
        }

        // assert
        result.Value |> should equal 3
    
    [<Test>]
    member this.``NotNumbersInStringShouldReturnErrorString`` () =
        // act
        let result = calculate {
            let! x = "1"
            let! y = "b"
            let z = x + y
            return z
        }

        // assert
        result |> should equal None