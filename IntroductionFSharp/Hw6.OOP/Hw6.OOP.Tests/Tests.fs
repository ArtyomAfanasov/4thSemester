namespace Hw6.OOP.Tests

open FsUnit
open NUnit.Framework
open Foq
open LocalNetwork    

[<TestFixture>]
type TestClass () =

    [<Test>]
    member this.``Setup property`` () =
        let mock = Mock<Resistance>()
                    .Setup(fun x -> <@ x.LinuxResistance @>).Returns(1.0)
                    .Setup(fun x -> <@ x.MacOSResistance @>).Returns(1.0)
                    .Setup(fun x -> <@ x.WindowsResistance @>).Returns(1.0)
                    .Setup(fun x -> <@ x.OtherOSResistance @>).Returns(1.0)
                    .Create()
        mock.LinuxResistance |> should equal 1.0

    [<Test>]
    member this.``Instance should be created with default restist`` () =       
        let doubleDemensionArray = Array2D.zeroCreate<int> 0 0        
        let instance = LocalNetwork(List<string>.Empty, doubleDemensionArray, DefaultResistance())
        Assert.IsTrue(true)

    // ====================================================================================================================================