/// Входная точка приложения
module EntryPoint 
    
    [<EntryPoint>]
    let main argv =                 
        printfn "%i" (Factorial.factorial 0)
        0