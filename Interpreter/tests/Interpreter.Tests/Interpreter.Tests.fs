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
    
    // Can i avoid such terrible DU?
    [<Test>]
    member this.``Test should correct count applique `((\l x. \l y. x y x) b) c` with ... result.`` () =
        Applique(
            Applique(
                LambdaAbstract('x', 
                    LambdaAbstract('y', 
                        Applique(
                            Applique(
                                Variable('x'), Variable('y')), Variable('x')))), Variable('b')), Variable('c'))
        |> countApplique |> should equal 4
    
    [<Test>]
    member this.``Test should correct normalize `((\l x. \l y. x y x) b) c` with ... result.`` () =
        Applique(
            Applique(
                LambdaAbstract('x', 
                    LambdaAbstract('y', 
                        Applique(
                            Applique(
                                Variable('x'), Variable('y')), Variable('x')))), Variable('b')), Variable('c'))
        |>
        normalizeTerm 
        |> should equal (Applique(Applique(Variable('b'), Variable('c')), Variable('b')))   
    
    [<Test>]
    member this.``Big main test. Test should correct normalize `((\l x. \l y. \l z. x z (y z)) (\l x. \l y. x)) \l x. \l y. x` with ... result.`` () =
        let firstPart = 
            LambdaAbstract('x', 
                LambdaAbstract('y', 
                    LambdaAbstract('z', 
                        Applique(
                            Applique(Variable('x'), Variable('z')), Applique(Variable('y'), Variable('z'))))))    
                        
        let secondPart = LambdaAbstract('x', LambdaAbstract('y', Variable('x')))

        let thirdPart = LambdaAbstract('x', LambdaAbstract('y', Variable('x')))

        Applique(Applique(firstPart, secondPart), thirdPart)
        |>
        normalizeTerm |> should equal (LambdaAbstract('z', Variable('z')))   
            
    [<Test>]
    member this.``Test should correct normalize `(\l x. x) (\l y. y)` with ... result.`` () =        
        let term = Applique(LambdaAbstract('x', Variable('x')), LambdaAbstract('y', Variable('y')))
        
        term |> normalizeTerm |> should equal (LambdaAbstract('y', Variable('y')))

    // Test with hand alpha conversion. But this conversion is useless.
    [<Test>]
    member this.``Test should correct normalize `((\l x. \l y. \l z. x z (y z)) (\l a. \l b. a)) \l c. \l d. c` with ... result.`` () =
        let firstPart = 
            LambdaAbstract('x', 
                LambdaAbstract('y', 
                    LambdaAbstract('z', 
                        Applique(
                            Applique(Variable('x'), Variable('z')), Applique(Variable('y'), Variable('z'))))))    
                        
        let secondPart = LambdaAbstract('a', LambdaAbstract('b', Variable('a')))

        let thirdPart = LambdaAbstract('c', LambdaAbstract('d', Variable('c')))

        let term = Applique(Applique(firstPart, secondPart), thirdPart)
        
        term |> normalizeTerm |> should equal (LambdaAbstract('z', Variable('z')))
        
    // Tests for debug.
    [<Test>]
    member this.``Step 2. Test should correct normalize `some` with ... result.`` () =
        let firstPart = 
            LambdaAbstract('y',
                LambdaAbstract('z', 
                    Applique(
                        Applique(LambdaAbstract('u', LambdaAbstract('i', Variable('u'))), Variable('z')), Applique(Variable('y'), Variable('z')))))
        
        let secondPart = 
            LambdaAbstract('h', 
                LambdaAbstract('j',
                    Variable('h')))

        Applique(firstPart, secondPart)
        |>
        normalizeTerm |> should equal (LambdaAbstract('z', Variable('z')))

    [<Test>]
    member this.``Step 3. Test should correct normalize `(\l z. ((\l t u. t) z)) ((\l b c. b) z))` with ... result.`` () =
        (LambdaAbstract('z', 
            Applique(
                Applique(
                    LambdaAbstract('t', 
                        LambdaAbstract('u', 
                            Variable('t'))), 
                                Variable('z')),
                                    Applique(
                                        LambdaAbstract('b', 
                                            LambdaAbstract('c', Variable('b'))), Variable('z')))))
        |>
        normalizeTerm |> should equal (LambdaAbstract('z', Variable('z')))

    [<Test>]
    member this.``Step 4. Test should correct normalize `(\l z. (\l a. z)) ((\l b c. b) z))` with ... result.`` () =
        (LambdaAbstract('z', 
            Applique(
                LambdaAbstract('a', 
                    Variable('z')),
                        Applique(
                            LambdaAbstract('b', 
                                LambdaAbstract('c', Variable('b'))), Variable('z')))))
        |>
        normalizeTerm |> should equal (LambdaAbstract('z', Variable('z')))