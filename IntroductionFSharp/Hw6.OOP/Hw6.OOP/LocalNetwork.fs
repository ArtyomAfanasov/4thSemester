open System.Collections

/// Абстрактный класс с данными о сопротивляемости вирусам разных ОС.
[<AbstractClass>]
type Resistance() =       
    abstract member LinuxResistance: float
    abstract member MacOSResistance: float
    abstract member WindowsResistance: float
    abstract member OtherOSResistance: float

/// Стандартная сопротивляемость вирусам.
type DefaultResistance() =
    inherit Resistance()
    override this.LinuxResistance = 0.7
    override this.MacOSResistance = 0.6
    override this.WindowsResistance = 0.3
    override this.OtherOSResistance = 0.75

/// Моделирует работу локальной сети.
type LocalNetwork(givenListOS : list<string>, givenConnections : int[,], resistance : Resistance) =  
    // let setResistance (resistance : Resistance option) =
    //     match resistance with
    //     | None -> DefaultResistance()
    //     | Some(x) -> x
    
    let countOfOS = List.length givenListOS
    let listOS = givenListOS
    let mutable connections = givenConnections   

        /// Новый круг жизни вирусов.
        ////let newEpoch () =
        ////    (Seq.choose (fun array ->
        ////        Seq.choose (fun link ->
        ////            
        ////            )
        ////            array 
        ////        )
        ////        givenConnections)
        ////                
        ////member this.NewEpoch () = newEpoch ()
        
[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    0