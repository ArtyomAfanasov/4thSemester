namespace Interpreter.Tests

open NUnit.Framework
open FsUnit
open NewInterpreter

[<TestFixture>]
type InterpreterTestClass () =
    [<Test>]
    member this.``Test should correct normalize `(\l x. x) b` with 'b' result.`` () =
        (Applique(LambdaAbstract('x', Variable('x')), Variable('b')))
        |>
        normalizeTerm |> should equal (Variable('b'))
    
    [<Test>]
    member this.``Test should correct normalize `(\l x. \l y. x y) b` with ... result.`` () =
        (Applique(LambdaAbstract('x', LambdaAbstract('y', Applique(Variable('x'), Variable('y')))), Variable('b')))
        |>
        normalizeTerm |> should equal (LambdaAbstract('y', Applique(Variable('b'), Variable('y'))))
    
    [<Test>]
    member this.``Test should correct count applique `((\l x. \l y. x y x) b) c` with ... result.`` () =
        Applique(
            Applique(
                LambdaAbstract('x', 
                                LambdaAbstract('y', Applique(
                                                        Applique(
                                                            Variable('x'), Variable('y')), Variable('x')))), Variable('b')), Variable('c'))
        |> countApplique |> should equal 4
    
    [<Test>]
    member this.``Test should correct normalize `((\l x. \l y. x y x) b) c` with ... result.`` () =
        Applique(
            Applique(
                LambdaAbstract('x', 
                                LambdaAbstract('y', Applique(
                                                        Applique(
                                                            Variable('x'), Variable('y')), Variable('x')))), Variable('b')), Variable('c'))
        |>
        normalizeTerm 
        |> should equal (Applique(Applique(Variable('b'), Variable('c')), Variable('b')))   
    
    [<Test>]
    member this.``Test should correct normalize `((\l x. \l y. \l z. x z (y z)) (\l x. \l y. x)) \l x. \l y. x` with ... result.`` () =
        let firstPart = 
            LambdaAbstract('x', 
                LambdaAbstract('y', 
                    LambdaAbstract('z', 
                        Applique(
                            Applique(Variable('x'), Variable('z')), Applique(Variable('y'), Variable('z'))))))    
                        
        let secondPart = LambdaAbstract('x', LambdaAbstract('y', Variable('x')))

        let thirdPart = LambdaAbstract('x', LambdaAbstract('y', Variable('x')))

        let term = Applique(Applique(firstPart, secondPart), thirdPart)
        
        term |> normalizeTerm |> should equal (LambdaAbstract('x', Variable('x')))