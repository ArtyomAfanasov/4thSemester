/// Содержит информацию о компьютере.
module Computer

open OS

/// Класс с информацией о компьютере.
type Computer(name: string, OS: OS, infected: bool) =    
    /// Имя компьютера.
    member val Name = name with get, set

    /// ОС компьютера.
    member val OS = OS with get, set

    /// Здоровье компьютера.
    member val Infected = infected with get, set

    /// Попытаться заразить компьютеры.
    static member TryInfect (indexOfPrey : int) (preyInfo : Computer) (computers : Computer[]) (OSResistance : Map<OS, int>) (isNewbie : bool[]) =            
        let random = System.Random()
        
        let innerAttemptInfection nameOfOS =
            let bigIsDangerous = random.Next(0, 100)
            if bigIsDangerous > OSResistance.Item nameOfOS then
                computers.[indexOfPrey] <- new Computer(preyInfo.Name, preyInfo.OS, true)
                isNewbie.[indexOfPrey] <- true

        match preyInfo.OS with
        | Linux -> innerAttemptInfection Linux
        | Windows -> innerAttemptInfection Windows            
        | MacOS -> innerAttemptInfection MacOS            
        | Other -> innerAttemptInfection Other
        
        computers