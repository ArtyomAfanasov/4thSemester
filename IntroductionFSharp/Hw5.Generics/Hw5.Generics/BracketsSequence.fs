/// Модуль, содержащий функцию, которая по произвольной строке 
/// проверяет корректность скобочной последовательности в этой строке. 
/// Скобки бывают трёх видов.
module BracketSequence 

(*
/// Собственный тип. 'a - конкретное значение типа.
///
/// Remarks: подходит и для скобок.
type ObjectType<'a> =
    | FirstValue of 'a * 'a
    | SecondValue of 'a * 'a
    | ThirdValue of 'a * 'a
*)

/// Проверить корректность последовательности выбранных символов.
/// Первый элемент пары - это открывающий символ. Второй - закрывающий.
///
/// Если какая-то из скобок не нужна, то задайте для неё пару значений char по умолчанию ('\u0000', '\u0000').
/// ? Можно было бы сделать три функции, чтобы пользователь не заморачивался. Как лучше ?
let checkBrackets (input : string) (firstValue : char* char) (secondValue : char* char) (thirdValue : char* char) =
    let openF, closeF = firstValue
    let openS, closeS = secondValue
    let openT, closeT = thirdValue
    
    // Из (()) получу ))((.
    let listsOfOpenAndCloseValues =
        Seq.fold (fun (firstList, secondList, thirdList)(*((listOpenF, listCloseF), (listOpenS, listCloseS), (listOpenT, listCloseT))*) elem ->
            match elem with 
            | def when def = '\u0000' -> (firstList, secondList, thirdList)
            | a when a = openF -> (elem :: firstList, secondList, thirdList)
            | b when b = closeF -> (elem :: firstList, secondList, thirdList)
            | c when c = openS -> (firstList, elem :: secondList, thirdList)
            | d when d = closeS -> (firstList, elem :: secondList, thirdList)
            | e when e = openT -> (firstList, secondList, elem :: thirdList)
            | f when f = closeT -> (firstList, secondList, elem :: thirdList)
            | _ -> (firstList, secondList, thirdList)
           ) ([], [], []) input
    
    let firstCouples, secondCouples, thirdCouples = listsOfOpenAndCloseValues
        
    let rec loop list number =
        let checkList list openBr closeBr = 
            match list with   
            | [] -> true
            | head :: tail -> 
                if head = closeBr && List.last tail = openBr then 
                    loop (List.rev (List.rev tail).Tail) number
                else false

        match number with 
        | 1 -> checkList list openF closeF                        
        | 2 -> checkList list openS closeS 
        | 3 -> checkList list openT closeT 

    loop firstCouples 1 && loop secondCouples 2 && loop thirdCouples 3






















    //Seq.choose (fun elem ->
    //    match elem with 
    //    | def when def = '\u0000' -> None
    //    | a when a = openF -> Some(elem)
    //    | b when b = closeF -> Some(elem)
    //    | c when c = openS -> Some(elem)
    //    | d when d = closeS -> Some(elem)
    //    | e when e = openT -> Some(elem)
    //    | f when f = closeT -> Some(elem)
    //    | _ -> None
    //
    //    ) inputWithSeq            

(*
/// Проверить корректность последовательности выбранных типов.
/// first/second/thirdType - рассматриваемые типы.
/// Если какой-то из типов не нужен, то задайте для него конкретную пару значений по умолчанию
///
/// Remarks: подходит и для скобочной последовательности.
let checkBrackets inputWithSeq firstType secondType thirdType =        
    match firstType, secondType, thirdType with
    | FirstValue (a, b), SecondValue (c, d), ThirdValue (e, f) -> 
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
*)


[<EntryPoint>]
let main argv =         
    0