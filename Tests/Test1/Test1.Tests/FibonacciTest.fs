namespace Test1.Tests

open FsUnit
open NUnit.Framework
open FibonacciNumber

[<TestFixture>]
type FibonacciTestClass () =

    [<Test>]
    member this.``SumOfOddFibonacciNumberLessThenSupremum with supremum is 4 should equal 2``() =       
        sumOfOddFibonacciNumberLessThenSupremumTest 4 |> should equal 2

    [<Test>]
    member this.``SumOfOddFibonacciNumberLessThenSupremum with supremum is 55 should equal 44``() =       
        sumOfOddFibonacciNumberLessThenSupremumTest 55 |> should equal 44

    // ( (1 + sqrt(5) )^x - (1 - sqrt(5))^x )/( (2^x) * sqrt(5) ) = 1346269, where x = 31 
    // ( (1 + sqrt(5) )^x - (1 - sqrt(5))^x )/( (2^x) * sqrt(5) ) = 832040, where x = 30 
    [<Test>]
    member this.``LastFibonacciNumberLessThan1000000 should equal 832040``() =       
        lastFibonacciNumberLessThan1000000 () |> should equal 832040

    // WolframAlpha: sum (( (1 + sqrt(5) )^x - (1 - sqrt(5))^x )/( (2^x) * sqrt(5) )) = 2178308, x = 1 to x = 30
    [<Test>]
    member this.``SumOfAllFibonacciNumberLessThenSupremumTest should equal 2178308``() =       
        sumOfAllFibonacciNumberLessThenSupremumTest 1000000 |> should equal 2178308        