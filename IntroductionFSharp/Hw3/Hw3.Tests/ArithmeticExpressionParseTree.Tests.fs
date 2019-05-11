namespace Hw3.Tests

open NUnit.Framework
open FsUnit
open ArithmeticExpressionParseTree

[<TestFixture>]
type ArithmeticExpressionParseTreeTestCalss () =

    [<Test>]
    member this.``calculateValue on Plus(Value(2),Value(4)) should return 6`` () =                    
        calculateValue (Plus(Value(2),Value(4))) |> should equal 6        
    
    [<Test>]
    member this.``calculateValue on Minus(Value(6), Value(4)) should return 2`` () = 
        calculateValue (Minus(Value(6), Value(4))) |> should equal 2

    [<Test>]
    member this.``calculateValue on Division(Value(7), Value(2) should return 3`` () = 
        calculateValue (Division(Value(7), Value(2))) |> should equal 3

    [<Test>]
    member this.``calculateValue on Multiply(Value(5), Value(5) should return 25`` () = 
        calculateValue (Multiply(Value(5), Value(5))) |> should equal 25   
   
    [<Test>]
    member this.``calculateValue on Minus (Plus (Multiply (Value (2), Value (4)), Division (Value (9), Value (3))), Division (Multiply (Value (4), Value (5)), Value (2))) should return 1`` () =
        calculateValue (Minus (Plus (Multiply (Value (2), Value (4)), Division (Value (9), Value (3))), Division (Multiply (Value (4), Value (5)), Value (2))))
        |> should equal 1
   
    [<Test>]
    member this.``calculateValue on Division(Value(7), Value(0) should return Exception`` () =
        (fun () -> calculateValue (Division (Value (7), Value (0))) |> ignore) |> should throw typeof<System.Exception>