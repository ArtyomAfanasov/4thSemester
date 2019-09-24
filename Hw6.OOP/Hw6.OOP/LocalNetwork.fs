module Virus

/// Абстрактный класс с данными о сопротивляемости вирусам разных ОС.
[<AbstractClass>]
type Resistance() =       
    abstract member LinuxResistance: int
    abstract member MacOSResistance: int
    abstract member WindowsResistance: int
    abstract member OtherOSResistance: int

/// Стандартная сопротивляемость вирусам.
type DefaultResistance() =
    inherit Resistance()
    override this.LinuxResistance = 70
    override this.MacOSResistance = 60
    override this.WindowsResistance = 30
    override this.OtherOSResistance = 75

/// Моделирует работу локальной сети. OS: linux, windows, macos, other.
type LocalNetwork(computers : (string * string * bool)[], connections : int[,], resistance : Resistance) =         
        
    /// Получить первый элемент кортежа.
    let first (a, _, _) = a

    /// Получить второй элемент кортежа.
    let second (_, b, _) = b

    /// Получить третий элемент кортежа.
    let third (_, _, c) = c 
    
    /// Список операционных систем в локальной сети.
    let mutable _computers = 
        let mutable inner = computers
        
        Seq.iteri (fun index PC ->     
            let lowerCaseOSName = ((second PC) : string).ToLower()
            inner.[index] <- (first PC, lowerCaseOSName, third PC)) inner
        
        inner

    /// Вспомогательный массив для избежания заражения через вновь заражённых.
    let mutable isNewbie = Array.create _computers.Length false

    /// Матрица смежности соединения компьютеров в локальной сети.
    let _connections = connections

    /// Сопротивление операционных систем.
    let _resistance = resistance

    /// Длина первого измерения (отвечает за соединение с другими ПК) двумерного массива соединений.   
    let lengthOfConnections = Array2D.length1 _connections

    /// Количество компьютеров в сети.
    let lengthOfComputers = _computers.Length  

    /// Linux ОС.
    let linux = "linux"

    /// Windows ОС.
    let windows = "windows"

    /// MacOS ОС.
    let macos = "macos"

    /// Другие ОС.
    let other = "other"

    /// Сопротивляемость ОС.
    let OSResistance = Map.ofList [ ("linux", _resistance.LinuxResistance); 
                                    ("windows", _resistance.WindowsResistance); 
                                    ("macos", _resistance.MacOSResistance); 
                                    ("other", _resistance.OtherOSResistance) ]  
    
    /// Объект для случайных величин.
    let random = System.Random()    

    /// Попытаться заразить компьютер.
    let tryInfect indexOfPrey preyInfo = 
        let innerAttemptInfection nameOfOS = 
            let bigIsDangerous = random.Next(0,100)
            if bigIsDangerous > OSResistance.Item nameOfOS then
                _computers.[indexOfPrey] <- (first preyInfo, second preyInfo, true)
                isNewbie.[indexOfPrey] <- true

        match second preyInfo with
        | x when x = linux -> 
            innerAttemptInfection linux
        | x when x = windows ->
            innerAttemptInfection windows
        | x when x = macos ->
            innerAttemptInfection macos
        | _ ->
            innerAttemptInfection other
            
    /// Найти компьютеры, заражённые с прошлой эпохи, либо с самого начала.
    let noOneIsNewbieNow () = 
        let rec loop step =
            if step = lengthOfComputers then ()
            else 
                isNewbie.[step] <- false
                loop (step + 1)

        loop 0

    /// Найти компьютеры, соединенные с заражённым.
    let checkConnections fromThisComputer =                
        let rec loop index =
            if index = lengthOfConnections then ()
            elif _connections.[fromThisComputer, index] = 1 then                

                // Попытаться заразить жертву:
                let preyInfo = _computers.[index]
                if third preyInfo then loop (index + 1)
                else 
                    tryInfect index preyInfo
                    loop (index + 1)
            else loop (index + 1)
        
        loop 0        

    /// Новый этап жизни вируса в локальной сети.
    let newEpoch () = 
        //let illComputers = findOldVirus _computers
        
        let rec findPCWithVirus index = 
            if index = lengthOfComputers then ()
            else
                if third _computers.[index] && isNewbie.[index] <> true then
                    checkConnections index
                    findPCWithVirus (index + 1)
                else findPCWithVirus (index + 1)
                
        findPCWithVirus 0

        noOneIsNewbieNow ()
    
    /// Новый этап жизни вируса в локальной сети.
    member this.NewEpoch() = newEpoch ()

    /// Информация о компьютерах: имя, OS, заражён ли.
    member this.Computers =         
        _computers
    
    /// Инициализирует новый экземпляр класса LocalNetwork с сопротивлением к вирусам по умолчанию.
    new(computers : (string * string * bool)[], connections : int[,]) = LocalNetwork(computers, connections, DefaultResistance())        
        
[<EntryPoint>]
let main argv =    
    0