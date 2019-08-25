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

    /// Вирус заражает компьютеры, соединенные с данным.
    let infect fromThisComputer =                
        let rec checkVulnerabilitys index =
            if index = lengthOfConnections ()
            elif _connections.[fromThisComputer, index] = 1 then
                let computerInfo = _computers.[index]
                if third computerInfo then checkVulnerabilitys (index + 1)
                else 
                    _computers.[index] <- (first computerInfo, second computerInfo, true)
                    checkVulnerabilitys (index + 1)
            else checkVulnerabilitys (index + 1)
        
        checkVulnerabilitys 0

    /// Новый этап жизни вируса в локальной сети.
    let newEpoch () = 
        let rec findPCWithVirus index = 
            if index = lengthOfComputers then ()
            else
                if third (_computers.[index]) then
                    infect index
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