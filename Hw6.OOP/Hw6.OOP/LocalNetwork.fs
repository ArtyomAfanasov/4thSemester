module Virus

open Computer
open OS

/// Моделирует работу локальной сети. OS: linux, windows, macos, other.
type LocalNetwork(computers : Computer[], connections : int[,], resistance : IResistance) =                 
    /// Компьютеры.
    let mutable _computers = computers
    
    /// Вспомогательный массив для избежания заражения через вновь заражённых.
    let isNewbie = Array.create computers.Length false
    
    /// Длина первого измерения (отвечает за соединение с другими ПК) двумерного массива соединений.   
    let lengthOfConnections = Array2D.length1 connections

    /// Количество компьютеров в сети.
    let lengthOfComputers = computers.Length  
    
    /// Сопротивляемость ОС.
    let OSResistance = Map.ofList [ (Linux, resistance.LinuxResistance); 
                                    (Windows, resistance.WindowsResistance); 
                                    (MacOS, resistance.MacOSResistance); 
                                    (Other, resistance.OtherOSResistance) ]      
            
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
                let preyInfo = computers.[index]
                if preyInfo.Infected then loop (index + 1)
                else 
                    _computers <- Computer.TryInfect index preyInfo computers OSResistance isNewbie //tryInfect index preyInfo
                    loop (index + 1)
            else loop (index + 1)
        
        loop 0        

    /// Новый этап жизни вируса в локальной сети.
    let newEpoch () =         
        let rec findPCWithVirus index = 
            if index = lengthOfComputers then ()
            else
                if computers.[index].Infected && not <| isNewbie.[index] then
                    findConnectedComputers index
                    findPCWithVirus (index + 1)
                else findPCWithVirus (index + 1)
                
        findPCWithVirus 0

        resetInfectedInfo ()

    /// Состояние сети.
    let showState () = 
        Seq.fold (fun state (PC : Computer) ->
            (state + PC.Name + " " + (PC.OS).ToString() + " " + (PC.Infected).ToString() + "\n")) "" computers
    
    /// Новый этап жизни вируса в локальной сети.
    member this.NewEpoch() = newEpoch ()

    /// Информация о компьютерах: имя, OS, заражён ли.
    member this.Computers = _computers

    /// Состояние сети.
    member this.ShowState() = showState ()
    
    /// Инициализирует новый экземпляр класса LocalNetwork с сопротивлением к вирусам по умолчанию.
    new(computers : Computer[], connections : int[,]) = LocalNetwork(computers, connections, DefaultResistance())        
        
[<EntryPoint>]
let main argv =       
    0