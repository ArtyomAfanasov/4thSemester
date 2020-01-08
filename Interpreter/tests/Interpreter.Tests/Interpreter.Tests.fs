namespace Interpreter.Tests

open NUnit.Framework
open FsUnit
open Interpreter
open Normalizer

[<TestFixture>]
type InterpreterTestClass () =    
    [<Test>]
    member this.``Test.`` () =
        let termFromString = 
            "(lx.ly.(x y) b)"
                |> Parse.parse
        termFromString 
        |> normalizeTerm 
        |> should equal (LambdaAbstraction("y", Application(Variable("b"), Variable("y"))))      