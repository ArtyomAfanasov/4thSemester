namespace Hw6.OOP.Tests

open FsUnit
open NUnit.Framework
open Foq
open Virus   

[<TestFixture>]
type LocalNetworkTests () =    
    /// Computer info.
    let getComputers () = [|("1", Linux, true); 
                            ("2", Windows, false); 
                            ("3", MacOS, false); 
                            ("4", Other, false); 
                            ("5", Linux, false); 
                            ("6", Windows, false); 
                            ("7", Other, false); 
                            ("8", MacOS, false); 
                            ("9", Other, false)|]

    /// All computers are connected.
    let _fullConnections = array2D [[0; 1; 0; 1; 0; 0; 0; 0; 0];
                                    [1; 0; 1; 0; 1; 0; 0; 0; 0];
                                    [0; 1; 0; 0; 0; 1; 0; 0; 0];
                                    [1; 0; 0; 0; 1; 0; 1; 0; 0];
                                    [0; 1; 0; 1; 0; 1; 0; 1; 0];
                                    [0; 0; 1; 0; 1; 0; 0; 0; 1];
                                    [0; 0; 0; 1; 0; 0; 0; 1; 0];
                                    [0; 0; 0; 0; 1; 0; 1; 0; 1];
                                    [0; 0; 0; 0; 0; 1; 0; 1; 0];]
                              
    /// Return third element of the tuple.
    let third (_, _, c) = c

    [<Test>]
    member this.``With 100% resistance virus should not expand.`` () =
        // arrange
        let resistance = Mock<IResistance>()
                            .Setup(fun x -> <@ x.LinuxResistance @>).Returns(100)
                            .Setup(fun x -> <@ x.MacOSResistance @>).Returns(100)
                            .Setup(fun x -> <@ x.WindowsResistance @>).Returns(100)
                            .Setup(fun x -> <@ x.OtherOSResistance @>).Returns(100)
                            .Create()
        let network = LocalNetwork(getComputers (), _fullConnections, resistance)
        
        // act 
        let rec loop n =
            network.NewEpoch ()
            if n > 0 then loop (n - 1)
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
        let resistance = Mock<IResistance>()
                            .Setup(fun x -> <@ x.LinuxResistance @>).Returns(0)
                            .Setup(fun x -> <@ x.MacOSResistance @>).Returns(0)
                            .Setup(fun x -> <@ x.WindowsResistance @>).Returns(0)
                            .Setup(fun x -> <@ x.OtherOSResistance @>).Returns(0)
                            .Create()
        let network = LocalNetwork(getComputers (), _fullConnections, resistance)
        
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
        let computersInFirstEpoch = network.Computers
        checkVirusInFirstSet true computersInFirstEpoch
        checkVirusInSecondSet false computersInFirstEpoch
        checkVirusInThirdSet false computersInFirstEpoch
        Seq.item 8 computersInFirstEpoch |> third |> should equal false

        network.NewEpoch ()
        let computersInSecondEpoch = network.Computers
        checkVirusInFirstSet true computersInSecondEpoch
        checkVirusInSecondSet true computersInSecondEpoch
        checkVirusInThirdSet false computersInSecondEpoch
        Seq.item 8 computersInSecondEpoch |> third |> should equal false

        network.NewEpoch ()
        let computersInThirdEpoch = network.Computers
        checkVirusInFirstSet true computersInThirdEpoch
        checkVirusInSecondSet true computersInThirdEpoch
        checkVirusInThirdSet true computersInThirdEpoch
        Seq.item 8 computersInThirdEpoch |> third |> should equal false

        network.NewEpoch ()
        let computersInFourthEpoch = network.Computers
        checkVirusInFirstSet true computersInFourthEpoch
        checkVirusInSecondSet true computersInFourthEpoch
        checkVirusInThirdSet true computersInFourthEpoch
        Seq.item 8 computersInFourthEpoch |> third |> should equal true
        
    [<Test>]
    member this.``Should show correct state with 0% resistance.`` () =
        // arrange
        let alias = Other        
        
        let computersInfo = [|("1", alias, true);
                          ("2", alias, false);
                          ("3", alias, false)|]

        let connections = array2D [[0; 1; 0];
                                   [1; 0; 1];
                                   [0; 1; 0]]

        let resistance = Mock<IResistance>()
                            .Setup(fun x -> <@ x.LinuxResistance @>).Returns(0)
                            .Setup(fun x -> <@ x.MacOSResistance @>).Returns(0)
                            .Setup(fun x -> <@ x.WindowsResistance @>).Returns(0)
                            .Setup(fun x -> <@ x.OtherOSResistance @>).Returns(0)
                            .Create()
        let network = LocalNetwork(computersInfo, connections, resistance)

                
        // assert
        network.ShowState () |> should equal ("1 " + (alias.ToString()) + " True\n2 " + alias.ToString() + " False\n3 " + alias.ToString() + " False\n")

        network.NewEpoch () 
        network.ShowState () |> should equal ("1 " + (alias.ToString()) + " True\n2 " + alias.ToString() + " True\n3 " + alias.ToString() + " False\n")
        
        network.NewEpoch () 
        network.ShowState () |> should equal ("1 " + (alias.ToString()) + " True\n2 " + alias.ToString() + " True\n3 " + alias.ToString() + " True\n")