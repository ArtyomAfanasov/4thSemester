namespace Hw6.OOP.Tests

open FsUnit
open NUnit.Framework
open Foq
open Virus   

[<TestFixture>]
type LocalNetworkTests () =    
    /// Операционные системы
    let _computers = [|("1", "linux", true); 
                    ("2", "windows", false); 
                    ("3", "macos", false); 
                    ("4", "other", false); 
                    ("5", "linux", false); 
                    ("6", "windows", false); 
                    ("7", "other", false); 
                    ("8", "macos", false); 
                    ("9", "other", false)|]

    /// Все компьютеры соединены.
    let _fullConnections = array2D [[0; 1; 0; 1; 0; 0; 0; 0; 0];
                                    [1; 0; 1; 0; 1; 0; 0; 0; 0];
                                    [0; 1; 0; 0; 0; 1; 0; 0; 0];
                                    [1; 0; 0; 0; 1; 0; 1; 0; 0];
                                    [0; 1; 0; 1; 0; 1; 0; 1; 0];
                                    [0; 0; 1; 0; 1; 0; 0; 0; 1];
                                    [0; 0; 0; 1; 0; 0; 0; 1; 0];
                                    [0; 0; 0; 0; 1; 0; 1; 0; 1];
                                    [0; 0; 0; 0; 0; 1; 0; 1; 0];]
                              
    /// Вернуть третий элемент кортежа.
    let third (_, _, c) = c

    [<Test>]
    member this.``With 100% resistance virus should not expand.`` () =
        // assert
        let resistance = Mock<Resistance>()
                            .Setup(fun x -> <@ x.LinuxSusceptibility @>).Returns(0.0)
                            .Setup(fun x -> <@ x.MacOSSusceptibility @>).Returns(0.0)
                            .Setup(fun x -> <@ x.WindowsSusceptibility @>).Returns(0.0)
                            .Setup(fun x -> <@ x.OtherOSSusceptibility @>).Returns(0.0)
                            .Create()
        let network = LocalNetwork(_computers, _fullConnections, resistance)
        
        // act 
        let rec loop n =
            network.NewEpoch ()
            if n > 0 then loop (n-1)
            else ()
        loop 10
           
        // assert
        Seq.fold (fun acc elem -> 
                    if third elem then acc 
                    else (acc + 1)) 0 network.Computers
        |> should equal 8
    
    [<Test>]
    member this.``With 0% resistance virus should expand like BFS.`` () =
        // arrange
        let resistance = Mock<Resistance>()
                            .Setup(fun x -> <@ x.LinuxSusceptibility @>).Returns(1.0)
                            .Setup(fun x -> <@ x.MacOSSusceptibility @>).Returns(1.0)
                            .Setup(fun x -> <@ x.WindowsSusceptibility @>).Returns(1.0)
                            .Setup(fun x -> <@ x.OtherOSSusceptibility @>).Returns(1.0)
                            .Create()        
        let network = LocalNetwork(_computers, _fullConnections, resistance)
        
        let checkVirusInFirstSet isIll epoch = 
            let rec loop n =                
                if n = 3 then ()
                else 
                    third (Seq.item (List.item n [0; 1; 3]) epoch) |> should equal isIll
                    loop (n + 1)
                
            loop 0

        let checkVirusInSecondSet isIll epoch = 
            let rec loop n =                
                if n = 3 then ()
                else 
                    third (Seq.item (List.item n [2; 4; 6]) epoch) |> should equal isIll
                    loop (n + 1)
            
            loop 0

        let checkVirusInThirdSet isIll epoch = 
            let rec loop n =                
                if n = 2 then ()
                else 
                    third (Seq.item (List.item n [5; 7]) epoch) |> should equal isIll
                    loop (n + 1)
            
            loop 0

        // assert
        network.NewEpoch ()
        let firstEpoch = network.Computers
        checkVirusInFirstSet true firstEpoch
        checkVirusInSecondSet false firstEpoch
        checkVirusInThirdSet false firstEpoch
        Seq.item 8 firstEpoch |> should equal false

        network.NewEpoch ()
        let secondEpoch = network.Computers
        checkVirusInFirstSet true secondEpoch
        checkVirusInSecondSet true secondEpoch
        checkVirusInThirdSet false secondEpoch
        Seq.item 8 secondEpoch |> should equal false

        network.NewEpoch ()
        let thirdEpoch = network.Computers
        checkVirusInFirstSet true thirdEpoch
        checkVirusInSecondSet true thirdEpoch
        checkVirusInThirdSet true thirdEpoch
        Seq.item 8 thirdEpoch |> should equal false

        network.NewEpoch ()
        let fourthEpoch = network.Computers
        checkVirusInFirstSet true fourthEpoch
        checkVirusInSecondSet true fourthEpoch
        checkVirusInThirdSet true fourthEpoch
        Seq.item 8 fourthEpoch |> should equal true