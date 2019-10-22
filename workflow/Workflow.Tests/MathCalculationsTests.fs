namespace Workflow.Tests

open NUnit.Framework
open FsUnit
open MathCalculations

[<TestFixture>]
type MathCalculationsTestsClass () =
    let rounding calculationAccurancy = new CalculationFlow(calculationAccurancy)
    
    [<Test>]
    member this.``RoundingToThreeSignForDivide`` () =
        // act
        let result = 
            rounding 3 {
                let! a = 2.0 / 12.0
                let! b = 3.5
                return a / b
            }

        // assert
        result |> should equal 0.048
       
    [<Test>]
    member this.``RoundingToZeroSignForDivide`` () =
        // act
        let result = 
            rounding 0 {
                let! a = 8.0
                let! b = 3.5
                return a / b
            }

        // assert
        result |> should equal 2
    
    [<Test>]
    member this.``RoundingToOneSignForMultiplication`` () =
        // act
        let result = rounding 1 { return 1.5 * 3.0 }

        // assert
        result |> should equal 4.5 
    
    [<Test>]
    member this.``RoundingToOneSignForSumAndMinus`` () =
        // act
        let result = 
            rounding 1 { 
                let! a = 5.2 + 2.7
                let! b = a - 2.7
                return b
                }

        // assert
        result |> should equal 5.2
    
    [<Test>]
    member this.``RoundingToOneSignForPowAndSqrt`` () =
        // act
        let result = 
            rounding 1 { 
                let! a = pown 3.0 2
                let! b = sqrt a
                return b
                }

        // assert
        result |> should equal 3.0
    
    [<Test>]
    member this.``WithComputationalAccuracyArgumentMoreThan15ShouldFail`` () =
        // assert
        (fun () -> 
            rounding 16 { 
                let! a = 1.0 * 1.0
                return a 
            } 
            |> ignore) |> should throw typeof<System.ArgumentOutOfRangeException>
   
// I thought about diriving by zero.
[<TestFixture>]
type FSharpTestsClass () =
    let rounding calculationAccurancy = new CalculationFlow(calculationAccurancy)
        
    [<Test>]
    member this.``DivideByMonadZeroShouldReturnInfinity`` () =
        // act
        let result = 
            rounding 0 {
                let! a = 25.0 / 12.0
                let! b = 0.0
                return a / b
            }

        // assert
        result |> should equal infinity
       
    [<Test>]
    member this.``DivideByZeroToTheRightOfTheEqualSignShouldReturnInfinity`` () =
        // act
        let result = 
            rounding 0 {
                let! a = 25.0 / 0.0
                let! b = 5.0
                return a / b
            }

        // assert
        result |> should equal infinity

    [<Test>]
    member this.``DivideInfinityByInfinityShouldReturn`` () =
        // act
        let result = 
            rounding 0 {
                let! a = 25.0 / 0.0
                let! b = 25.0 / 0.0
                return a / b
            }

        // assert
        result |> should equal System.Double.NaN