/// Модуль с функцией описывающей бесконечную последовательность 
/// [1, 2, 2, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, …].
module InfinitySequence

/// Сгенерировать последовательность [1, 2, 2, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, …].
let generateInfinitySequence () =   
    (1, 1)
    |>
    Seq.unfold (fun (state, curIndex) ->                
        match curIndex with 
        | _ when curIndex < (((1 + state) * state)/2) -> 
            Some(state, (state, (curIndex + 1)))
        | _ when curIndex = (((1 + state) * state)/2) -> 
            Some(state, (state + 1, (curIndex + 1)))
        )

[<EntryPoint>]
let main argv = 
    Seq.iter (fun elem -> printfn "%d " elem) (generateInfinitySequence ())
    0