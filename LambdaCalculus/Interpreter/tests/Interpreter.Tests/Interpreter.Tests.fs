namespace Interpreter.Tests

open NUnit.Framework
open FsUnit
open Interpreter

[<TestFixture>]
type InterpreterTestClass () =

    [<Test>]
    member this.``For ((lambda a.(lambda b.b b) (lambda b.b b)) b) ((lambda c.(c b)) (lambda a.a)) should return (((lambda b.b b) (lambda b.b b)) b)`` () =
        ()

    [<Test>]
    member this.``For (lambda x y z.x z (y z)) (lambda x y.x) (lambda x y.x) should return (lambda x.x)`` () =
        ()