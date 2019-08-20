open System.Collections.Generic

/// Абстрактный класс с данными о сопротивляемости вирусам разных ОС.
[<AbstractClass>]
type Resistance() =       
    abstract member LinuxSusceptibility: float
    abstract member MacOSSusceptibility: float
    abstract member WindowsSusceptibility: float
    abstract member OtherOSSusceptibility: float

/// Стандартная сопротивляемость вирусам.
type DefaultResistance() =
    inherit Resistance()
    override this.LinuxSusceptibility = 0.7
    override this.MacOSSusceptibility = 0.6
    override this.WindowsSusceptibility = 0.3
    override this.OtherOSSusceptibility = 0.75

/// Моделирует работу локальной сети. OS: linux, windows, macos, other.
type LocalNetwork(computers : (string * string * bool)list, connections : int[,], resistance : Resistance) =         
    
    /// Список операционных систем в локальной сети.
    let mutable _computers = computers

    /// Матрица смежности соединения компьютеров в локальной сети.
    let _connections = connections

    /// Сопротивление операционных систем.
    let _resistance = resistance

    /// Новый этап жизни вируса в локальной сети.
    member this.NewEpoch() = ()

    /// Информация о компьютерах: имя, OS, заражён ли.
    member this.Computers = _computers
    
    /// Инициализирует новый экземпляр класса LocalNetwork с сопротивлением к вирусам по умолчанию.
    new(computers : (string * string * bool)list, connections : int[,]) = LocalNetwork(computers, connections, DefaultResistance())        
        
[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    0

    //// let countOfOS = List.length OSList


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