namespace Hw6.OOP.Tests

open FsUnit
open NUnit.Framework
open Foq
open LocalNetwork    

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
        
        let checkVirusFirstSet isIll epoch = 
            let rec loop n =                
                if n = 4 then ()
                else 
                    third (Seq.item (List.item n [1; 2; 4; 5]) epoch) |> should equal isIll
                    loop (n + 1)
                
            loop 0

        let checkVirusSecondSet isIll epoch = 
            let rec loop n =                
                if n = 5 then ()
                else 
                    third (Seq.item (List.item n [3; 6; 7; 8; 9]) epoch) |> should equal isIll
                    loop (n + 1)
            
            loop 0

        // act 
        network.NewEpoch ()
        let firstEpoch = network.Computers
        network.NewEpoch ()
        let secondEpoch = network.Computers

        // assert
        checkVirusFirstSet true firstEpoch
        checkVirusSecondSet false firstEpoch

        checkVirusFirstSet true secondEpoch
        checkVirusSecondSet true secondEpoch