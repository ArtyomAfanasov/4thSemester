module Virus

/// Абстрактный класс с данными о сопротивляемости вирусам разных ОС.
type IResistance =       
    abstract member LinuxResistance: int
    abstract member MacOSResistance: int
    abstract member WindowsResistance: int
    abstract member OtherOSResistance: int

/// Стандартная сопротивляемость вирусам.
type DefaultResistance() =
    interface IResistance with
        member this.LinuxResistance = 70
        member this.MacOSResistance = 60
        member this.WindowsResistance = 30
        member this.OtherOSResistance = 75

/// Операционные системы.
type OS =
    | Linux
    | Windows
    | MacOS
    | Other

/// Моделирует работу локальной сети. OS: linux, windows, macos, other.
type LocalNetwork(computers : (string * OS * bool)[], connections : int[,], resistance : IResistance) =         
        
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
            inner.[index] <- (first PC, second PC (*lowerCaseOSName*), third PC)) inner
        
        inner

    /// Вспомогательный массив для избежания заражения через вновь заражённых.
    let mutable isNewbie = Array.create _computers.Length false

    /// Сопротивление операционных систем.
    let _resistance = resistance

    /// Длина первого измерения (отвечает за соединение с другими ПК) двумерного массива соединений.   
    let lengthOfConnections = Array2D.length1 connections

    /// Количество компьютеров в сети.
    let lengthOfComputers = _computers.Length  
    
    /// Сопротивляемость ОС.
    let OSResistance = Map.ofList [ (Linux, _resistance.LinuxResistance); 
                                    (Windows, _resistance.WindowsResistance); 
                                    (MacOS, _resistance.MacOSResistance); 
                                    (Other, _resistance.OtherOSResistance) ]  
    
    /// Объект для случайных величин.
    let random = System.Random()    

    /// Попытаться заразить компьютер.
    let tryInfect indexOfPrey preyInfo = 
        let innerAttemptInfection nameOfOS = 
            let bigIsDangerous = random.Next(0, 100)
            if bigIsDangerous > OSResistance.Item nameOfOS then
                _computers.[indexOfPrey] <- (first preyInfo, second preyInfo, true)
                isNewbie.[indexOfPrey] <- true

        match second preyInfo with
        | Linux -> innerAttemptInfection Linux
        | Windows -> innerAttemptInfection Windows            
        | MacOS -> innerAttemptInfection MacOS            
        | Other -> innerAttemptInfection Other
            
    /// Сбросить информацию о заражённых в эту эпоху, сделав их "не новичками".
    let resetInfectedInfo () = 
        let rec loop step =
            if step = lengthOfComputers then ()
            else 
                isNewbie.[step] <- false
                loop (step + 1)

        loop 0

    /// Найти компьютеры, соединенные с заражённым.
    let findConnectedComputers fromThisComputer =                
        let rec loop index =
            if index = lengthOfConnections then ()
            elif connections.[fromThisComputer, index] = 1 then                

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
        let rec findPCWithVirus index = 
            if index = lengthOfComputers then ()
            else
                if third _computers.[index] && not <| isNewbie.[index] then
                    findConnectedComputers index
                    findPCWithVirus (index + 1)
                else findPCWithVirus (index + 1)
                
        findPCWithVirus 0

        resetInfectedInfo ()

    /// Состояние сети.
    let showState () = 
        Seq.fold (fun state PC ->
            (state + first PC + " " + (second PC).ToString() + " " + (third PC).ToString() + "\n")) "" _computers
    
    /// Новый этап жизни вируса в локальной сети.
    member this.NewEpoch() = newEpoch ()

    /// Информация о компьютерах: имя, OS, заражён ли.
    member this.Computers = _computers

    /// Состояние сети.
    member this.ShowState() = showState ()
    
    /// Инициализирует новый экземпляр класса LocalNetwork с сопротивлением к вирусам по умолчанию.
    new(computers : (string * OS * bool)[], connections : int[,]) = LocalNetwork(computers, connections, DefaultResistance())        
        
[<EntryPoint>]
let main argv =       
    0