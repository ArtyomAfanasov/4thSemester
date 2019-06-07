/// Модуль, содержащий функцию, которая по произвольной строке 
/// проверяет корректность скобочной последовательности в этой строке. 
/// Скобки бывают трёх видов.
module BracketSequence 

open System.Text.RegularExpressions

/// Собственный тип. 'a, 'b, 'c - конкретные значения типов.
///
/// Remarks: подходит и для скобок.
type ObjectType<'a, 'b, 'c> =
    | First of 'a * 'a
    | Second of 'b * 'b 
    | Third of 'c * 'c

/// Проверить корректность последовательности выбранных типов.
/// first/second/thirdType - рассматриваемые типы.
/// Если какой-то из типов не нужен, то задайте для него конкретную пару значений (null, null).
///
/// Remarks: подходит и для скобочной последовательности.
let checkBrackets inputWithSeq firstType secondType thirdType =        
    match firstType, secondType, thirdType with
    | First (a, b), Second (c, d), Third (e, f) -> 
        let openF = a
        let closeF= b
        let openS = c
        let closeS = d
        let openT = e
        let closeT = f

        let countOfOpenAndCloseValues = 
            (Seq.fold (fun ((oF, cF), (oS, cS), (oT, cT)) elem ->
                match elem with 
                | a when a = openF -> ((oF + 1, cF), (oS, cS), (oT, cT))
                | b when b = closeF -> ((oF, cF + 1), (oS, cS), (oT, cT))
                | c when c = openS -> ((oF, cF), (oS + 1, cS), (oT, cT))
                | d when d = closeS -> ((oF, cF), (oS, cS + 1), (oT, cT))
                | e when e = openT -> ((oF, cF), (oS, cS), (oT + 1, cT))
                | f when f = closeT -> ((oF, cF), (oS, cS), (oT, cT + 1))
                | _ -> ((oF, cF), (oS, cS), (oT, cT))

                ) ((0, 0), (0, 0), (0, 0)) inputWithSeq)
        
        let (countOpenFirst, countCloseFirst), (countOpenSecond, countCloseSecond), (countOpenThird, countCloseThird) = 
            countOfOpenAndCloseValues

        if (countOpenFirst = countCloseFirst && 
            countOpenSecond = countCloseSecond &&
            countOpenThird = countCloseThird) then true
        else false
               
    | _ -> failwith "Should never get here."    

[<EntryPoint>]
let main argv =      
    0