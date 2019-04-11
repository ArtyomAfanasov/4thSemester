module ``PalindromeTest``

open FsUnit
open NUnit.Framework

[<Test>]
let ``mamam is polinrdom`` () = 
    Palindrome.isPolindrome "mamam" |> should equal true

[<Test>]
let ``Son isn't polinrdom`` () = 
    Palindrome.isPolindrome "Son" |> should equal false

[<Test>]
let ``With upper case`` () = 
    Palindrome.isPolindrome "Sos" |> should equal true

[<Test>]
let ``Meme isn't polidrom`` () =
    Palindrome.isPolindrome "Meme" |> should equal false

[<Test>]
let ``Polindrome with spaces`` () =
    Palindrome.isPolindrome "Я иду с мечем судия" |> should equal true 
    
[<Test>]
let ``Polindrome with spaces and upper case`` () =
    Palindrome.isPolindrome "Я иДу С мЕчеМ сУдИя" |> should equal true