namespace Interpreter.Tests

open NUnit.Framework
open FsUnit
open AttractiveInterpreter

[<TestFixture>]
type AttractiveInterpreterTestClass () =    
    [<Test>]
    member this.``Complex test: alpha conversion, normalization. Test should correct normalize complex term.`` () =
        let theInnermostRightTerm =
            Application(LambdaAbstraction('x', Application(Application(Variable('x'), Variable('y')), Variable('x'))), Variable('y'))
        let theInnermostLeftTerm =
            Application(Variable('x'), Variable('z'))
        let bigLeftTerm =
            LambdaAbstraction('x', LambdaAbstraction('y', LambdaAbstraction('z', Application(theInnermostLeftTerm, theInnermostRightTerm))))
        let mainTerm = 
            Application(Application(bigLeftTerm ,Variable('y')), LambdaAbstraction('x', Variable('x')))

        let expected =
            LambdaAbstraction('z', Application(Application(Variable('y'), Variable('z')), LambdaAbstraction('x', Variable('x'))))

        mainTerm.Normalized |> should equal expected