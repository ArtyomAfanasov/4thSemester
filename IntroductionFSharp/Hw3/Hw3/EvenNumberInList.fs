module EvenNumberInList

open System.IO

type Pet =
    { Nick: string;
      DataBirthday: System.DateTime; }

type Route = int
type Make = string
type Model = string

type Proposition =
    | True
    | And of Proposition * Proposition
    | Or of Proposition * Proposition
    | Not of Proposition

type Transport =
    | Car of Make * Model
    | Bicycle
    | Bus of Route

type node =
    { Name : string;
    Links : link list }
and link =
    | Dangling
    | Link of node
and sommm =
    | Niche

type CustomerId = CustomerId of int // размеченное объединение
type OrderId = OrderrrId of int





[<EntryPoint>]
let main argv =    

  
    let printOrderId (OrderrrId orderId) = // деконструкция в параметре
        printfn "The orderId is %i" orderId
    let customerId = OrderrrId 1
    printOrderId customerId //

    let rec eval (p: Proposition) =
        match p with
        | True -> true
        | And(p1, p2) -> eval p1 && eval p2
        | Or (p1, p2) -> eval p1 || eval p2
        | Not(p1) -> not (eval p1)

    printfn "%A" <| eval (Or(True, And(True, Not True)))

   

    let f (Bus bus) = 
        5

    let checkerboardCoordinates n =
        seq { for row in 1 .. n do
            for col in 1 .. n do
                if (row + col) % 2 = 0 then
                    yield (row, col) }

    checkerboardCoordinates 8 |> Seq.iter (printfn "%A")

    let withBang = 
        seq { for i in 1..100 do 
                yield! seq { i..i + 5 } }        

    let witohutBang = 
        seq { for i in 1..100 do
                yield seq { i..i + 5 } }
    
    withBang |> Seq.iter (printf "%A")

    witohutBang |> Seq.iter (printf "%A")

    let reader =
        seq {
            use reader = new StreamReader(
                File.OpenRead("test.txt")
                )
            while not reader.EndOfStream do
                    yield reader.ReadLine() }

    let requset = Seq.toList reader
     
    

    let dog = { Nick = "Will"; DataBirthday = new System.DateTime(1962, 09, 02)}

    let cat = { dog with Nick = "Kitty"}

    let { Nick = nick; DataBirthday = data} = cat

    let nome = nick 

    
    
    
    

    let str = cat.Nick

    0 