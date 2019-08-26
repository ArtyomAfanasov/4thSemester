module Virus

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
type LocalNetwork(computers : (string * string * bool)[], connections : int[,], resistance : Resistance) =         
    
    /// Список операционных систем в локальной сети.
    let mutable _computers = computers

    /// Для избежания заражения через вновь заражённых.
    let mutable supportInOrderToFindNewbie = Array.create _computers.Length bool

    /// Матрица смежности соединения компьютеров в локальной сети.
    let _connections = connections

    /// Сопротивление операционных систем.
    let _resistance = resistance

    /// Длина первого измерения (отвечает за соединение с другими ПК) двумерного массива соединений.   
    let lengthOfConnections = Array2D.length1 _connections

    /// Количество компьютеров в сети.
    let lengthOfComputers = _computers.Length 

    /// Получить первый элемент кортежа.
    let first (a, _, _) = a

    /// Получить второй элемент кортежа.
    let second (_, b, _) = b

    /// Получить третий элемент кортежа.
    let third (_, _, c) = c   
    
    /// Попытаться заразить компьютер.
    let tryInfect indexOfPrey preyInfo =         
        _computers.[indexOfPrey] <- (first preyInfo, second preyInfo, true)

    /// Найти компьютеры, соединенные с заражённым.
    let checkConnections fromThisComputer =                
        let rec loop index =
            if index = lengthOfConnections then ()
            elif _connections.[fromThisComputer, index] = 1 then
                
                // Заразить жертву:
                let preyInfo = _computers.[index]
                if third preyInfo then loop (index + 1)
                else 
                    tryInfect index preyInfo
                    
                    //_computers.[index] <- (first computerInfo, second computerInfo, true)
                    loop (index + 1)
            else loop (index + 1)
        
        loop 0

    /// Найти компьютеры, заражённые с прошлой эпохи, либо с самого начала.
    let resetSupportedArray () = 
        let rec loop step =
            if step = lengthOfComputers then ()
            else 
                supportInOrderToFindNewbie.[step] <- false
                loop (step + 1)
        loop 0

    /// Новый этап жизни вируса в локальной сети.
    let newEpoch () = 
        //let illComputers = findOldVirus _computers
        
        let rec findPCWithVirus index = 
            if index = lengthOfComputers then ()
            else
                if third (_computers.[index]) then
                    checkConnections index
                    findPCWithVirus (index + 1)
                else findPCWithVirus (index + 1)
                
        findPCWithVirus 0
    
    /// Новый этап жизни вируса в локальной сети.
    member this.NewEpoch() = newEpoch ()

    /// Информация о компьютерах: имя, OS, заражён ли.
    member this.Computers = _computers
    
    /// Инициализирует новый экземпляр класса LocalNetwork с сопротивлением к вирусам по умолчанию.
    new(computers : (string * string * bool)[], connections : int[,]) = LocalNetwork(computers, connections, DefaultResistance())        
        
[<EntryPoint>]
let main argv =    
    0