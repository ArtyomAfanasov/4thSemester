namespace Interpreter.Tests

open NUnit.Framework
open FsUnit
open Interpreter

[<TestFixture>]
type InterpreterTestClass () =
    [<Test>]
    member this.``Test should correct normalize `(\l x. x) b` with 'b' result.`` () =
        (Application(LambdaAbstraction('x', Variable('x')), Variable('b')))
        |>
        normalizeTerm |> should equal (Variable('b'))
    
    [<Test>]    
    member this.``Test should correct with right redex `x ((\l a. a) b)` with 'b' result.`` () =
        (Application(Variable('x'), Application(LambdaAbstraction('a', Variable('a')), Variable ('b'))))
        |>
        normalizeTerm |> should equal (Application(Variable('x'), Variable('b')))
    
    [<Test>]
    member this.``Test should correct normalize `(\l x. \l y. x y) b` with ... result.`` () =
        (Application(LambdaAbstraction('x', LambdaAbstraction('y', Application(Variable('x'), Variable('y')))), Variable('b')))
        |>
        normalizeTerm |> should equal (LambdaAbstraction('y', Application(Variable('b'), Variable('y'))))
    
    // Can i avoid such terrible DU?
    [<Test>]
    member this.``Test should correct count left applications `((\l x. \l y. x y x) b) c` with ... result.`` () =
        Application(
            Application(
                LambdaAbstraction('x', 
                    LambdaAbstraction('y', 
                        Application(
                            Application(
                                Variable('x'), Variable('y')), Variable('x')))), Variable('b')), Variable('c'))
        |> countApplique |> should equal 4    
    
    [<Test>]
    member this.``Test should correct count left and right applications `((\l x. \l y. \l z. x z (y z)) (\l x. \l y. x)) \l x. \l y. x` with ... result.`` () =
        let firstPart = 
            LambdaAbstraction('x', 
                LambdaAbstraction('y', 
                    LambdaAbstraction('z', 
                        Application(
                            Application(Variable('x'), Variable('z')), Application(Variable('y'), Variable('z'))))))    
                
        let secondPart = LambdaAbstraction('x', LambdaAbstraction('y', Variable('x')))

        let thirdPart = LambdaAbstraction('x', LambdaAbstraction('y', Variable('x')))

        Application(Application(firstPart, secondPart), thirdPart)
        |> countApplique |> should equal 5    
    
    [<Test>]
    member this.``Test should correct normalize with right redex`((\l x. x) ((\y. y) b))` with ... result.`` () =
        let rightRedex = Application(LambdaAbstraction('f', Variable('f')), Application(Variable('y'), Variable('z')))

        let firstPart = 
            LambdaAbstraction('x', 
                LambdaAbstraction('y', 
                    LambdaAbstraction('z', 
                        Application(
                            Application(Variable('x'), Variable('z')), rightRedex))))    
                        
        let secondPart = LambdaAbstraction('x', LambdaAbstraction('y', Variable('x')))

        let thirdPart = LambdaAbstraction('x', LambdaAbstraction('y', Variable('x')))

        Application(Application(firstPart, secondPart), thirdPart)
        |>
        normalizeTerm |> should equal (LambdaAbstraction('z', Variable('z')))   
    
    [<Test>]
    member this.``Test should correct normalize `((\l x. \l y. x y x) b) c` with ... result.`` () =
        Application(
            Application(
                LambdaAbstraction('x', 
                    LambdaAbstraction('y', 
                        Application(
                            Application(
                                Variable('x'), Variable('y')), Variable('x')))), Variable('b')), Variable('c'))
        |>
        normalizeTerm 
        |> should equal (Application(Application(Variable('b'), Variable('c')), Variable('b')))   
    
    [<Test>]
    member this.``Big main test. Test should correct normalize `((\l x. \l y. \l z. x z (y z)) (\l x. \l y. x)) \l x. \l y. x` with ... result.`` () =
        let firstPart = 
            LambdaAbstraction('x', 
                LambdaAbstraction('y', 
                    LambdaAbstraction('z', 
                        Application(
                            Application(Variable('x'), Variable('z')), Application(Variable('y'), Variable('z'))))))    
                        
        let secondPart = LambdaAbstraction('x', LambdaAbstraction('y', Variable('x')))

        let thirdPart = LambdaAbstraction('x', LambdaAbstraction('y', Variable('x')))

        Application(Application(firstPart, secondPart), thirdPart)
        |>
        normalizeTerm |> should equal (LambdaAbstraction('z', Variable('z')))   
            
    [<Test>]
    member this.``Test should correct normalize `(\l x. x) (\l y. y)` with ... result.`` () =        
        let term = Application(LambdaAbstraction('x', Variable('x')), LambdaAbstraction('y', Variable('y')))
        
        term |> normalizeTerm |> should equal (LambdaAbstraction('y', Variable('y')))

    // Test with hand alpha conversion. But this conversion is useless.
    [<Test>]
    member this.``Test should correct normalize `((\l x. \l y. \l z. x z (y z)) (\l a. \l b. a)) \l c. \l d. c` with ... result.`` () =
        let firstPart = 
            LambdaAbstraction('x', 
                LambdaAbstraction('y', 
                    LambdaAbstraction('z', 
                        Application(
                            Application(Variable('x'), Variable('z')), Application(Variable('y'), Variable('z'))))))    
                        
        let secondPart = LambdaAbstraction('a', LambdaAbstraction('b', Variable('a')))

        let thirdPart = LambdaAbstraction('c', LambdaAbstraction('d', Variable('c')))

        let term = Application(Application(firstPart, secondPart), thirdPart)
        
        term |> normalizeTerm |> should equal (LambdaAbstraction('z', Variable('z')))
        
    // Tests for debug.
    [<Test>]
    member this.``Step 2. Test should correct normalize `some` with ... result.`` () =
        let firstPart = 
            LambdaAbstraction('y',
                LambdaAbstraction('z', 
                    Application(
                        Application(LambdaAbstraction('u', LambdaAbstraction('i', Variable('u'))), Variable('z')), Application(Variable('y'), Variable('z')))))
        
        let secondPart = 
            LambdaAbstraction('h', 
                LambdaAbstraction('j',
                    Variable('h')))

        Application(firstPart, secondPart)
        |>
        normalizeTerm |> should equal (LambdaAbstraction('z', Variable('z')))

    [<Test>]
    member this.``Step 3. Test should correct normalize `(\l z. ((\l t u. t) z)) ((\l b c. b) z))` with ... result.`` () =
        (LambdaAbstraction('z', 
            Application(
                Application(
                    LambdaAbstraction('t', 
                        LambdaAbstraction('u', 
                            Variable('t'))), 
                                Variable('z')),
                                    Application(
                                        LambdaAbstraction('b', 
                                            LambdaAbstraction('c', Variable('b'))), Variable('z')))))
        |>
        normalizeTerm |> should equal (LambdaAbstraction('z', Variable('z')))

    [<Test>]
    member this.``Step 4. Test should correct normalize `(\l z. (\l a. z)) ((\l b c. b) z))` with ... result.`` () =
        (LambdaAbstraction('z', 
            Application(
                LambdaAbstraction('a', 
                    Variable('z')),
                        Application(
                            LambdaAbstraction('b', 
                                LambdaAbstraction('c', Variable('b'))), Variable('z')))))
        |>
        normalizeTerm |> should equal (LambdaAbstraction('z', Variable('z')))
    
    (*[<Test>]
    member this.``Test should correct set alphabet with ... result.`` () =
        Applique(LambdaAbstract('x', Applique(Variable('x'), Variable('y'))), LambdaAbstract('y', Variable('y')))
        |> 
        prepareToAlphaConversion |> should equal ['z']*)