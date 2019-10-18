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
    member this.``Test should correct normalize `(\l x. \l y. x y) b` with (\l y. b y) result.`` () =
        (Application(LambdaAbstraction('x', LambdaAbstraction('y', Application(Variable('x'), Variable('y')))), Variable('b')))
        |>
        normalizeTerm |> should equal (LambdaAbstraction('y', Application(Variable('b'), Variable('y'))))

    [<Test>]    
    member this.``Test should correct normalize with right redex `x ((\l a. a) b)` with 'b' result.`` () =
        (Application(Variable('x'), Application(LambdaAbstraction('a', Variable('a')), Variable ('b'))))
        |>
        normalizeTerm |> should equal (Application(Variable('x'), Variable('b')))
    
    // Can i avoid such terrible DU?    
    [<Test>]
    member this.``Test should correct count applications with 7 result.`` () =
        let firstPart = Application(Variable('1'), Variable('1'))  
        let secondPart = Application(Variable('2'), Variable('2'))  
        let thirdart = Application(Variable('3'), Variable('3'))  
        let fourthPart = Application(Variable('4'), Variable('4'))  
        let left = Application(firstPart, secondPart)
        let rigth = Application(thirdart, fourthPart)
        let main = Application(left, rigth)

        main |> countApplications |> should equal 7    
    
    [<Test>]
    member this.``Test should correct normalize `((\l x. \l y. x y x) b) c` with (b c b) result.`` () =
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
    member this.``Test should correct normalize `(\l x. x) (\l y. y)` with (\l y. y) result.`` () =        
        let term = Application(LambdaAbstraction('x', Variable('x')), LambdaAbstraction('y', Variable('y')))
        
        term |> normalizeTerm |> should equal (LambdaAbstraction('y', Variable('y')))
            
    // Tests for debugging via S K K = I.
    [<Test>]
    member this.``Step 2. Test should correct normalize `lazy` with (\l z. z) result.`` () =
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
    member this.``Step 3. Test should correct normalize `(\l z. ((\l t u. t) z)) ((\l b c. b) z))` with (\l z. z) result.`` () =
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
    member this.``Step 4. Test should correct normalize `(\l z. (\l a. z)) ((\l b c. b) z))` with (\l z. z) result.`` () =
        (LambdaAbstraction('z', 
            Application(
                LambdaAbstraction('a', 
                    Variable('z')),
                        Application(
                            LambdaAbstraction('b', 
                                LambdaAbstraction('c', Variable('b'))), Variable('z')))))
        |>
        normalizeTerm |> should equal (LambdaAbstraction('z', Variable('z')))
    
    [<Test>]
    member this.``Test should correct normalize (S K K) with I result.`` () =
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
    member this.``Test should correct normalize with same name (l x. x (l x. x)) (l x. x) with ... result.`` () =
        Application(
            LambdaAbstraction('x', Application(Variable('x'), LambdaAbstraction('x', Variable('x')))), LambdaAbstraction('x', Variable('x')))
        |> normalizeTerm |> should equal (LambdaAbstraction('x', Variable('x')))
    
    [<Test>]
    member this.``Test should correct normalize with different name (l x. x (l y. y)) (l x. x) with ... result.`` () =
        Application(
            LambdaAbstraction('x', Application(Variable('x'), LambdaAbstraction('y', Variable('y')))), LambdaAbstraction('x', Variable('x')))
        |> normalizeTerm |> should equal (LambdaAbstraction('y', Variable('y')))
    
    [<Test>]
    member this.``Test should correct perform alpha conversion (x \l x. x) with argument (x) with ... result.`` () =
        Application(Variable('x'), LambdaAbstraction('x', Variable('x')))
        |> performAlphaConversion ['x']
        |> should equal 
            (Application(Variable('x'), LambdaAbstraction('x', Variable('x'))))    
    
    [<Test>]
    member this.``Test should correct perform alpha conversion (x (\l x. x b)) with argument (x b) with ... result.`` () =
        Application(Variable('x'), LambdaAbstraction('x', Application(Variable('x'), Variable('b'))))
        |> performAlphaConversion ['x'; 'b']
        |> should equal 
            (Application(Variable('x'), LambdaAbstraction('x', Application(Variable('x'), Variable('b')))))    
    
    [<Test>]
    member this.``Test should correct perform alpha conversion (\l x. x b a) with argument (b a) with ... result.`` () =
        LambdaAbstraction('x', Application(Application(Variable('x'), Variable('b')), Variable('a')))
        |> performAlphaConversion ['b'; 'a']
        |> should equal 
            (LambdaAbstraction('x', Application(Application(Variable('x'), Variable('B')), Variable('A'))))

    [<Test>]
    member this.``Test should correct get alphabet from term `(\l x. \l y. x y) b`.`` () =
        Application(LambdaAbstraction('x', LambdaAbstraction('y', Application(Variable('x'), Variable('y')))), Variable('b'))
        |>
        getLocalFreeAlphabet
        |> should equal ['b']
    
    [<Test>]
    member this.``Test should correct get alphabet from term `a b a b`.`` () =
        Application(Application(Application(Variable('a'), Variable('b')), Variable('a')), Variable('b'))
        |>
        getLocalFreeAlphabet
        |> should equal ['b'; 'a']  
    
    [<Test>]
    member this.``Test should correct get alphabet from term `a b c d`.`` () =
        Application(Application(Application(Variable('a'), Variable('b')), Variable('c')), Variable('d'))
        |>
        getLocalFreeAlphabet
        |> should equal ['d'; 'c'; 'b'; 'a']    
    
    [<Test>]
    member this.``Test should correct get alphabet from term `(\l a. a b) b c`.`` () =
        Application(Application(LambdaAbstraction('a', Application(Variable('a'), Variable('b'))), Variable('b')), Variable('c'))
        |>
        getLocalFreeAlphabet
        |> should equal ['c'; 'b']