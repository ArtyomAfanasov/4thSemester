module TestSrArimf

let average list =
    let count = List.length list
    let rec loop tail acc =
        match tail with 
        | head :: tail -> loop tail (acc + head)
        | _ -> acc

    loop list 0