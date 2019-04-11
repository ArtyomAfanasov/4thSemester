namespace Hw2.Tests

open NUnit.Framework
open FsUnit
open Palindrome

[<TestFixture>]
type PalindromeTestClass () =

    [<Test>]
    member this.``mamam is polinrdom`` () = 
        isPalindrome "mamam" |> should equal true
    
    [<Test>]
     member this.``Son isn't polinrdom`` () = 
        isPalindrome "Son" |> should equal false
    
    [<Test>]
     member this.``With upper case`` () = 
        isPalindrome "Sos" |> should equal true
    
    [<Test>]
     member this.``Meme isn't polidrom`` () =
        isPalindrome "Meme" |> should equal false
    
    [<Test>]
     member this.``Polindrome with spaces`` () =
        isPalindrome "Я иду с мечем судия" |> should equal true 
        
    [<Test>]
     member this.``Polindrome with spaces and upper case`` () =
        isPalindrome "Я иДу С мЕчеМ сУдИя" |> should equal true